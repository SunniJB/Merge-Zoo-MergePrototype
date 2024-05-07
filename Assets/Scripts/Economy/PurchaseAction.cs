using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PurchaseAction : MonoBehaviour
{

    public UnityEvent OnPurchase;
    public string actionID;

    private void Awake()
    {
        //set up the action using eventlink, you can copy this to set up an action anywhere in code.
        //should be done on awake
        EventLink.SetAction<string>(actionID, DoAction);
    }

    //Paramater is ignored as this is a simplified way of using eventlink
    public void DoAction(string paramater)
    {
        OnPurchase.Invoke();
    }

}
