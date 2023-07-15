using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    [SerializeField]
    Button startButton;


    [SerializeField]
    Button carsButton;

    [SerializeField]
    Button exitButton;



    // Start is called before the first frame update
    void Start()
    {

        startButton.onClick.AddListener(StartButton);
        carsButton.onClick.AddListener(Cars);
        exitButton.onClick.AddListener(Exit);
    }

    void StartButton()
    {   
        SceneManager.LoadScene("ModeSelection");
    }

    void Cars()
    {
        SceneManager.LoadScene("CarSelection");
    }

    void Exit()
    {
        Application.Quit();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
