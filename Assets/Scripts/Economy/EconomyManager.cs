using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EconomyManager
{

    public static Action onUpdateEconomy;

    public static float goldAmmount = 250;
    public static float diamondAmmount = 10;

    public static (float gold,float diamond) GetResources()
    {
        GetProfileEconomy();
        return (goldAmmount, diamondAmmount);
    }

    public static bool RemoveResources(float gold, float diamond)
    {
        if (goldAmmount >= gold &&
            diamondAmmount >= diamond)
        {
            goldAmmount -= gold;
            diamondAmmount -= diamond;
            return true;
        }
        return false;
    }

    public static void UpdateEconomy()
    {
        onUpdateEconomy();
    }

    public static void GetProfileEconomy()
    {
        UpdateEconomy();
    }

    public static void SaveProfileEconomy()
    {
        UpdateEconomy();
    }

}

public enum resourceType
{
    Gold, Diamond
}