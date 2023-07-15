using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficGenerator : MonoBehaviour
{

    [SerializeField]
    GameObject[] cars;

    [SerializeField]
    CarController mainCarController;

    [SerializeField]
    float carSpeed; 

    [SerializeField]
    float rate = 10;
    float time1; 
    float time2;
    float time3;
    float time4;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject car in cars)
        {
            if (car.GetComponent<TrafficAI>() != null)
            {
                car.GetComponent<TrafficAI>().carController = mainCarController;
            }
            else
            {
                car.AddComponent<TrafficAI>();
                car.GetComponent<TrafficAI>().carController = mainCarController;
            }
            car.transform.rotation = new Quaternion(car.transform.rotation.x, 0, car.transform.rotation.z, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*        rate = 1 /(mainCarController.acceleration * 50);
                if (rate < 0) rate = 5;  
                rate = Mathf.Clamp(rate, 0, 3);
                rate = rate/3f;*/

        rate = Mathf.Sqrt(-Mathf.Pow((mainCarController.speed - 4), 7) * 0.001f) + 0.2f;

        carSpeed = mainCarController.speed; 
    }

    private void FixedUpdate()
    {
        GenerateTraffic(rate);
    }

    void GenerateTraffic(float pRate)
    {

        time1 += Random.Range(-0.01f, 0.03f);
        time2 += Random.Range(-0.01f, 0.03f);
        time3 += Random.Range(-0.01f, 0.03f);
        time4 += Random.Range(-0.01f, 0.03f);


        if (time1/5 > pRate)
        {
            SpawnCar(3.4f, 0);
            time1 = 0;
        }
        if(time4/5 > pRate)
        {
            SpawnCar(11, 0);
            time4 = 0;  
        }
        if(time3/2 > pRate)
        {
            SpawnCar(3.4f, 180);
            time3 = 0;
        }

        if (time2/2 > pRate)
        {
            SpawnCar(11, 180);
            time2 = 0;
        }
    }

    void SpawnCar(float offset, float angle)
    {
        int randomCar = Random.Range(0, cars.Length);
        GameObject car = cars[randomCar];

        car.transform.position = new Vector3(offset, 0, transform.position.z);
        car.transform.RotateAround(transform.position, transform.up, angle);
        Instantiate(car);
        car.transform.RotateAround(transform.position, transform.up, -angle);


    }
}
