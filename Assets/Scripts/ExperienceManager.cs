using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExperienceManager
{
    private static uint exp = 0;
    private static uint tempExp = 0;
    private static uint expLevel = 0;
    public static uint Exp {  get { return exp; } }
    public static uint TempExp { get { return tempExp; } }
    public static uint ExpLevel { get { return expLevel; } }

    private static (string, uint)[] levels = new (string, uint)[11]
    {
        ("Literally who?", 0),
        ("Beginner", 100),
        ("Novice", 200),
        ("Apprentice", 300),
        ("Student", 500),
        ("Undergrad", 700),
        ("Grad Student", 1000),
        ("Teacher", 1400),
        ("Expert", 1800),
        ("Master", 2400),
        ("Legend", 3000)
    };

    public static string UpdateExperiencePoints(uint increase)
    {
        exp = (uint)PlayerPrefs.GetInt("UserExperiencePoints", 0);
        exp += increase;
        PlayerPrefs.SetInt("UserExperiencePoints", (int)exp);
        PlayerPrefs.Save();

        tempExp = exp;
        string msg = "";
        for (int i = 0; i < levels.Length; i++)
        {
            if (tempExp >= levels[i].Item2)
            {
                tempExp -= levels[i].Item2;
                expLevel++;
                msg = $"Level {i}: {levels[i].Item1} | Experience to next level: {tempExp} / {levels[i + 1].Item2}";
            }
            else
            {
                break;
            }
        }
        return msg;
    }
}
