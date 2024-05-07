using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendScene : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI visitText;
    [SerializeField] private Image userImage;

    private void Awake()
    {
        Profile friend = SceneSwitcher.currentProfile;

        if (friend != null)
        {
            visitText.text = "Visiting " + friend.Name;


            //userImage.sprite = friend.userSprite;
            //Tools.CalculateImageSize(userImage, friend.userSprite);

        }
    }

    public void Leave()
    {
        SceneSwitcher.LeaveFriend();
    }
}
