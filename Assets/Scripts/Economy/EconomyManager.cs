using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EconomyManager : MonoBehaviour
{

    public Action onUpdateEconomy;

    public List<Resource> resources = new List<Resource>();

    private static EconomyManager instance;
    public static EconomyManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }

    }

    public void ChangeResource(int resourceID, int ammount)
    {
        resources[resourceID].SetQuantity(resources[resourceID].quantity - ammount);

    }

    private void Update()
    {
        UpdateEconomy();
    }

    public void UpdateEconomy()
    {
        onUpdateEconomy();
    }

    public void GetProfileEconomy()
    {
        UpdateEconomy();
    }

    public void SaveProfileEconomy()
    {
        UpdateEconomy();
    }

}
