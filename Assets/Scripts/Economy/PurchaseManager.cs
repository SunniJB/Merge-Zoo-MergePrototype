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

    public Image purchaseImage;

    public Sprite purchaseSprite;

    public string purchaseTitle;
    public string purchaseDescription;

    public ConfirmPurchaseWindowUIManager confirmPurchaseWindowUIManager;

    private void Awake()
    {
        purchaseButton.onClick.AddListener(StartPurchase);

        if (goldCost > 0)
        {
            goldText.text = goldCost.ToString();
        }

        if (diamondCost > 0)
        {
            diamondText.text = diamondCost.ToString();
        }

        purchaseImage.sprite = purchaseSprite;
    }

    public void StartPurchase()
    {
        confirmPurchaseWindowUIManager.openWindow(ConfirmPurchase, goldCost, diamondCost, purchaseTitle,
            purchaseDescription, purchaseSprite, CanPurchase());
    }

    public void ConfirmPurchase()
    {
        if (!EconomyManager.RemoveResources(goldCost, diamondCost))
        {
            Debug.Log("Resources not removed");
            return;
        }
        EconomyManager.SaveProfileEconomy();
        EventLink.InvokeAction(actionID, paramater);
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

}