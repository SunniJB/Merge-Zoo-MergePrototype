using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    /// <summary>
    /// TODO: Replace the "highestPossibleAnimalType" variable with a more dynamic solution.
    /// Replace the "animalTypes" enum in the Quest.cs script with a more dynamic solution.
    /// </summary>
    
    public static QuestManager instance;
    
    [SerializeField] private int questsToStartWith = 20;
    public List<Quest> CurrentQuests = new List<Quest>();
    
    private int highestPossibleAnimalType = 2;

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < questsToStartWith; i++)
        {
            ReceiveNewQuest();
        }
        
        PrintQuests();
    }

    public void ReceiveNewQuest()
    {
        Quest newQuest = new Quest();

        newQuest.animalsToCollectAmount = Random.Range(1, 3);
        newQuest.animalsTypeCollect = (Quest.animalTypes)Random.Range(0, Enum.GetValues(typeof(Quest.animalTypes)).Length);
        newQuest.questText = "Collect " + newQuest.animalsToCollectAmount + " " +
                                       newQuest.animalsTypeCollect;
        
        CurrentQuests.Add(newQuest);
    }

    public void CompleteQuest(Quest questToComplete)
    {
        CurrentQuests.Remove(questToComplete);
    }

    public List<Quest> GetQuests()
    {
        List<Quest> questList = new List<Quest>();
        for (int i = 0; i < CurrentQuests.Count; i++)
        {
            questList.Add(CurrentQuests[i]);
        }

        return questList;
    }

    public string PrintQuests()
    {
        StringBuilder quests = new StringBuilder();
        
        for (int i = 0; i < CurrentQuests.Count; i++)
        {
            quests.AppendLine(CurrentQuests[i].questText);
        }

        return quests.ToString();
    }
}
