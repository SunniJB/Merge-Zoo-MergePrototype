using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Friends.Models;
using UnityEngine;
using tusj.Services;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public SocialUI socialUI;
    public SocialManager socialManager;
    public TextMeshProUGUI username, userID;

    [Header("Friend")]
    public UIElementFriend friendPrefab;
    public Transform friendContainer;
    public ProfilePanel panel;

    [Header("Request")]
    public UIElementRequest requestPrefab;
    public Transform requestContainer;

    [Header("Add")]
    public TMP_InputField inputField;

    private PlayerInfo playerInfo;

    private List<UIElementRequest> requestList = new();
    private List<UIElementFriend> friendList = new();

    private Task<Relationship> sendRequest;

    private void Awake()
    {
        panel.Close();
        socialUI.CloseSocial();
        Authenticator.OnSignedIn += WriteUser;

        SocialManager.Instance.OnRequestRefresh += RequestRefresh;
        SocialManager.Instance.OnAdded += CheckStatus;
        SocialManager.Instance.OnFriendRefresh += FriendRefresh;
    }
    private void WriteUser(PlayerInfo info, string name)
    {
        playerInfo = info;
        username.text = name;
        userID.text = playerInfo.Id;
    }

    #region Friend
    private void FriendRefresh(List<Profile> friends)
    {
        Debug.Log(friends.Count);

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
    #endregion

    #region Request
    private void RequestRefresh(List<Profile> requests)
    {
        //Remove previous requests
        for (int i = 0; i < requestList.Count; i++)
        {
            Destroy(requestList[i].gameObject);
            requestList.RemoveAt(i);
        }

        //Create a new request object for each request

        foreach (Profile playerProfile in requests)
        {
            UIElementRequest requestUIElement = Instantiate(requestPrefab, requestContainer);

            //Set the profile and the events for each button
            requestUIElement.SetProfile(playerProfile, OnRequestAccept, OnRequestDecline);

            requestList.Add(requestUIElement);
        }

        Debug.Log("Updating Request List");
    }

    private void OnRequestAccept(string id)
    {
        SocialManager.Instance.AcceptRequest(id);
    }
    private void OnRequestDecline(string id)
    {
        SocialManager.Instance.DeclineRequest(id);
    }
    #endregion

    #region Add

    public async void Add()
    {
        if(sendRequest == null || sendRequest.Status != TaskStatus.Running)
        {
            sendRequest = SocialManager.Instance.SendFriendRequest_ID(inputField.text);
            Relationship relationship = await sendRequest;
        }

    }
    private void Update()
    {
        Debug.Log(sendRequest?.Status);
    }
    private void CheckStatus(Relationship relationship)
    {
        Debug.Log(relationship.Member.Profile.Name);
    }

    #endregion
}