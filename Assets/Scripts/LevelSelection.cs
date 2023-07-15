using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{

    public List<RawImage> locks = new List<RawImage>();

    // Start is called before the first frame update
    void Start()
    {
        locks[GameInfo.psMaxLevel].gameObject.SetActive(false);


        for (int i = 0; i < locks.Count; i++)
        {
            if (locks[i].gameObject.activeSelf == false)
            {
                for (int j = i; j > -1; j--)
                {
                    locks[j].gameObject.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {



    }
}
