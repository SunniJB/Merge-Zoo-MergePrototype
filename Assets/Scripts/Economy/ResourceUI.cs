using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{

    public TMP_Text quantityText;
    public Image icon;
    public int resourceID;

    private void Start()
    {
        EconomyManager.Instance.onUpdateEconomy += UpdateResource;
        UpdateResource();
    }

    public void UpdateResource()
    {
        Resource resource = EconomyManager.Instance.resources[resourceID];
        icon.sprite = resource.icon;
        quantityText.text = resource.quantity.ToString();
    }

}
