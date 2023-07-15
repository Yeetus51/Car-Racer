using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "road")
        {
            other.transform.position = new Vector3(0, 0, 500);
        }
        else
        {
            Debug.Log("Tag : " + other.gameObject.tag);
            //Destroy(other);
        } 
            
    }
}
