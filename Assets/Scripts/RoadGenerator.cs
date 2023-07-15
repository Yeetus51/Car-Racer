using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject[] items;
    [SerializeField]
    GameObject mainCar;
    [SerializeField]
    CarController mainCarController;
    public float delay;
    [SerializeField]
    float spaceBetween = 1;

    float time;
    float rate;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in items)
        {
            if (item.GetComponent<ItemMovement>() != null)
            {
                item.GetComponent<ItemMovement>().car = mainCar;
                item.GetComponent<ItemMovement>().carController = mainCarController;
            }
            else
            {
                item.AddComponent<ItemMovement>();
                item.GetComponent<ItemMovement>().car = mainCar;
                item.GetComponent<ItemMovement>().carController = mainCarController;
            }
        }
        for (int i = 0; i < 87; i++)
        {
            GameObject newItem1 = items[0];
            newItem1.transform.position = transform.position + (transform.forward * (500 - i*5.8f));
            Instantiate(newItem1).transform.parent = this.gameObject.transform;
        }
    }

    private void FixedUpdate()
    {
        time += 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
/*        rate = spaceBetween / mainCarController.speed;

        if (time >= rate)
        {
            GameObject newItem1 = items[0];
            newItem1.transform.position = transform.position + (transform.forward * 500);
            Instantiate(newItem1).transform.parent = this.gameObject.transform;
            time = 0;
        }*/
    }
}
