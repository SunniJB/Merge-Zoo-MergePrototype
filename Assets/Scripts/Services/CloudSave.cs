﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models.Data.Player;
using UnityEngine;
using SaveOptions = Unity.Services.CloudSave.Models.Data.Player.SaveOptions;

namespace tusj.Services {

public static class CloudSave {

    public static event Action OnPreSave;
    
    private static readonly SaveOptions PublicSaveOptions = new(new PublicWriteAccessClassOptions());
    private static readonly SaveOptions DefaultSaveOptions = new(new DefaultWriteAccessClassOptions());
    private static readonly LoadOptions PublicLoadOptions = new(new PublicReadAccessClassOptions());
    private static readonly LoadOptions DefaultLoadOptions = new(new DefaultReadAccessClassOptions());
    
    private static readonly List<ISavable> Variables = new();

    //This needs to be beneath the variables
    public static readonly Savable<float> SaveTimestamp = new("SaveTimestamp", float.MinValue);
    
    public static async Task SaveAllData() {
        //Pre save tasks
        OnPreSave?.Invoke();
        SaveTimestamp.Write(DateTime.Now.Second);
        
        var data = new Dictionary<string, object>();

        foreach (var savable in Variables)
            data.Add(savable.GetKey(), savable.Read());

        await SaveDataInternal(data);
        await SaveLocalDataInternal(data);
        Variables.Clear();
    }

    public static async Task SaveData(ISavable savable) {
        var data = new Dictionary<string, object> {
            {savable.GetKey(), savable.Read()}
        };
        
        await SaveDataInternal(data);
    }

    public static async Task LoadAllData() {
        var keys = new HashSet<string>();

        foreach (var variable in Variables)
            keys.Add(variable.GetKey());
        
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys, GetLoadOptions());

        foreach (var variable in Variables)
            if (data.TryGetValue(variable.GetKey(), out var value))
                variable.Write(value.Value.GetAs<object>());
    }

    public static async Task<LoadResult<T>> LoadData<T>(string key) {
        return await LoadData<T>(key, GetLoadOptions());
    }
    
    public static async Task<LoadResult<T>> LoadData<T>(string key, string playerId) {
        return await LoadData<T>(key, GetLoadOptions(playerId: playerId));
    }
        
    public static async Task<LoadResult<T>> LoadData<T>(string key, LoadOptions options) {
        var keys = new HashSet<string> {key};
        
        var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys, options);
        return data.TryGetValue(key, out var value) 
            ? new LoadResult<T> {success = true, value = value.Value.GetAs<T>()} 
            : new LoadResult<T> {success = false, value = default};
    }

    private static async Task SaveDataInternal(IDictionary<string, object> data) {
        await CloudSaveService.Instance.Data.Player.SaveAsync(data, GetSaveOptions());
        Debug.Log($"Saved data {string.Join(',', data)}");
    }

    private static async Task SaveLocalDataInternal(IDictionary<string, object> data) {
        //Local save
    }
    
    public static void RegisterVariable(ISavable variable) {
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