using System.Collections.Generic;
using UnityEngine;

public class TabRequest : MonoBehaviour
{
    public UIElementRequest requestPrefab;
    public Transform requestContainer;

    private List<UIElementRequest> requestList = new();

    private void Start()
    {
        SocialManager.Instance.OnRequestRefresh += RequestRefresh;
    }


    private void RequestRefresh(List<Profile> requests)
    {
        Debug.Log(requests.Count + "Count");
        //Remove previous requests
        for(int i = 0; i < requestList.Count; i++)
        {
            Destroy(requestList[i].gameObject);
            requestList.RemoveAt(i);
        }

        //Create a new request object for each request

        foreach(Profile playerProfile in requests)
        {
            UIElementRequest requestUIElement = Instantiate(requestPrefab, requestContainer);

            //Set the profile and the events for each button
            requestUIElement.SetProfile(playerProfile, OnRequestAccept, OnRequestDecline);

            requestList.Add(requestUIElement);
        }

        Debug.Log("UpdatingList");
    }

    private void OnRequestAccept(string id)
    {
        SocialManager.Instance.AcceptRequest(id);
    }
    private void OnRequestDecline(string id)
    {
        SocialManager.Instance.DeclineRequest(id);
    }
}
