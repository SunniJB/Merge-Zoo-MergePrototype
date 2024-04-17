using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    [SerializeField] private int questsToStartWith = 20;
    private int questIndex;
    public List<string> currentQuests;

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
        for (int i = 0; i < questsToStartWith; i++)
        {
            ReceiveNewQuest();
        }
    }

    public void ReceiveNewQuest()
    {
        int randomQuest = Random.Range(1, 3);

        for (int i = 0; i < randomQuest; i++)
        {
            int randomAnimalAmount = Random.Range(1, 4);
            animalsToCollectAmount.Add(randomAnimalAmount);
        }
        
        for (int i = 0; i < randomQuest; i++)
        {
            int randomAnimalType = Random.Range(1, 5);
            animalsToCollect.Add((animalTypes)randomAnimalType);
        }

        switch (randomQuest)
        {
            case 1: currentQuests.Add("Hi! I would like to see " + animalsToCollectAmount[questIndex] + " " + animalsToCollect[questIndex] +" please!");
                questIndex++;
                break;

            case 2: currentQuests.Add("Hi! I would like to see " + animalsToCollectAmount[questIndex] + " " + animalsToCollect[questIndex] +", and " + animalsToCollectAmount[questIndex + 1]
                                      + " "+ animalsToCollect[questIndex + 1] + " please!");
                questIndex += 2;
                break;
            
            case 3: currentQuests.Add("Hi! I would like to see " + animalsToCollectAmount[questIndex] + " " + animalsToCollect[questIndex] +", " + animalsToCollectAmount[questIndex + 1]
                                      + " "+ animalsToCollect[questIndex + 1] + ", and " + animalsToCollectAmount[questIndex + 2] + " " + animalsToCollect[questIndex + 2] + " please!");
                questIndex += 3;
                break;
        }
    }

    public string GetQuests(string questList)
    {
        questList = null;
        for (int i = 0; i < currentQuests.Count; i++)
        {
            questList += currentQuests[i] + "\n";
        }

        return questList;
    }
}
