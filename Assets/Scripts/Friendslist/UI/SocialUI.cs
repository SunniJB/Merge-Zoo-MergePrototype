using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialUI : MonoBehaviour
{
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color activeColor;

    [SerializeField] private ScrollRect[] scrollRects;
    [SerializeField] private ProfilePanel panel;
    [SerializeField] private Image friendButton, requestButton, addButton;
    [SerializeField] private GameObject friendTab, requestTab, addTab;
    [SerializeField] private FriendsUI friendsUI;
    public void OpenSocial()
    {
        gameObject.SetActive(true);
        panel.Close();
        FriendTab();
    }
    public void CloseSocial()
    {
        gameObject.SetActive(false);
    }

    public void FriendTab()
    {
        ResetScrollRects();
        DisableAllTabs();
        friendTab.SetActive(true);
        SetTabColor(friendButton);
    }
    public  void RequestTab()
    {
        ResetScrollRects();
        DisableAllTabs();
        requestTab.SetActive(true);
        SetTabColor(requestButton);
    }
    public void AddTab()
    {
        ResetScrollRects();
        DisableAllTabs();
        addTab.SetActive(true);
        SetTabColor(addButton);
        friendsUI.EmptyFeedback();
    }

    private void DisableAllTabs()
    {
        friendTab.SetActive(false);
        requestTab.SetActive(false);
        addTab.SetActive(false);

        SetTabColor(null);
    }
    private void ResetScrollRects()
    {
        for (int i = 0; i < scrollRects.Length; i++)
        {
            scrollRects[i].verticalNormalizedPosition = 1;
        }
    }

    private void SetTabColor(Image image)
    {
        friendButton.color = inactiveColor;
        requestButton.color = inactiveColor;
        addButton.color = inactiveColor;

        if(image != null)
        {
            image.color = activeColor;
        }
    }
    
}
