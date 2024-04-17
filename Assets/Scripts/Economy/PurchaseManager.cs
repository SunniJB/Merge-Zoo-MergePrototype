using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseManager : MonoBehaviour
{

    public float goldCost;
    public float diamondCost;

    public string actionID;
    public string paramater;

    public Button purchaseButton;
    public TMP_Text goldText;
    public TMP_Text diamondText;

    private void Awake()
    {
        purchaseButton.enabled = CanPurchase();
        goldText.text = goldCost.ToString();
        diamondText.text = diamondCost.ToString();
    }

    public bool CanPurchase()
    {
        if (!CanAfford())
            return false;
        if (!EventLink.DoesActionExist<string>(actionID))
            return false;
        return true;
    }

    public bool CanAfford()
    {
        var resources = EconomyManager.GetResources();

        if (resources.gold < goldCost)
        {
            return false;
        }
        if (resources.diamond < diamondCost)
        {
            return false;
        }
        return true;
    }

    public void StartPurchase()
    {
        ConfirmPurchase();
    }

    public void ConfirmPurchase()
    {
        if (!EconomyManager.RemoveResources(goldCost, diamondCost))
        {
            return;
        }
        EconomyManager.SaveProfileEconomy();
        EventLink.InvokeAction(actionID, paramater);
    }

}