using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject[] items;

    [SerializeField]
    Material[] materials;

    [SerializeField]
    Texture[] textures;

    [SerializeField]
    GameObject mainCar;

    [SerializeField]
    CarController mainCarController;

    [SerializeField]
    GameObject ground;


    List<GameObject> itemsToSpawn;


    int nextBiome = 0;
    float distanceToTunnle; 

    public float delay;

    float biomeChangeTime = 7;
    bool biomeChanged; 

    float time;
    float rate;

    bool generateTrees = false; 



    // Start is called before the first frame update
    void Start()
    {
        itemsToSpawn = new List<GameObject>();
        if (ground.GetComponent<ItemMovement>() != null)
        {
            ground.GetComponent<ItemMovement>().car = mainCar;
            ground.GetComponent<ItemMovement>().carController = mainCarController;
        }
        else
        {
            ground.AddComponent<ItemMovement>();
            ground.GetComponent<ItemMovement>().car = mainCar;
            ground.GetComponent<ItemMovement>().carController = mainCarController;
        }


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
            item.transform.rotation = new Quaternion(item.transform.rotation.x, 0, item.transform.rotation.z, 0);
        }
        for (int i = 0; i < 5; i++)
        {
            GameObject pGround = ground;
            pGround.transform.position = transform.position + transform.right * 15 + (transform.forward * (1000 - i * 290f)) + transform.up * -2;
            Instantiate(pGround).transform.parent = this.gameObject.transform;

            pGround.transform.position = new Vector3(0, 0, 0);
            pGround.transform.position = transform.position + transform.right * -15 + (transform.forward * (1000 - i * 290f)) + transform.up * -2;
            pGround.transform.Rotate(transform.up, 180);
            Instantiate(pGround).transform.parent = this.gameObject.transform;
            pGround.transform.Rotate(transform.up, -180);
        }

        Biome(0, true);
        Biome(0, false);
    }

    private void FixedUpdate()
    {
        time += 0.02f;
        biomeChangeTime -= Time.deltaTime;

        BiomeChanging(nextBiome);
    }

    // Update is called once per frame
    void Update()
    {
        rate = 1/mainCarController.speed;

        if(time >= rate && generateTrees)
        {
            GenerateItems(20, 50);
            time = 0; 
        }
        else if(time >= rate && !generateTrees)
        {
            GenerateItems(2, 30);
            time = 0;
        }

        if(biomeChangeTime < 0 && !biomeChanged)
        {
            biomeChanged = true;
            InitiateBiomeChange(Random.Range(0, 4));
        }


        if (Input.GetKeyDown(KeyCode.Alpha1)) InitiateBiomeChange(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) InitiateBiomeChange(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) InitiateBiomeChange(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) InitiateBiomeChange(3);
    }


    void GenerateItems(int randomRange, int offset)
    {
        int r1 = Random.Range(0, itemsToSpawn.Count);
        int r2 = Random.Range(0, itemsToSpawn.Count);// needs to be in fixed update
        GameObject newItem1 = itemsToSpawn[r2];
        GameObject newItem2 = itemsToSpawn[r1];
        int rOffsetx = Random.Range(0, randomRange*2);
        int rOffsety = Random.Range(-randomRange, randomRange);
        newItem1.transform.Rotate(transform.up, 180);
        newItem1.transform.position = transform.position + (transform.right * (offset + rOffsetx)) + (transform.forward * (1000 + rOffsety));
        Instantiate(newItem1).transform.parent = this.gameObject.transform;
        newItem1.transform.Rotate(transform.up, -180);


        rOffsetx = Random.Range(0, randomRange * 2);
        rOffsety = Random.Range(-randomRange, randomRange);
        newItem2.transform.position = transform.position + (transform.right * -(offset + rOffsetx)) + (transform.forward * (1000 + rOffsety));
        Instantiate(newItem2).transform.parent = this.gameObject.transform;
    }

    void BiomeChanging(int name)
    {
        if (name != 0)
        {
            distanceToTunnle += mainCarController.speed;

            if(distanceToTunnle > 1000)
            {
                Biome(name, false);
                nextBiome = 0; 
                distanceToTunnle = 0;
            }
        }
    }

    void InitiateBiomeChange(int name)
    {
        nextBiome = name;
        Biome(name, true);

        GameObject tunnel = items[13];
        GameObject tunnelEntrance = items[14];

        tunnelEntrance.transform.position = transform.position + (transform.forward * (1000 -  5.8f));
        Instantiate(tunnelEntrance);

        for (int i = 0; i < 50; i++)
        {
            tunnel.transform.position = transform.position + (transform.forward * (1000 + i * 5.8f));
            Instantiate(tunnel);
        }
        
    }


    void Biome(int name, bool first)
    {
        switch (name)
        {
            case 0:

                if (first)
                {
                    itemsToSpawn.Clear();
                    generateTrees = true;

                    for (int i = 0; i < 3; i++)
                    {

                        GameObject item = items[i];
                        itemsToSpawn.Add(item);
                    }

                }
                else
                {
                    materials[0].mainTexture = textures[0];
                    materials[1].mainTexture = textures[3];
                    materials[2].mainTexture = textures[6];
                }
                
                break;

            case 1:

                if (first)
                {
                    itemsToSpawn.Clear();
                    generateTrees = true;   

                    for (int i = 4; i < 7; i++)
                    {
                        GameObject item = items[i];
                        itemsToSpawn.Add(item);
                    }
                }
                else
                {
                    materials[0].mainTexture = textures[1];
                    materials[1].mainTexture = textures[4];
                }
                break;

            case 2:

                if (first)
                {
                    itemsToSpawn.Clear();
                    generateTrees = true;

                    for (int i = 0; i < 3; i++)
                    {
                        GameObject item = items[i];
                        itemsToSpawn.Add(item);
                    }
                }
                else
                {
                    materials[0].mainTexture = textures[2];
                    materials[1].mainTexture = textures[5];
                    materials[2].mainTexture = textures[7];
                }
                break;

            case 3:

                if (first)
                {
                    itemsToSpawn.Clear();
                    generateTrees = false;

                    for (int i = 8; i < 12; i++)
                    {
                        GameObject item = items[i];
                        itemsToSpawn.Add(item);
                    }
                }
                break; 
        }
    }
}


            