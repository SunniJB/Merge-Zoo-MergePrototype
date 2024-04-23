using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;

namespace tusj.Services {

public class Authenticator : MonoBehaviour {

    public static event Action OnInitialized;
    public static event SignedInCallback OnSignedIn;
    public static event Action OnSigningOut;
    
    public static bool IsInitialized { get; private set; }
    public static PlayerInfo Info { get; private set; }
    public static string PlayerName { get; private set; }
    
    public static string AccessToken => PlayerAccountService.Instance.AccessToken;
    public static string PlayerId => Info?.Id;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init() => DontDestroyOnLoad(Instantiate(Resources.Load("Authenticator")));
    
    private void Awake() {
        gameObject.hideFlags = HideFlags.NotEditable;

        Initialize();
    }
    
    private void OnDestroy() => PlayerAccountService.Instance.SignedIn -= OnPlayerSignedIn;

    private void OnApplicationQuit() {
        CloudSave.SaveData();
    }

    private static async void Initialize() {
        //UGS initialization
        await UnityServices.InitializeAsync();
        
        if (AuthenticationService.Instance.SessionTokenExists) {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Signed in cached user");
            await HandleOnSingIn();
        } else {
            PlayerAccountService.Instance.SignedIn += OnPlayerSignedIn;
            await PlayerAccountService.Instance.StartSignInAsync();
        }
    }
    
    private static async void OnPlayerSignedIn() {
        try {
            if (!AuthenticationService.Instance.IsSignedIn) {
                await AuthenticationService.Instance.SignInWithUnityAsync(AccessToken);
                Debug.Log("Signed in with Unity");
            }

            await HandleOnSingIn();
        }
        catch (AuthenticationException e) {
            Debug.LogException(e);
        }
        catch (RequestFailedException e) {
            Debug.LogException(e);
        }
    }

    private static async Task HandleOnSingIn() {
        //Properties are cached, so we can get them immediately, but they could be null. In that case, we fetch them.
        Info = AuthenticationService.Instance.PlayerInfo ?? await AuthenticationService.Instance.GetPlayerInfoAsync();
        PlayerName = AuthenticationService.Instance.PlayerName ?? await AuthenticationService.Instance.GetPlayerNameAsync();
        OnSignedIn?.Invoke(Info, PlayerName);

        //Try to load all registered variables
        await CloudSave.LoadAllData();
        IsInitialized = true;
        OnInitialized?.Invoke();
    }
    
    private static async void UpdateName(string name) {
        try {
            var newName = await AuthenticationService.Instance.UpdatePlayerNameAsync(name);
            PlayerName = newName;
        }
        catch (AuthenticationException e) {
            Debug.LogException(e);
        }
    }
    
    //Dunno if im gonna use this
    private string GetSessionToken() {
        var sessionToken = PlayerPrefs.GetString($"{Application.cloudProjectId}.{AuthenticationService.Instance.Profile}.unity.services.authentication.session_token");
        return sessionToken;
    }

    public delegate void SignedInCallback(PlayerInfo info, string name);
}

}