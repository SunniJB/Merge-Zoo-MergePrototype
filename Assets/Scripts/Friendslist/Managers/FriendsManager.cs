using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tusj.Services;
using Unity.Services.Friends;
using Unity.Services.Friends.Exceptions;
using Unity.Services.Friends.Models;
using UnityEngine;
using System.Net;

public class FriendsManager : MonoBehaviour
{
    private static FriendsManager internalActive;

    public static FriendsManager Instance
    {
        get
        {
            if (internalActive == null)
            {
                internalActive = FindObjectOfType<FriendsManager>();
            }
            return internalActive;
        }
    }

    public FriendsUI friendsUI;

    //Event for other scripts to assign to when authenticated
    public Action<List<Profile>> OnRequestRefresh;
    public Action<List<Profile>> OnFriendRefresh;

    private IReadOnlyList<Relationship> friends;
    private List<Profile> requestList = new List<Profile>();
    private List<Profile> friendList = new List<Profile>();


    private void Awake()
    {
        if (Authenticator.IsInitialized)
        {
            InitializeFriends();
        }
        else
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
        SubscribeToFriendsEventCallbacks();
    }

    public async Task<FriendRequestData> SendFriendRequest_ID(string username)
    {
        List<Relationship> friendRequest = FriendRequests();

        if (friendRequest.Count >= 10)
        {
            //Deleting the oldest friend request
            await FriendsService.Instance.DeleteOutgoingFriendRequestAsync(friendRequest[0].Member.Id);
        }


        try
        {
            //We add the friend by name in this sample but you can also add a friend by ID using AddFriendAsync
            Relationship relationship = await FriendsService.Instance.AddFriendByNameAsync(username);

            Debug.Log($"Friend request send to {username}. New relationshipstatus is {relationship.Type}");
            //If both players send friend request to each other, their relationship is changed to Friend.


            return new FriendRequestData
            {
                relationship = relationship,
            };
        }
        catch (FriendsServiceException e)
        {
            if(e.StatusCode == HttpStatusCode.Conflict)
            {
                return new FriendRequestData
                {
                    statusCode = e.StatusCode,
                };
            }


            Debug.Log("Failed to Request " + username + " - " + e.StatusCode + ", Message: " + e.Message);
            
            return new FriendRequestData
            {
                relationship = null,
            };
        }

    }

    private List<Relationship> FriendRequests()
    {
        IReadOnlyList<Relationship> allRelationships = FriendsService.Instance.Relationships;

        List<Relationship> friendRequests = new();

        for(int i = 0; i < allRelationships.Count; i++)
        {
            if (allRelationships[i].Type == RelationshipType.FriendRequest)
            {
                friendRequests.Add(allRelationships[i]);
            }
        }

        return friendRequests;
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

    public async void DeleteFriend(string memberID)
    {
        await FriendsService.Instance.DeleteFriendAsync(memberID);
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
    private void SubscribeToFriendsEventCallbacks()
    {
        try
        {
            FriendsService.Instance.RelationshipAdded += e =>
            {
                Debug.Log($"Create {e.Relationship} event received");
                RefreshList();
            };
            FriendsService.Instance.MessageReceived += e =>
            {
                Debug.Log("Message received, event received");
            };
            FriendsService.Instance.PresenceUpdated += e =>
            {
                Debug.Log("Presence updated, event received");
            };
            FriendsService.Instance.RelationshipDeleted += e =>
            {
                Debug.Log($"Delete {e.Relationship}, event received");
            };
        }
        catch (FriendsServiceException e)
        {
            Debug.Log($"An error occured while performing the action. Code: {e.StatusCode}, message: {e.Message}.");
        }
    }


    public struct FriendRequestData
    {
        public Relationship relationship;
        public HttpStatusCode statusCode;
    }

}
