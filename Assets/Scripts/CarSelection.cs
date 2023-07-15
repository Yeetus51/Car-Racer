using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    [SerializeField]
    GameObject[] cars;

    List<GameObject> newCarsList = new List<GameObject>();
    List<float> carPrices = new List<float>();


    [SerializeField]
    Button play; 

    [SerializeField]
    Button menu; 

    [SerializeField]
    Button next;

    [SerializeField]
    Button pervious;

    [SerializeField]
    Button seleted;

    [SerializeField]
    Button buy;

    [SerializeField]
    TextMeshProUGUI cash;

    [SerializeField]
    TextMeshProUGUI carPrice;



    float delay;

    int carListPosition; 

    float position;
    float desPoition; 


    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < cars.Length; i++)
        {
            GameObject newCar = Instantiate(cars[i]) as GameObject;
            if (newCar.GetComponent<TrafficAI>() != null) Destroy(newCar.GetComponent<TrafficAI>());
            if (newCar.GetComponent<CarController>() != null) Destroy(newCar.GetComponent<CarController>());
            if (newCar.GetComponent<BoxCollider>() != null) Destroy(newCar.GetComponent<BoxCollider>());
            if (newCar.GetComponent<Rigidbody>() != null) Destroy(newCar.GetComponent<Rigidbody>());
            if (newCar.GetComponentInChildren<WheelRotation>() != null) Destroy(newCar.GetComponentInChildren<WheelRotation>());
            newCar.transform.position = new Vector3(i * 20, 0, 0);
            newCar.transform.rotation = Quaternion.Euler(0, 220, 0); 
            newCarsList.Add(newCar);
            carPrices.Add(i*6000);

        }

        next.onClick.AddListener(NextCar);
        pervious.onClick.AddListener(PerviousCar);
        seleted.onClick.AddListener(SelectCar);
        buy.onClick.AddListener(BuyCar);
        play.onClick.AddListener(Play);
        menu.onClick.AddListener(GoToMenu);


        GameInfo.psOwnedCars.Add(0);



        GameInfo.psMainCar = 0;


        GameInfo.psPlayerMoney += 800; 
        cash.text = "Cash : " + GameInfo.psPlayerMoney + "$"; 

    }

    void NextCar()
    {
        desPoition -= 20;
    }
    void PerviousCar()
    {
        desPoition += 20;
    }
    void SelectCar()
    {
        GameInfo.psMainCar = carListPosition;
        GameInfo.psCarPower = 1 + carListPosition / 2;
        GameInfo.psBrakePower = 2 + carListPosition /2;
        GameInfo.psNosTanks = 3 + carListPosition;
    }
    void Play()
    {
        SceneManager.LoadScene("ModeSelection");
    }
    void GoToMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    void BuyCar()
    {
        if (GameInfo.psPlayerMoney >= carPrices[carListPosition])
        {
            GameInfo.psPlayerMoney -= carPrices[carListPosition];
            cash.text = "Cash : " + GameInfo.psPlayerMoney + "$";
            GameInfo.psOwnedCars.Add(carListPosition);
            SelectCar();
        }
        else Debug.Log("Youre too broke to buy this car ! ");
    }

    void Animate()
    {
        for(int i = 0; i < newCarsList.Count; i++)
        {
            newCarsList[i].transform.position = new Vector3(i * 20 + position, 0, 0); 

        }
        position = Mathf.Lerp(position, desPoition, 0.1f);

        if(desPoition > 0) delay += 0.07f;
        if (desPoition > 0 && delay > 0.3f) 
        {
            desPoition = 0;
            delay = 0;
        }

        if (desPoition < (newCarsList.Count - 1) * -20) delay += 0.1f;
        if (desPoition < (newCarsList.Count - 1) * -20 && delay > 0.3f)
        {
            desPoition = (newCarsList.Count - 1) * -20;
            delay = 0;
        }

    }

    private void FixedUpdate()
    {
        Animate();
    }

    // Update is called once per frame
    void Update()
    {

        carListPosition = Mathf.RoundToInt(-position / 20);
        if(carListPosition < 0) carListPosition = 0;
        if(carListPosition > newCarsList.Count-1) carListPosition = newCarsList.Count-1;


        if(GameInfo.psOwnedCars.Contains(carListPosition)) buy.gameObject.SetActive(false);
        else buy.gameObject.SetActive(true);

        if (GameInfo.psMainCar == carListPosition) seleted.interactable = false;
        else seleted.interactable = true;


        if (GameInfo.psPlayerMoney >= carPrices[carListPosition]) carPrice.color = new Color(0, 1, 0);
        else carPrice.color = new Color(1, 0, 0);

        carPrice.text = carPrices[carListPosition].ToString() + "$";

    }
}
