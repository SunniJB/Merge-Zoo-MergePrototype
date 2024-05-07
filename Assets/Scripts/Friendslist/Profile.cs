using System;
using UnityEngine;

[Serializable]

public class Profile
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string ID { get; private set; }

    public Profile(string name, string id)
    {
        Name = name; 
        ID = id;
    }

    public override string ToString()
    {
        return $"{Name}, ID: {ID}";
    }
}
