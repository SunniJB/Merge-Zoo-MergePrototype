using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLManager : MonoBehaviour
{
    void Awake()
    {
        if (gameObject.tag != "DDOL")
            gameObject.tag = "DDOL";

        if (GameObject.FindGameObjectsWithTag("DDOL").Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
