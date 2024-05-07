using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIElementFriend : MonoBehaviour
{
    public Image userImage;
    public TextMeshProUGUI userName;
    public Sprite backupSprite;

    private Profile playerProfile;
    private ProfilePanel panel;

    public void SetProfile(Profile profile, ProfilePanel panel)
    {
        playerProfile = profile;
        userName.text = profile.Name;


        //Image, some time in the future
        //userImage.sprite = user.userSprite;
        //Tools.CalculateImageSize(userImage, user.userSprite);
        


        this.panel = panel;
    }


    public void OpenPanel()
    {
        if(panel != null)
        {
            panel.OpenUserPanel(playerProfile);
        }
    }
}
