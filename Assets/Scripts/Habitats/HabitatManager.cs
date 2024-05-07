using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class HabitatManager : MonoBehaviour
{
    public Habitat[] habitat;
    public GameObject animal;

    public void Inhabit()
    {
        foreach (var Habitat in habitat)
        {
            if (Habitat.occupiedBy == null)
            {
                Object spawnedAnimal = Instantiate(Habitat, Habitat.gameObject.transform.position, Habitat.gameObject.transform.rotation);

            }
        }
    }
    
}
