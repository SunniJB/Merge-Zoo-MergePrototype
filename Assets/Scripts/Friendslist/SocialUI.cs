using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialUI : MonoBehaviour
{
    [SerializeField] private float moveDuration = 0.1f;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color activeColor;

    [SerializeField] private UserUIElement userUIElement;
    [SerializeField] private UserPanel userPanel;
    [SerializeField] private Sprite backupSprite;
    [SerializeField] private ScrollRect[] scrollRects;
    [SerializeField] private UIButton friendButton, requestButton, addButton;
    [SerializeField] private GameObject friendTab, requestTab, addTab;
    [SerializeField] private Transform friendList;


    public void PopulateUserList(List<User> users)
    {
        for(int i = 0; i < friendList.childCount; i++)
        {
            Destroy(friendList.GetChild(i).gameObject);
        }
        for (int i = 0; i < users.Count; i++)
        {
            UserUIElement element = Instantiate(userUIElement, friendList);

            if (users[i].userSprite == null)
            {
                users[i].userSprite = backupSprite;
            }

            element.NewUser(users[i], userPanel);
        }
    }


    public void OpenSocial()
    {
        gameObject.SetActive(true);
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
        friendButton.SetBackgroundColor(activeColor);
    }
    public  void RequestTab()
    {
        ResetScrollRects();
        DisableAllTabs();
        requestTab.SetActive(true);
        requestButton.SetBackgroundColor(activeColor);
    }
    public void AddTab()
    {
        ResetScrollRects();
        DisableAllTabs();
        addTab.SetActive(true);
        addButton.SetBackgroundColor(activeColor);
    }

    private void DisableAllTabs()
    {
        friendTab.SetActive(false);
        requestTab.SetActive(false);
        addTab.SetActive(false);

        friendButton.SetBackgroundColor(inactiveColor);
        requestButton.SetBackgroundColor(inactiveColor);
        addButton.SetBackgroundColor(inactiveColor);
    }
    private void ResetScrollRects()
    {
        for (int i = 0; i < scrollRects.Length; i++)
        {
            scrollRects[i].verticalNormalizedPosition = 1;
        }
    }

    
}
