using System;
using System.Collections;
using System.Collections.Generic;
using tusj.Services;
using UnityEngine;

public class ModifiersManager : MonoBehaviour
{
    public Dictionary<string, float> activeModifiers;

    public void activateBoost(string code)
    {
        string[] splitCode = code.Split(",");
        string boostID = splitCode[0];
        float modifier = 1;
        switch (int.Parse(splitCode[1]))
        {
            case 1: modifier = 1.25f; break;
            case 2: modifier = 1.50f; break;
            case 3: modifier = 2.00f; break;
        }

        activeModifiers.Add(boostID, modifier);

    }
}