using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitcher 
{
    public static Profile currentProfile;

    public static void VisitFriend(Profile profile)
    {
        currentProfile = profile;

        SceneManager.LoadScene("FriendScene");
    }

    public static void LeaveFriend()
    {
        currentProfile = null;
        SceneManager.LoadScene(0);
    }
}
