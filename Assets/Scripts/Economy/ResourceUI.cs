using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{

    public TMP_Text quantityText;
    public resourceType resourceType;

    private void Start()
    {
        EconomyManager.onUpdateEconomy += UpdateResource;
        UpdateResource();
    }

    public void UpdateResource()
    {
        switch (resourceType)
        {
            case resourceType.Gold:
                quantityText.text = EconomyManager.goldAmmount.ToString();
                break;
            case resourceType.Diamond:
                quantityText.text = EconomyManager.diamondAmmount.ToString();
                break;
        }
    }

}
