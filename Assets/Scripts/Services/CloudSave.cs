using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models.Data.Player;
using UnityEngine;
using SaveOptions = Unity.Services.CloudSave.Models.Data.Player.SaveOptions;

namespace tusj.Services {

public static class CloudSave {

    private const int CLOUD_TIMESTAMP_ADJUSTMENT = 3;

    public static event Action OnPreSave;
    
    private static readonly SaveOptions PublicSaveOptions = new(new PublicWriteAccessClassOptions());
    private static readonly SaveOptions DefaultSaveOptions = new(new DefaultWriteAccessClassOptions());
    private static readonly LoadOptions PublicLoadOptions = new(new PublicReadAccessClassOptions());
    private static readonly LoadOptions DefaultLoadOptions = new(new DefaultReadAccessClassOptions());
    
    private static readonly List<ISavable> Variables = new();
    private static readonly List<ISavable> LocalOnlyVariables = new();

    //This needs to be beneath the variables
    public static readonly Savable<int> SaveTimestamp = new("SaveTimestamp", int.MinValue);
    
    /// <summary>
    /// Save all variables to the cloud at once.
    /// </summary>
    public static async Task SaveAllData() {
        //Pre-save tasks
        OnPreSave?.Invoke();
        var unixTimestamp = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        SaveTimestamp.Write(unixTimestamp);
        
        //Cloud variables
        var data = new Dictionary<string, object>();
        foreach (var savable in Variables)
            data.Add(savable.GetKey(), savable.Read());

        //Local variables
        var localData = new Dictionary<string, object>(data);
        foreach (var savable in LocalOnlyVariables) {
            localData.Add(savable.GetKey(), savable.Read());
        }

        SaveLocalDataInternal(localData);
        await SaveDataInternal(data);
    }

    /// <summary>
    /// Save a single variable to the cloud.
    /// </summary>
    /// <param name="savable">The variable to save.</param>
    /// <param name="localOnly">Whether to save this variable to cloud</param>
    public static async Task SaveData(ISavable savable, bool localOnly = false) {
        var data = new Dictionary<string, object> {
            {savable.GetKey(), savable.Read()}
        };
        
        SaveLocalDataInternal(data);
        if (!localOnly)
            await SaveDataInternal(data);
    }

    /// <summary>
    /// Loads all variables from the cloud at once.
    /// </summary>
    public static async Task LoadAllData() {
        var keys = new HashSet<string>();

        foreach (var variable in Variables)
            keys.Add(variable.GetKey());
        
        //Get data from cloud
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys, GetLoadOptions());

        //Compare save timestamps
        var cloudTimestamp = data.TryGetValue("SaveTimestamp", out var cloudValue) ? cloudValue.Value.GetAs<int>() : int.MinValue;
        var localTimestamp = PlayerPrefs.HasKey("SaveTimestamp") ? PlayerPrefs.GetInt("SaveTimestamp") : int.MinValue;

        //If cloud data is newer, load that, otherwise load local data
        if (cloudTimestamp + CLOUD_TIMESTAMP_ADJUSTMENT > localTimestamp) {
            Debug.Log("Cloud data is newer than local data. Loading cloud data.");
            foreach (var variable in Variables)
                if (data.TryGetValue(variable.GetKey(), out var value))
                    variable.Write(value.Value.GetAs<object>());
        } else {
            Debug.Log("Local data is newer than cloud data. Loading local data.");
            LoadLocalData(Variables);
        }
        
        //Load local only variables
        LoadLocalData(LocalOnlyVariables);
    }

    /// <summary>
    /// Loads all variables from PlayerPrefs at once.
    /// </summary>
    public static void LoadLocalData(List<ISavable> variables) {
        foreach (var variable in variables) {
            var key = variable.GetKey();
            if (PlayerPrefs.HasKey(key)) {
                var type = variable.Read().GetType();
                
                if (type == typeof(float))
                    variable.Write(PlayerPrefs.GetFloat(key));
                if (type == typeof(int))
                    variable.Write(PlayerPrefs.GetInt(key));
                if (type == typeof(string))
                    variable.Write(PlayerPrefs.GetString(key));
                if (type == typeof(bool))
                    variable.Write(PlayerPrefs.GetInt(key) == 1);
            }
        }
    }

