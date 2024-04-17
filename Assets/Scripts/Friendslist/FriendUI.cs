using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI visitText;
    [SerializeField] private Image userImage;

    private void Awake()
    {
        User friend = SceneSwitcher.currentFriend;

        if (friend != null)
        {
            visitText.text = "Visiting " + friend.userName;
            userImage.sprite = friend.userSprite;
            Tools.CalculateImageSize(userImage, friend.userSprite);

        }
    }

    public void Leave()
    {
        SceneSwitcher.LeaveFriend();
    }
}
