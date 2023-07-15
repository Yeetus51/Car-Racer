using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public GameObject car;
    public CarController carController;
/*    public ItemMovement(GameObject pCar, CarController pCarController)
    {
        car = pCar;
        carController = pCarController;
    }*/

    float speed = 1;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -50)
        {
            if (gameObject.tag == "repeat")
            {
                gameObject.transform.position += new Vector3(0, 0, 500);
            }
            else if (gameObject.tag == "groundRepeat" && transform.position.z < -100)
            {
                gameObject.transform.position += new Vector3(0, 0, 1000);
            }
            else if(gameObject.tag != "groundRepeat") Destroy(gameObject);
        }
    }


    private void FixedUpdate()
    {
        transform.Translate(-transform.forward * carController.speed * carController.slowMotion); 
    }

}
