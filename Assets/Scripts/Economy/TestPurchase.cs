using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPurchase : MonoBehaviour
{

    private void Awake()
    {
        EventLink.SetAction("testPurchase", (string text) =>
        {
            Debug.Log("you bought: " + text);
        });
    }

}
