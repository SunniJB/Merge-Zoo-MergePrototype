using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserUIElement : MonoBehaviour
{
    public Image userImage;
    public TextMeshProUGUI userName;
    public Sprite backupSprite;

    private User currentUser;
    private UserPanel panel;

    public void NewUser(User user, UserPanel panel)
    {
        currentUser = user;
        this.panel = panel;
        userName.text = user.userName;
        userImage.sprite = user.userSprite;

        Tools.CalculateImageSize(userImage, user.userSprite);
        
    }


    public void OpenPanel()
    {
        if(panel != null)
        {
            panel.OpenUserPanel(currentUser);
        }
    }
}
