using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    Dictionary<string, GameObject> wheelsDic = new Dictionary<string , GameObject>();
    [SerializeField]
    Transform[] objects;
    List<GameObject> wheelsList = new List<GameObject>();

    [SerializeField]
    CarController carController;

    [SerializeField]
    TrafficAI trafficAi;

    [SerializeField]
    float wheelSpeed;

    float wheelRpm; 

    List<Vector3> wheelOriginalPos = new List<Vector3>();

    float posY;

    string type; 

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<CarController>() != null)
        {
            type = "CARCONTROLLER";
            carController = GameObject.Find("MainCar").GetComponent<CarController>();
        }
            
        if (GetComponentInParent<TrafficAI>() != null)
        {
            type = "TRAFFICAI";
            trafficAi = GetComponentInParent<TrafficAI>();
            carController = GameObject.Find("MainCar").GetComponent<CarController>(); 
        }


        foreach(Transform obj in objects)
        {
            wheelsDic.Add(obj.name, obj.gameObject);
        }
        wheelsList.Add(wheelsDic["WFR"]);
        wheelsList.Add(wheelsDic["WBR"]);
        wheelsList.Add(wheelsDic["WFL"]);
        wheelsList.Add(wheelsDic["WBL"]);

        posY = wheelsDic["WFR"].transform.position.y;

        foreach(GameObject wheel in wheelsList)
        {
            wheelOriginalPos.Add(wheel.transform.position);
        }

    }


    
    // Update is called once per frame
    private void FixedUpdate()
    {

        if(type == "CARCONTROLLER")
        {           
            wheelRpm += carController.speed * 360 / (1.36f * Mathf.PI);
            wheelRpm *= carController.slowMotion;
            wheelSpeed = carController.speed;
        }

        if(type == "TRAFFICAI")
        {
            wheelRpm += trafficAi.speed * 360 / (1.36f * Mathf.PI);
            wheelRpm *= carController.slowMotion;
        }
      
        
        if (wheelRpm > 360) wheelRpm -= 360; 

        for (int i = 0; i < wheelsList.Count; i++)
        {
            wheelsList[i].transform.localRotation = Quaternion.Euler(wheelRpm, 0, 0);
            if (!carController.crashed)
            {
                wheelsList[i].transform.position = new Vector3(wheelsList[i].transform.position.x, posY, wheelsList[i].transform.position.z);
            }
        }
        wheelsList[0].transform.rotation = Quaternion.Euler(wheelRpm, carController.steering * 30,0);
        wheelsList[0].transform.rotation = Quaternion.Euler(wheelRpm, carController.steering * 30, 0);


    }
}
