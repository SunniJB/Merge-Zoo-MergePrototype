using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models.Data.Player;
using UnityEngine;
using SaveOptions = Unity.Services.CloudSave.Models.Data.Player.SaveOptions;

namespace tusj.Services {

public static class CloudSave {
    
    private static readonly SaveOptions PublicSaveOptions = new(new PublicWriteAccessClassOptions());
    private static readonly LoadOptions PublicLoadOptions = new(new PublicReadAccessClassOptions());
    
    private static readonly List<ISavable> Variables = new();

    public static void GetPublicData() {
        const string playerId = "1Uw5Dzm67NrxqMlFl04nI0ffBlWO"; //Edvart
        // LoadData(GetLoadOptions(playerId));
    }
    
    public static async void SaveData() {
        var data = new Dictionary<string, object>();

        foreach (var savable in Variables)
            data.Add(savable.GetKey(), savable.Read());

        SaveDataInternal(data);
    }

    public static async void SaveData(ISavable savable) {
        var data = new Dictionary<string, object> {
            {savable.GetKey(), savable.Read()}
        };
        
        SaveDataInternal(data);
    }
    
    public static async Task<LoadResult<T>> LoadData<T>(string key, LoadOptions options = null) {
        var keys = new HashSet<string> {key};
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys, PublicLoadOptions);
        return data.TryGetValue(key, out var value) 
            ? new LoadResult<T> {success = true, value = value.Value.GetAs<T>()} 
            : new LoadResult<T> {success = false, value = default};
    }

    private static async void SaveDataInternal(IDictionary<string, object> data) {
        await CloudSaveService.Instance.Data.Player.SaveAsync(data, PublicSaveOptions);
        Debug.Log($"Saved data {string.Join(',', data)}");
    }
    
    public static void RegisterVariable(ISavable variable) => Variables.Add(variable);

    private static LoadOptions GetLoadOptions(string playerId = null) {
        return playerId == null 
            ? PublicLoadOptions 
            : new LoadOptions(new PublicReadAccessClassOptions(playerId));
    }
    
    private static SaveOptions GetSaveOptions() => PublicSaveOptions;

    public struct LoadResult<T> {
        public bool success;
        public T value;
    }
}

}