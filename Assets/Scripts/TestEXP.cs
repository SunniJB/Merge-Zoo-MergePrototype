using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestEXP : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        levelText.text = ExperienceManager.UpdateExperiencePoints(150);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
