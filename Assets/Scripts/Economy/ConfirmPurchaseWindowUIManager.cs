using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class ConfirmPurchaseWindowUIManager : MonoBehaviour
{

    public TMP_Text goldCostText, diamondCostText, purchaseTitleText, purchaseDescriptionText;

    public Image purchaseImage;

    public Button confirmButton, cancelButton;

    public GameObject window;

    private void Awake()
    {
        cancelButton.onClick.AddListener(cancel);
    }

    public void cancel()
    {
        window.SetActive(false);
    }

    public void openWindow(UnityAction confirmMethod, float goldCost, float diamondCost, string purchaseTitle,
        string purchaseDescription, Sprite purchaseImage, bool canPurchase)
    {
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(confirmMethod);
        confirmButton.onClick.AddListener(cancel);

        if (goldCost > 0)
        {
            goldCostText.text = goldCost.ToString();
        }

        if (diamondCost > 0)
        {
            diamondCostText.text = diamondCost.ToString();
        }

        purchaseTitleText.text = purchaseTitle;
        purchaseDescriptionText.text = purchaseDescription;

        this.purchaseImage.sprite = purchaseImage;

        confirmButton.interactable = canPurchase;

        window.SetActive(true);




    }

}
