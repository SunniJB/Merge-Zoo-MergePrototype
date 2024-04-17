using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private TMP_Text questPrint;
    [SerializeField] private GameObject questPanel;
    public void ToggleQuests()
    {
        if (questPanel.activeSelf == false)
        {
            questPanel.SetActive(true);
            string questsToPrint = "";
            QuestManager.instance.GetQuests(out questsToPrint);
            questPrint.text = questsToPrint;
        }
        else
        {
            questPanel.SetActive(false);
        }
    }
}
