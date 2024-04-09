using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (gameObject.tag != "DDOL")
            gameObject.tag = "DDOL";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
