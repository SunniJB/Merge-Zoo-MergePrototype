using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ResourceBankScriptableObject ResourceBank;
    public float energy, maxEnergy = 100, regenTime = 15f;
    private DateTime lastUpdateTime;

    private void Awake()
    {
        // Sets last updated time to current time
        lastUpdateTime = DateTime.Now;

        instance = this;
        energy = ResourceBank.energy;
        if (energy == 0)
        {
            energy = maxEnergy;
        }
    }
    void Update()
    {
        // Calculate the time elapsed since the last update
        TimeSpan elapsedTime = DateTime.Now - lastUpdateTime;
        
        // If energy is less than max energy starts regenerating
        if (energy < maxEnergy)
        {
            // If the elapsed time is greater than or equal to the update interval
            if (elapsedTime.TotalSeconds >= regenTime)
            {
                // Increment the float value
                energy++;

                // Log the updated energy
                Debug.Log("Energy: " + energy);

                // Update the last update time to the current time
                lastUpdateTime = DateTime.Now;
            }
        }
    }
}
