using System.Collections.Generic;
using UnityEngine;

public class TabFriends : MonoBehaviour
{
    public UIElementFriend friendPrefab;
    public Transform friendContainer;
    public ProfilePanel panel;

    private void Start()
    {
        SocialManager.Instance.OnFriendRefresh += FriendRefresh;
    }

    private List<UIElementFriend> friendList = new();

    private void FriendRefresh(List<Profile> friends)
    {
        //Remove previous friends
        for (int i = 0; i < friendList.Count; i++)
        {
            Destroy(friendList[i].gameObject);
            friendList.RemoveAt(i);
        }

        //Create a new friend object for each friend

        foreach (Profile playerProfile in friends)
        {
            UIElementFriend friendUIElement = Instantiate(friendPrefab, friendContainer);

            //Set the profile and the events for each button
            friendUIElement.SetProfile(playerProfile, panel);

            friendList.Add(friendUIElement);
        }
    }
}
