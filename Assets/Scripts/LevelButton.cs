using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class LevelButton : MonoBehaviour
{

    [SerializeField]
    RectTransform template;

    [SerializeField]
    Button level; 

    [SerializeField]
    RawImage locked;

    [SerializeField]
    TextMeshProUGUI levelNumber;

    [SerializeField]
    TextMeshProUGUI xpGoal;

    [SerializeField]
    TextMeshProUGUI reward;

    // Start is called before the first frame update
    void Start()
    {
        level.onClick.AddListener(StartLevel);
        xpGoal.text = (Int32.Parse(xpGoal.text) * 3).ToString();
        reward.text = (Int32.Parse(reward.text) * 3).ToString();

    }

    void StartLevel()
    {
        GameInfo.psLevelScore = Int32.Parse(xpGoal.text);
        GameInfo.psLevelTime = 30;
        GameInfo.psCurrentLevel = Int32.Parse(levelNumber.text);

        SceneManager.LoadScene("Game"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
