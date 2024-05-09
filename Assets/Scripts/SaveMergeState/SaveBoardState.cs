using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;

public class SaveBoardState : MonoBehaviour
{
    public GameObject[] containers;
    public string[] loadedString;
    public string contents;
    public GameObject frog, generator;
    public GameObject temporaryFrog;

    public GameObject debugText;

    private void Start()
    {
        if (PlayerPrefs.GetString("BoardStateString") != null)
        {
            LoadState();
        }

    }

    private void Update()
    {
        debugText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("BoardStateString");
    }
    public void SaveState()
    {
        contents = string.Empty;
        for (int i = 0; i < containers.Length; i++)
        {
            if (containers[i].GetComponent<Container>().currentItem == null)
            {
                contents += "E,";
                Debug.Log("Found an empty container");
            }
            else if (containers[i].GetComponent<Container>().currentItem.name == "Frog")
            {
                Debug.Log("Found a container with a frog");
                temporaryFrog = containers[i].GetComponent<Container>().currentItem;
                contents += temporaryFrog.GetComponent<Mergable>().resourceLevel.ToString();
                contents += ",";
            }
        }
        Debug.Log(contents);
        PlayerPrefs.SetString("BoardStateString", contents);
    }

    public void LoadState()
    {
        loadedString = PlayerPrefs.GetString("BoardStateString").Split(",");

        for (int i = 0; i < containers.Length; i++)
        {
            if (loadedString[i] == "E" || loadedString[i] == null)
            {
                continue;
            }
            else
            {
                if (loadedString[i] == "G")
                {
                    GameObject spawnedGenerator = Instantiate(generator, containers[i].gameObject.transform.position, containers[i].gameObject.transform.rotation);
                }
                else
                {
                    GameObject go = Instantiate(frog, containers[i].gameObject.transform.position, containers[i].gameObject.transform.rotation);
                    containers[i].GetComponent<Container>().currentItem = go;
                    go.name = "Frog";
                    go.GetComponent<Mergable>().lastContainer = containers[i].gameObject;
                    //go.GetComponent<Mergable>().resourceLevel = int.Parse(loadedString[i]);

                    if (int.TryParse(loadedString[i], out int result))
                    {
                        go.GetComponent<Mergable>().resourceLevel = result;
                        Debug.Log("The string was " + loadedString[i] + " and the parsed result was " + result);
                    }
                }
            }
        }

    }
}
