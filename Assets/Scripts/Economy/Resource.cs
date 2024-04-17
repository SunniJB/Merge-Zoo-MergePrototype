using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Resource
{

    public string name;
    public string description;
    public float quantity;
    public Sprite icon;

    public void SetQuantity(float newQuantity)
    {
        quantity = newQuantity;
    }

}