    /// <summary>
    /// Loads all data as local. Used for internet fallback.
    /// </summary>
    public static void LoadAllLocalData() {
        LoadLocalData(Variables);
        LoadLocalData(LocalOnlyVariables);
    }

    public static async Task<LoadResult<T>> LoadData<T>(string key, bool localOnly) => await LoadData<T>(key, localOnly, GetLoadOptions());

    public static async Task<LoadResult<T>> LoadData<T>(string key, bool localOnly, string playerId) => await LoadData<T>(key, localOnly, GetLoadOptions(playerId: playerId));

    public static async Task<LoadResult<T>> LoadData<T>(string key, bool localOnly, LoadOptions options) {
        var keys = new HashSet<string> {key};

        //Check if we are authenticated, if not, we can't load cloud data and will attempt to load local data
        if (Authenticator.IsInitialized && !localOnly) {
            //Cloud load
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys, options);
            return data.TryGetValue(key, out var value) 
                ? new LoadResult<T> {success = true, value = value.Value.GetAs<T>()} 
                : new LoadResult<T> {success = false, value = default};
        } else {
            //Local load
            if (PlayerPrefs.HasKey(key)) {
                var type = typeof(T);
                
                if (type == typeof(float))
                    return new LoadResult<T> {success = true, value = (T) (object) PlayerPrefs.GetFloat(key)};
                if (type == typeof(int))
                    return new LoadResult<T> {success = true, value = (T) (object) PlayerPrefs.GetInt(key)};
                if (type == typeof(string))
                    return new LoadResult<T> {success = true, value = (T) (object) PlayerPrefs.GetString(key)};
                if (type == typeof(bool))
                    return new LoadResult<T> {success = true, value = (T) (object) (PlayerPrefs.GetInt(key) == 1)};
            }

            return new LoadResult<T> {success = false, value = default};
        }
    }

    private static async Task SaveDataInternal(IDictionary<string, object> data) {
        //If we never initialized, we can't save data to cloud
        if (!Authenticator.IsInitialized || !Authenticator.HasInternetConnection) return;
        
        await CloudSaveService.Instance.Data.Player.SaveAsync(data, GetSaveOptions());
        Debug.Log($"Saved data to cloud {string.Join(',', data)}");
    }

    private static void SaveLocalDataInternal(IDictionary<string, object> data) {
        foreach (var (key, value) in data) {
            if (value is float floatValue)
                PlayerPrefs.SetFloat(key, floatValue);
            else if (value is int intValue)
                PlayerPrefs.SetInt(key, intValue);
            else if (value is string stringValue)
                PlayerPrefs.SetString(key, stringValue);
            else if (value is bool boolValue)
                PlayerPrefs.SetInt(key, boolValue ? 1 : 0);
            else
                Debug.LogWarning($"Could not save {key} with value {value}. Only float, int, string and bool are supported.");
        }
        
        Debug.Log($"Saved data locally {string.Join(',', data)}");
    }
    
    /// <summary>
    /// Register the variable to the system.
    /// </summary>
    /// <param name="variable">The variable in question.</param>
    /// <param name="saveFlag">Whether to have it as a cloud or local variable.</param>
    public static void RegisterVariable(ISavable variable, SaveFlag saveFlag) {
        if (saveFlag == SaveFlag.Local)
            LocalOnlyVariables.Add(variable);
        else
            Variables.Add(variable);
    }

    private static LoadOptions GetLoadOptions(bool isPublic = false, string playerId = null) {
        return playerId == null 
            ? isPublic ? PublicLoadOptions : DefaultLoadOptions 
            : new LoadOptions(new PublicReadAccessClassOptions(playerId));
    }
    
    private static SaveOptions GetSaveOptions(bool isPublic = false) => isPublic ? PublicSaveOptions : DefaultSaveOptions;

    public struct LoadResult<T> {
        public bool success;
        public T value;
    }
}

}