using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{

    [SerializeField] private Image loadingPanel;
    [SerializeField] private Text loadingText;
    void Awake()
    {
        loadingPanel.gameObject.SetActive(false);
        SceneManager.activeSceneChanged += HideLoadingPanel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneAsync(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        loadingPanel.gameObject.SetActive(true);
        
    }

    private void HideLoadingPanel(Scene prevScene, Scene curScene)
    {
        loadingPanel.gameObject.SetActive(false);
    }
}
