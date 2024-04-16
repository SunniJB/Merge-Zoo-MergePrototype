using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public enum animalTypes
    {
        EGG,
        EGGS,
        TADPOLE,
        FROG,
        TOAD
    }

    public List<animalTypes> animalsToCollect;
    public List<int> animalsToCollectAmount;
    private void Awake()
    {
        instance = this;
        ReceiveNewQuest();
    }

    public void ReceiveNewQuest()
    {
        int randomQuest = Random.Range(1, 3);

        for (int i = 0; i < randomQuest; i++)
        {
            animalsToCollectAmount.Add(Random.Range(1,4));
        }
        
        for (int i = 0; i < randomQuest; i++)
        {
            animalsToCollect.Add((animalTypes)Random.Range(1,5));
        }

        switch (randomQuest)
        {
            case 1: Debug.Log("Hi! I would like to see " + animalsToCollectAmount[0] + " " + animalsToCollect[0] +" please!");
                break;
            
            case 2: Debug.Log("Hi! I would like to see " + animalsToCollectAmount[0] + " " + animalsToCollect[0] +", and " + animalsToCollectAmount[1]
                + " "+ animalsToCollect[1] + " please!");
                break;
            
            case 3: Debug.Log("Hi! I would like to see " + animalsToCollectAmount[0] + " " + animalsToCollect[0] +", " + animalsToCollectAmount[1]
                + " "+ animalsToCollect[1] + ", and " + animalsToCollectAmount[2] + " " + animalsToCollect[2] + " please!");
                break;
        }

    }
}
