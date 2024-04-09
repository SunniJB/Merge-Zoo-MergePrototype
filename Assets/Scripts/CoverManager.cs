using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CoverManager : MonoBehaviour
{

    private ChangeScene changeScene;

    [SerializeField] private Button startGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        changeScene = GameObject.FindGameObjectWithTag("DDOL").GetComponent<ChangeScene>();

        if (startGameButton != null )
        {
            startGameButton.onClick.AddListener(StartGame);
        }
        if (quitGameButton != null )
        {
            quitGameButton.onClick.AddListener(QuitGame);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartGame()
    {
        changeScene.LoadSceneAsync(1);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
