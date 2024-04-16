using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public ResourceBankScriptableObject ResourceBank;
    public float energy, maxEnergy = 100;

    private void Awake()
    {
        instance = this;
        energy = ResourceBank.energy;
        if (energy == 0)
        {
            energy = maxEnergy;
        }
    }
}
