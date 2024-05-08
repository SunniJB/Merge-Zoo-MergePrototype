using System;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace tusj.Services {

/// <summary>
/// Class for a cloud save variable. Should only be used in static fields.
/// </summary>
/// <typeparam name="T">The type of data.</typeparam>
public class Savable<T> : ISavable {
    
    public event Action<T> OnDataChanged;

    private readonly string _key;
    private T _value;
    private readonly SaveFlag _saveFlag;

    /// <summary>
    /// Shorthand for <see cref="Read"/> and <see cref="Write"/>.
    /// </summary>
    public T Value {
        get => Read();
        set => Write(value);
    }

    /// <summary>
    /// Constructor for a savable variable.
    /// </summary>
    /// <param name="key">The key that it will be labeled with in the cloud. DO NOT CHANGE THIS AFTER CREATION!</param>
    /// <param name="defaultValue">Default value to set if user does not have this variable in the cloud already.</param>
    /// <param name="saveFlag">Which mode this savable uses.</param>
    public Savable(string key, T defaultValue = default, SaveFlag saveFlag = SaveFlag.Cloud) {
        _key = key;
        _value = defaultValue;
        _saveFlag = saveFlag;
        Setup();
    }
    
    /// <summary>
    /// Set the value of the variable. If you want to read the value, use <see cref="Read"/>.
    /// This will not save the data to the cloud immediately, use <see cref="ManualSave"/>
    /// </summary>
    /// <param name="data">Data to write.</param>
    public void Write(T data) {
        _value = data;
        OnDataChanged?.Invoke(_value);
    }
    
    /// <summary>
    /// Read the value of the variable. If you want to set the value, use <see cref="Write"/>.
    /// </summary>
    /// <returns>Returns the current value.</returns>
    public T Read() => _value;
    
    /// <summary>
    /// Trigger a manual save to the cloud.
    /// </summary>
    public async Task ManualSave() => await CloudSave.SaveData(this, _saveFlag == SaveFlag.Local);

    void ISavable.Write(object data) {
        try {
            var castedData = Convert.ChangeType(data, typeof(T));
            Write((T) castedData);
        }
        catch {
            Debug.Log($"Could not convert to {typeof(T)}");
        }
    }
    
    object ISavable.Read() => Read();

    string ISavable.GetKey() => _key;

    private async void Setup() {
        CloudSave.RegisterVariable(this, _saveFlag);

        //If we are already initialized, we have to load the data ourselves
        if (Authenticator.IsInitialized) {
            Debug.Log($"Performing post-init load for key [{_key}]");
            var cloudValue = await CloudSave.LoadData<T>(_key, _saveFlag == SaveFlag.Local);
            if (cloudValue.success)
                Write(cloudValue.value);
            else
                Debug.LogWarning($"Could not load data for key [{_key}]");
        }
    }

}

public enum SaveFlag {
    /// <summary>
    /// Local variables are saved on the device itself, and do not require internet connection.
    /// </summary>
    Local,
    /// <summary>
    /// Cloud variables are saved on the cloud, and require internet connection. These are also cached locally.
    /// </summary>
    Cloud
}

}