using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tusj.Services;
using Unity.Services.Friends;
using Unity.Services.Friends.Models;
using UnityEngine;

public class SocialManager : MonoBehaviour
{
    private static SocialManager internalActive;

    public static SocialManager Instance
    {
        get
        {
            if (internalActive == null)
            {
                internalActive = FindObjectOfType<SocialManager>();
            }
            return internalActive;
        }
    }

    public bool useFriends;

    //Event for other scripts to assign to when authenticated
    public Action<List<Profile>> OnRequestRefresh;
    public Action<List<Profile>> OnFriendRefresh;
    public Action<Relationship> OnAdded;

    private IReadOnlyList<Relationship> friends;
    private List<Profile> requestList = new List<Profile>();
    private List<Profile> friendList = new List<Profile>();


    private void Awake()
    {
        if (useFriends)
        {
            Authenticator.OnInitialized += InitializeFriends;
        }
    }

    private async void InitializeFriends()
    {
        //Initialize friends
        await FriendsService.Instance.InitializeAsync();

        //Save the friends relation
        friends = FriendsService.Instance.Friends;
        RefreshList();
    }

    public async Task<Relationship> SendFriendRequest_ID(string memberID)
    {
        if(memberID == "")
        {
            return null;
        }

        Relationship relationship = await FriendsService.Instance.AddFriendAsync(memberID);

        //Debug information about the result of the friend request
        //This will be of type "FriendRequest"
        Debug.Log($"Friend request send to {memberID}. New relationshipstatus is {relationship.Type}");

        OnAdded?.Invoke(relationship);

        return relationship;
    }

    public async void AcceptRequest(string memberID)
    {
        Relationship relationship = await FriendsService.Instance.AddFriendAsync(memberID);

        //Debug information about the result of the friend request
        //This will be of type "Friend"
        Debug.Log($"Friend request accepted from {memberID}. New relationship status is {relationship.Type}");

        RefreshList();
    }
    public async void DeclineRequest(string memberID)
    {
        await FriendsService.Instance.DeleteIncomingFriendRequestAsync(memberID);

        //Delete friend request
        Debug.Log($"Friend request declined from {memberID}.");

        RefreshList();
    }
    public void RefreshList()
    {
        RefreshFriends();
        RefreshRequests();
    }
    public void RefreshFriends()
    {
        friendList.Clear();
        IReadOnlyList<Relationship> friends = FriendsService.Instance.Friends;


        foreach (Relationship f in friends)
        {
            friendList.Add(new Profile(f.Member.Profile.Name, f.Member.Id));
        }

        OnFriendRefresh?.Invoke(friendList);

    }
    public void RefreshRequests()
    {
        requestList.Clear();
        IReadOnlyList<Relationship> requests = FriendsService.Instance.IncomingFriendRequests;

        //Filters
        //1.Blocks

        // Removes any users that are blocked from friend requests
        IReadOnlyList<Relationship> blocks = FriendsService.Instance.Blocks;
        requests = requests.Where(r => !blocks.Any(b => b.Member.Id == r.Member.Id)).ToList();


        //Converts requests from relationships to player profile
        foreach (Relationship r in requests)
        {
            requestList.Add(new Profile(r.Member.Profile.Name, r.Member.Id));
        }

        OnRequestRefresh?.Invoke(requestList);
    }


}
