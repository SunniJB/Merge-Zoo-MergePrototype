using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class HoldingCell : MonoBehaviour
{

    public GameObject frogPrefab;
    public Container[] holdingCells;

    private void Awake()
    {
        EventLink.SetAction<string>("PurchaseAnimal", GeneratePurchase);
    }

    public void GeneratePurchase(string code)
    {
        GameObject usedPrefab = null;
        int generatorLevel = 0;

        if (code[0] == 'F')
        {
            usedPrefab = frogPrefab;
        }

        generatorLevel = int.Parse(code[1].ToString());

        foreach (var cell in holdingCells)
        {
            if (cell.currentItem == null)
            {
                GameObject spawnedResource = Instantiate(usedPrefab, cell.gameObject.transform.position, cell.gameObject.transform.rotation);
                cell.currentItem = spawnedResource;

                Mergable mergable = spawnedResource.GetComponent<Mergable>();
                mergable.lastContainer = cell.gameObject;
                mergable.resourceLevel = generatorLevel;
                mergable.GetComponent<SpriteRenderer>().sprite = mergable.sprites[generatorLevel];

                return;
            }
        }
    }

}
