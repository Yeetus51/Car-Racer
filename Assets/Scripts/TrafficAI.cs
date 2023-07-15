using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficAI : MonoBehaviour
{

    public CarController carController;

    public float speed = 1;
    float originalSpeed; 

    // Start is called before the first frame update
    void Start()
    {
        speed = carController.speed - Random.Range(carController.speed/4, carController.speed / 2.5f);
        originalSpeed = speed;
    }


    private void FixedUpdate()
    {
        AutoBrake();

        transform.Translate(transform.forward * -carController.speed * carController.slowMotion);
        transform.Translate(Vector3.forward * speed * carController.slowMotion);
    }


    void AutoBrake()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up * 2, transform.forward, out hit, 25))
        {
            if (hit.transform.gameObject.GetComponent<TrafficAI>() != null)
            {
                float otherSpeed = hit.transform.gameObject.GetComponent<TrafficAI>().speed;
                speed -= (speed - otherSpeed) / 8;
            }
        }
        else
        {
            speed += (originalSpeed - speed) / 8;
        }
        if (speed < 0) speed = 0;
    }
    // Update is called once per frame
    void Update()
    {
        DestroyCars();
    }

    void DestroyCars()
    {
        if (transform.position.z > 1200 || transform.position.z < -50) Destroy(gameObject); 
    }
}
