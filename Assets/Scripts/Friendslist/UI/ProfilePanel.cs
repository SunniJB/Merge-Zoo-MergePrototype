using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanel : MonoBehaviour
{
    [Header("Profile container")]
    public Image userImage;
    public TextMeshProUGUI userName;
    public GameObject profileContainer;

    [Header("Delete container")]
    public Image userImageDelete;
    public TextMeshProUGUI userNameDelete;
    public GameObject deleteContainer;

    private Profile currentProfile;
    private Vector2 imageStartSize;


    private void Awake()
    {
        imageStartSize = userImage.rectTransform.sizeDelta;
    }

    public void OpenUserPanel(Profile profile)
    {
        gameObject.SetActive(true);
        ProfileContainer();

        currentProfile = profile;
        userName.text = userNameDelete.text = profile.Name;

        //userImage.sprite = userImageDelete.sprite = profile.userSprite;

        userImage.rectTransform.sizeDelta = userImageDelete.rectTransform.sizeDelta = imageStartSize;
        //Tools.CalculateImageSize(userImage, user.userSprite);

    }
    public void SendGift()
    {
        if (currentProfile != null)
        {
            Debug.Log("Send a gift to " + currentProfile.Name);
        }
    }
    public void Visit()
    {
        if (currentProfile != null)
        {
            SceneSwitcher.VisitFriend(currentProfile);
        }
    }
    public void Delete()
    {
        if(currentProfile != null)
        {
            DeleteContainer();
        }
    }
    public void YesDelete()
    {
        FriendsManager.Instance.DeleteFriend(currentProfile.ID);
        Close();
    }

    public void NoDontDelete()
    {
        ProfileContainer();
    }
    public void Close()
    {
        gameObject.SetActive(false);
        currentProfile = null;
    }

    private void ProfileContainer()
    {
        profileContainer.SetActive(true);
        deleteContainer.SetActive(false);
    }
    private void DeleteContainer()
    {
        profileContainer.SetActive(false);
        deleteContainer.SetActive(true);
    }
}
