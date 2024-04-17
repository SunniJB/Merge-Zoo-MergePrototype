using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Overworld : MonoBehaviour
{
    private ChangeScene changeScene;

    [SerializeField] private Button startGameButton;
    public Slider energySlider;
    public GameObject energyText;
    [SerializeField] int sceneToGoTo;

    // Start is called before the first frame update
    void Start()
    {
        //changeScene = GameObject.FindGameObjectWithTag("DDOL").GetComponent<ChangeScene>();

        //if (startGameButton != null)
        //{
        //    startGameButton.onClick.AddListener(ChangeScene);
        //}

        energySlider.maxValue = GameManager.instance.maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        energySlider.value = GameManager.instance.energy;
        energyText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.energy + "/" + GameManager.instance.maxEnergy;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToGoTo); 
    }
}
