using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{


    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneAsync(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
    }
}
