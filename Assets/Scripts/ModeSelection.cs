using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ModeSelection : MonoBehaviour
{

    [SerializeField]
    GameObject[] displayCars;



    [SerializeField]
    Button levelsButton;

    [SerializeField]
    Button endlessButton;

    [SerializeField]
    Button customButton;


    // Start is called before the first frame update
    void Start()
    {
       // RemoveTroubleMakers();

        levelsButton.onClick.AddListener(Levels);
        endlessButton.onClick.AddListener(Endless);
        customButton.onClick.AddListener(Custom);
    }

    void RemoveTroubleMakers()
    {
        foreach(GameObject car in displayCars)
        {
            if (car.GetComponent<TrafficAI>() != null) Destroy(car.GetComponent<TrafficAI>());
            if (car.GetComponent<CarController>() != null) Destroy(car.GetComponent<CarController>());
            if (car.GetComponentInChildren<WheelRotation>() != null) Destroy(car.GetComponentInChildren<WheelRotation>());


/*            if (car.GetComponent<TrafficAI>() != null) DestroyImmediate(car.GetComponent<TrafficAI>(), true);
            if (car.GetComponent<CarController>() != null) DestroyImmediate(car.GetComponent<CarController>(), true);
            if (car.GetComponentInChildren<WheelRotation>() != null) DestroyImmediate(car.GetComponentInChildren<WheelRotation>(), true);*/
        }
    }

    void Levels()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    void Endless()
    {

    }

    void Custom()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
