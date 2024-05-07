using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class HabitatManager : MonoBehaviour
{
    public Habitat[] habitat;
    public GameObject animal;

    private void Start()
    {
        Inhabit();
    }

    public void Inhabit()
    {
        foreach (var Habitat in habitat)
        {
            if (Habitat.occupiedBy == null)
            {
                Instantiate(Habitat, Habitat.gameObject.transform.position, Habitat.gameObject.transform.rotation);

            }
        }
    }
    
}
