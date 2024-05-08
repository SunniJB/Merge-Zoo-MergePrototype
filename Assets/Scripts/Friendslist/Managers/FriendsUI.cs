using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Friends.Models;
using UnityEngine;
using tusj.Services;
using System.Threading.Tasks;
using System.Net;

public class FriendsUI : MonoBehaviour
{
    [Header("References")]
    public SocialUI socialUI;
    public FriendsManager socialManager;
    public TextMeshProUGUI username, userID;

    [Header("Friend")]
    public UIElementFriend friendPrefab;
    public Transform friendContainer;
    public ProfilePanel panel;
    public TextMeshProUGUI numberOfFriends;

    [Header("Request")]
    public UIElementRequest requestPrefab;
    public Transform requestContainer;
    public TextMeshProUGUI numberOfRequests;

    [Header("Request header")]
    public TextMeshProUGUI numberOfRequestsHeader;
    public GameObject circle, message;

    [Header("Add")]
    public TMP_InputField inputField;
    public TextMeshProUGUI feedback;

    private PlayerInfo playerInfo;

    private List<UIElementRequest> requestList = new();
    private List<UIElementFriend> friendList = new();

    private Task<FriendsManager.FriendRequestData> sendRequest;

    private void Awake()
    {
        panel.Close();
        socialUI.CloseSocial();
        Authenticator.OnSignedIn += WriteUser;

        FriendsManager.Instance.OnRequestRefresh += RequestRefresh;
        FriendsManager.Instance.OnFriendRefresh += FriendRefresh;
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
        Debug.Log("Number of friends " + friends.Count);

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

        string pluralFriends = "friends";

        if(friends.Count == 1)
        {
            pluralFriends = "friend";
        }
        numberOfFriends.text = "You have " + friends.Count + " " + pluralFriends; 
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

        string pluralRequests = "requests";
        int count = requests.Count;

        if(count > 0)
        {
            if (count == 1)
            {
                pluralRequests = "request";
            }

            numberOfRequestsHeader.text = count.ToString();
            Circle();
        }
        else
        {
            Message();
        }

        numberOfRequests.text = "You have " + count + " friend " + pluralRequests;
    }
    private void Message()
    {
        message.SetActive(true);
        circle.SetActive(false);
    }
    private void Circle()
    {
        message.SetActive(false);
        circle.SetActive(true);
    }

    private void OnRequestAccept(string id)
    {
        FriendsManager.Instance.AcceptRequest(id);
    }
    private void OnRequestDecline(string id)
    {
        FriendsManager.Instance.DeclineRequest(id);
    }
    #endregion

    #region Add

    public async void Add()
    {
        if(inputField.text == "")
        {
            feedback.text = "Enter a valid profile name";
            return;
        }
        else
        {
            EmptyFeedback();
        }

        if(sendRequest == null || sendRequest.Status != TaskStatus.Running)
        {


            sendRequest = FriendsManager.Instance.SendFriendRequest_ID(inputField.text);
            var friendRequestData = await sendRequest;

            if(friendRequestData.statusCode == HttpStatusCode.Conflict)
            {
                feedback.text = "Already sent request to " + inputField.text;
            }
            else if(friendRequestData.relationship == null)
            {
                feedback.text = "Couldn't send request!";
            }
            else if(friendRequestData.relationship.Type == RelationshipType.Block)
            {
                feedback.text = "User has blocked you!";
            }
            else if(friendRequestData.relationship.Type == RelationshipType.FriendRequest)
            {
                feedback.text = $"Succesfully sent a request to {inputField.text}!";
            }
            else if(friendRequestData.relationship.Type == RelationshipType.Friend)
            {
                feedback.text = "You are already friends with this user";
            }
        }

    }

    public void SetFeedBack(string feedback)
    {
        this.feedback.text = feedback;
    }


    public void EmptyFeedback()
    {
        feedback.text = string.Empty;
    }

    //private void Update()
    //{
    //    Debug.Log(sendRequest?.Status);
    //}
    #endregion
}