using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIElementRequest : MonoBehaviour
{

    public TextMeshProUGUI username;
    private Profile profile;

    //Add image in the future
    public Image profilePicure;
    
    private Action <string> OnAccept;
    private Action <string> OnDecline;

    public void SetProfile(Profile profile, Action<string> onAccept, Action<string> onDecline)
    {
        this.profile = profile;
        username.text = profile.userName;

        OnAccept = onAccept;
        OnDecline = onDecline;
    }

    public void Accept()
    {
        OnAccept?.Invoke(profile.ID);
    }
    public void Decline()
    {
        OnDecline?.Invoke(profile.ID);
    }
}
