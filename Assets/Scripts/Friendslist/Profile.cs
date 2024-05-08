using System;
using UnityEngine;

[Serializable]

public class Profile
{
    [field: SerializeField] public string userName { get; private set; }
    [field: SerializeField] public string ID { get; private set; }

    public Profile(string name, string id)
    {
        userName = name; 
        ID = id;
    }

    public override string ToString()
    {
        return $"{userName}, ID: {ID}";
    }
}
