using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class Analytics : MonoBehaviour
{
    // Start is called before the first frame update
   async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            GiveConsent();
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }
    
    private void GiveConsent() //Starts the data collection, should be a UI element that the player has to press
    {
        AnalyticsService.Instance.StartDataCollection();

        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }
}
