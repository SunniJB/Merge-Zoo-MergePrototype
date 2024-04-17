using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserPanel : MonoBehaviour
{
    public Image userImage;
    public TextMeshProUGUI userName;

    private User currentUser;
    private Vector2 imageStartSize;

    private void Awake()
    {
        imageStartSize = userImage.rectTransform.sizeDelta;
    }

    public void OpenUserPanel(User user)
    {
        gameObject.SetActive(true);
        currentUser = user;
        userName.text = user.userName;
        userImage.sprite = user.userSprite;

        userImage.rectTransform.sizeDelta = imageStartSize;
        Tools.CalculateImageSize(userImage, user.userSprite);

    }
    public void SendGift()
    {
        if (currentUser != null)
        {
            Debug.Log("Send a gift to " + currentUser.userName);
        }
    }
    public void Visit()
    {
        if (currentUser != null)
        {
            SceneSwitcher.VisitFriend(currentUser);
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
        currentUser = null;
    }
}
