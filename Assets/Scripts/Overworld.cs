using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Overworld : MonoBehaviour
{
    private ChangeScene changeScene;

    [SerializeField] private Button startGameButton;
    public Slider energySlider;
    public GameObject energyText;

    // Start is called before the first frame update
    void Start()
    {
        changeScene = GameObject.FindGameObjectWithTag("DDOL").GetComponent<ChangeScene>();

        if (startGameButton != null)
        {
            startGameButton.onClick.AddListener(StartGame);
        }

        energySlider.maxValue = GameManager.instance.maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        energySlider.value = GameManager.instance.energy;
        energyText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.energy + "/" + GameManager.instance.maxEnergy;
    }

    private void StartGame()
    {
        changeScene.LoadSceneAsync(2);
    }
}
