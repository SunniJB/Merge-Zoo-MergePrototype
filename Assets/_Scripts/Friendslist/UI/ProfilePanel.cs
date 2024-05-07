using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanel : MonoBehaviour
{
    public Image userImage;
    public TextMeshProUGUI userName;

    private Profile currentProfile;
    private Vector2 imageStartSize;

    private void Awake()
    {
        imageStartSize = userImage.rectTransform.sizeDelta;
    }

    public void OpenUserPanel(Profile profile)
    {
        gameObject.SetActive(true);
        currentProfile = profile;
        userName.text = profile.Name;

        //userImage.sprite = user.userSprite;

        userImage.rectTransform.sizeDelta = imageStartSize;
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
    public void Close()
    {
        gameObject.SetActive(false);
        currentProfile = null;
    }
}
