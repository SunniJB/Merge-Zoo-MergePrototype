using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Generator : MonoBehaviour, IPointerClickHandler
{
    public Image generator;
    
    public GameObject imagePrefab; // Reference to the prefab of the image you want to generate
    public Transform parentTransform; // Parent transform for the new image

    private void Start()
    {
        //generator.alphaHitTestMinimumThreshold = 0.5f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Generator Clicked");
        // Instantiate a new image from the prefab
        if (GameManager.instance.energy < 1) return;
        Debug.Log("Generated");
        GameObject newImage = Instantiate(imagePrefab, parentTransform);
        GameManager.instance.energy -= 1;
    }
}
