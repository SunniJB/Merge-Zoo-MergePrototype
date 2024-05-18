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

    public AudioUI audioUI;

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
        audioUI =  GameObject.FindWithTag("AudioUI").GetComponent<AudioUI>();
        //if (type == GeneratorType.Pond)
        //{
        //    if (level == GeneratorLevel.Zero)
        //    {

        //    }
        //}
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0) || Input.touchCount > 0)
        //{
        //    OnClick();
        //}
    }

    public void OnMouseDown()
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
        if (GameManager.instance.energy < 1) return;

        foreach (var container in containers)
        {
            if (container.currentItem == null)
            {
                GameObject spawnedResource = Instantiate(resource, container.gameObject.transform.position, container.gameObject.transform.rotation);
                container.currentItem = spawnedResource;
                spawnedResource.name = "Frog";

                spawnedResource.GetComponent<Mergable>().lastContainer = container.gameObject;
                GameManager.instance.energy -= 1;
                
                audioUI.PlayAudioClip(2);

                return;
            }
        }
    }
}
