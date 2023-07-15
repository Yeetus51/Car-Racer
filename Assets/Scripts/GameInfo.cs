using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    [HideInInspector]
    public static GameInfo psSelf;
    
    [HideInInspector]
    public static int psMainCar = 0;
    [HideInInspector]
    public static int psNosTanks = 3;
    [HideInInspector]
    public static float psCarPower = 1;
    [HideInInspector]
    public static float psBrakePower = 2;

    [HideInInspector]
    public static float psLevelScore;
    [HideInInspector]
    public static float psLevelTime;


    [HideInInspector]
    public static int psMaxLevel;

    [HideInInspector]
    public static int psCurrentLevel;

    [HideInInspector]
    public static List<int> psOwnedCars = new List<int>();

    [HideInInspector]
    public static float psPlayerMoney; 



    [SerializeField]
    public static CarController psCarController;

    [SerializeField]
    public static UIElements psUiElements; 

    




    // Start is called before the first frame update
    void Start()
    {
        psSelf = this;
        //SetValues();
    }



    public void SetCar()
    {
        psCarController.mainCarIndex = psMainCar;
        psCarController.nosTanks = psNosTanks;
        psCarController.power = psCarPower;
        psCarController.brakeForce = psBrakePower;

        Debug.Log("Values have been set! ");
        Debug.Log("Car Index : " + psMainCar + " : " + psCarController.mainCarIndex);
        Debug.Log("Power : " + psCarPower + " : " + psCarController.power);

    }


    public void SetUi()
    {
        psUiElements.levelScore = psLevelScore;
        psUiElements.levelTime = psLevelTime;

        Debug.Log("Values have been set! ");
        Debug.Log("Time : " + psLevelTime + " : " + psUiElements.levelTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
