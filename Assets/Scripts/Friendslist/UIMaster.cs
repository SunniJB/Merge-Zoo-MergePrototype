using System.Collections.Generic;
using UnityEngine;

public class UIMaster : MonoBehaviour
{
    public SocialUI socialUI;
    public UserPanel userPanel;

    public List<User> users;

    private void Awake()
    {
        userPanel.Close();
        socialUI.PopulateUserList(users);
        socialUI.CloseSocial();
    }
}
