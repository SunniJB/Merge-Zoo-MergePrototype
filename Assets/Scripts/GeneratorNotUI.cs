using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorNotUI : MonoBehaviour
{
    public GeneratorType type;
    public GeneratorLevel level;

    public GameObject resource;

    public Container[] containers;
    public LayerMask hitlayer;

    public enum GeneratorType
    {
        Pond
    }
    public enum GeneratorLevel
    {
        Zero,
        One,
        Two,
        Three,
        Four
    }
    private void Start()
    {
        //if (type == GeneratorType.Pond)
        //{
        //    if (level == GeneratorLevel.Zero)
        //    {

        //    }
        //}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.touchCount > 0)
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitlayer))
        {
            Generate();
        }

    }

    public void Generate()
    {
        foreach (var container in containers)
        {
            if (container.currentItem == null)
            {
                Debug.Log("Container " + container.name + " was picked. It is placed at " + container.transform.position);
                GameObject spawnedResource = Instantiate(resource, container.gameObject.transform.position, container.gameObject.transform.rotation);
                container.currentItem = spawnedResource;
                spawnedResource.name = "Frog";
                Debug.Log("The container's item is " + container.currentItem.name);
                spawnedResource.GetComponent<Mergable>().lastContainer = container.gameObject;
                Debug.Log("The resource's last container is " + resource.GetComponent<Mergable>().lastContainer.name);
                return;
            }
        }
    }
}
