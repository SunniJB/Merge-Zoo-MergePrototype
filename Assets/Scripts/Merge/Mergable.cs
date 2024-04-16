using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mergable : MonoBehaviour
{
    private bool dragging = false;
    private float distance;
    private Vector3 startDist;
    public LayerMask hitlayer;
    public Camera mergeCam;

    public int resourceLevel;
    public Sprite[] sprites;

    public GameObject lastContainer;

    public string speciesName;

    public virtual void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[resourceLevel];
        resourceLevel = 0;
        mergeCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    public void OnMouseDown()
    {
        distance = Vector2.Distance(transform.position, mergeCam.transform.position);
        dragging = true;
        Ray ray = mergeCam.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        startDist = transform.position - rayPoint;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitlayer))
        {
            Container containerScript;
            containerScript = hit.collider.gameObject.GetComponent<Container>();

            if (containerScript.currentItem != null)
            {
                containerScript.currentItem = null;
            }
        }
    }

    public void OnMouseUp()
    {
        dragging = false;

        Ray ray = mergeCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitlayer))
        {
            Container containerScript;
            containerScript = hit.collider.gameObject.GetComponent<Container>();

            if (containerScript.currentItem == null)
            {
                containerScript.currentItem = gameObject;
                transform.position = hit.transform.position;
                lastContainer = hit.collider.gameObject;
            }
            else
            {
                containerScript.newItem = gameObject;
                if (containerScript.newItem.GetComponent<Mergable>().speciesName == containerScript.currentItem.GetComponent<Mergable>().speciesName)
                {
                    if (containerScript.currentItem.GetComponent<Mergable>().resourceLevel == resourceLevel)
                    {
                        resourceLevel += 1;
                        GetMerged();
                        transform.position = hit.transform.position;
                        lastContainer = hit.collider.gameObject;

                        Destroy(containerScript.currentItem);
                        containerScript.currentItem = gameObject;
                        containerScript.newItem = null;
                    } else
                    {
                        transform.position = lastContainer.transform.position;
                    }
                } else
                {
                    transform.position = lastContainer.transform.position;
                }

                
            }
        }
    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = mergeCam.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint + startDist;
        }
    }


    public void GetMerged()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[resourceLevel];
        //    switch (resourceLevel)
        //    {
        //        case 0:

        //            break;
        //        case 1:
        //            print("Hello and good day!");
        //            break;
        //        case 2:
        //            print("Whadya want?");
        //            break;
        //        case 3:
        //            print("Ulg, glib, Pblblblblb");
        //            break;
        //        case 4:
        //            print("Ulg, glib, Pblblblblb");
        //            break;
        //        default:
        //            print("Incorrect intelligence level.");
        //            break;
        //    }
    }
}
