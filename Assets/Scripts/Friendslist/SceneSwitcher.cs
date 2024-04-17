using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwitcher 
{
    public static User currentFriend;

    public static void VisitFriend(User user)
    {
        currentFriend = user;

        SceneManager.LoadScene("FriendScene");
    }

    public static void LeaveFriend()
    {
        currentFriend = null;
        SceneManager.LoadScene(0);
    }
}
