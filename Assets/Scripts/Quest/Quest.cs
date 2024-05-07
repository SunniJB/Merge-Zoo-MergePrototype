using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string questText;
    public animalTypes animalsTypeCollect;
    public int animalsToCollectAmount;

    [System.Serializable]
    public enum animalTypes
    {
        EGG,
        EGGS,
        TADPOLE,
        FROG,
        TOAD
    }
}
