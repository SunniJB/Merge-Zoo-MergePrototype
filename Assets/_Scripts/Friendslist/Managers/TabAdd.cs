using TMPro;
using Unity.Services.Friends.Models;
using UnityEngine;

public class TabAdd : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Start()
    {
        SocialManager.Instance.OnAdded += CheckStatus;
    }

    public void Add()
    {
        SocialManager.Instance.SendFriendRequest_ID(inputField.text);
    }

    private void CheckStatus(Relationship relationship)
    {
        Debug.Log(relationship.Member.Profile.Name);
    }
}
