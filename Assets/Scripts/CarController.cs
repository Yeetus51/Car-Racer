using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class CarController : MonoBehaviour
{
    bool left;
    bool right;
    bool up;
    bool down;

    public int nosTanks = 3;
    public bool nosAvtive;
    public float nosTimer;
    float originalPower;

    [SerializeField]
    GameObject[] cars;

    public int mainCarIndex; 


    [SerializeField]
    float drag = 1;

    [SerializeField]
    float airDrag;

    [SerializeField]
    float minSpeed = 0.1f;

    public float speed = 1;

    public float steering;

    public float power = 0.1f;
    public float brakeForce = 0.5f; 

    [SerializeField]
    float turningSpeed = 0.1f;

    [SerializeField]
    float tunringAmount;

    [SerializeField]
    float weightTrasfer = 5;

    [SerializeField]
    float nosShot = 1;

    int lastCarID = 0;

    [SerializeField]
    UIElements hud;

    [SerializeField]
    GameOver gameOver;

    [SerializeField]
    GameObject explosion;



    public float slowMotion = 1;

    public bool crashed;
    public bool finished;
    public bool timeOut;
    float crashImpact;
    int frames;


    public Vector2 acceleration;
    public Vector2 gForces; 
    float oldSpeed;
    float oldTurnSpeed; 

    



    // Start is called before the first frame update
    void Start()
    {
        GameInfo.psCarController = this;
        GameInfo.psSelf.SetCar(); 

        SpawnCar(mainCarIndex);
        originalPower = power;

        // Add all wheels based on name
    }

    // Update is called once per frame
    void Update()
    {
        if(!finished && !timeOut) KeyboardControlls();


        nosTimer -= Time.deltaTime;
        if(nosTimer < 0)
        {      
            nosAvtive = false;
            power = originalPower;
        }


    }

    private void FixedUpdate()
    {
        if (!crashed)
        {
            oldSpeed = speed;
            oldTurnSpeed = steering;
            VelocityCalculation();

            transform.rotation = new Quaternion(-gForces.x * 10 * weightTrasfer, steering * tunringAmount, gForces.y * 10 * weightTrasfer, 180);
            transform.position = new Vector3(transform.position.x, 0, 0);

            acceleration = new Vector2(speed - oldSpeed, steering - oldTurnSpeed);


            gForces = gForces * 0.9f + acceleration * 0.1f;
        }  
        CrashAnimation(crashImpact);
    }

    void SpawnCar(int carIndex)
    {
        GameObject carObject = Instantiate(cars[carIndex]);
        carObject.transform.position = Vector3.zero; 
        if (carObject.GetComponent<TrafficAI>() != null) Destroy(carObject.GetComponent<TrafficAI>());
        carObject.transform.parent = gameObject.transform;

    }

    void VelocityCalculation()
    {
        if (up) speed += power /50;
        if (down) speed -= brakeForce / 50;
        if (right) steering += turningSpeed;
        if (left) steering -= turningSpeed;

        airDrag = -Mathf.Pow(speed,2f) * (drag/1000f) + 1f;
        airDrag = Mathf.Clamp(airDrag, 0, 1);

        speed *= airDrag;
        if (speed < minSpeed) speed = minSpeed; 

        steering *= 0.90f; 
        transform.position += transform.right * steering * 0.1f * slowMotion;
    }

    public bool closeCall()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position - transform.right*1.5f + transform.up*2, -transform.right, out hit, 1) || Physics.Raycast(transform.position + transform.right * 1.5f + transform.up * 2, transform.right, out hit, 1))
        {
            if (hit.transform.gameObject.GetComponent<TrafficAI>() != null)
            {
                TrafficAI other = hit.transform.gameObject.GetComponent<TrafficAI>();
                if(other.GetInstanceID() != lastCarID)
                {
                    float speedDiff = speed - other.speed;
                    if (other.transform.rotation.y > 0) speedDiff = speed + other.speed;
                    lastCarID = other.GetInstanceID();
                    if (speed > 2 && speedDiff > 0.8f) return true;
                }
            }
        }
        return false;
    }

    void CrashAnimation(float impact = 0)
    {
        if (crashed)
        {
            frames++; 
            transform.RotateAround(transform.position + transform.forward * 2, Vector3.right, impact * slowMotion);
            slowMotion = Mathf.Exp(-0.1f*frames);
            if (slowMotion < 0) slowMotion = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TrafficAI>() != null)
        {
            float impact = speed - other.GetComponent<TrafficAI>().speed;
            float impactX = steering;
            if (other.transform.rotation.y > 0)
            {
                impact = speed + other.GetComponent<TrafficAI>().speed;
            }

            Debug.Log("Fowrads Impact : " + impact + " Side Impact : " + impactX);

            if (Mathf.Abs(impact) > 0.25f && !timeOut)
            {

                speed -= impact;
                crashImpact = impact * 1.3f;
                crashed = true;
                if(!finished && !timeOut)
                {
                    GameOver dialog = Instantiate(gameOver) as GameOver;
                    dialog.successful = false;
                    dialog.reason = "CRASHED";
                    dialog.self.SetParent(hud.gameObject.transform);
                    dialog.self.anchoredPosition = new Vector3(0, 0, 0);

                    Instantiate(explosion).transform.position = transform.position + new Vector3(0, 2, 5.68f);
                }
               



                Debug.Log("You Died");

            }

            else speed -= impact * 1.5f;
            steering -= impactX * 1.5f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }



    void NOS()
    {
        if (!nosAvtive && nosTimer < -6 && nosTanks >= 1)
        {
            nosAvtive = true;
            power += nosShot;
            nosTimer = 3;
            nosTanks -= 1; 
        }
    }




    void KeyboardControlls()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) up = true;
        else up = false;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) down = true;
        else down = false;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) left = true;
        else left = false;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) right = true;
        else right = false;

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift)) NOS();

        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1 && !crashed && !finished)
        {
            GameOver dialog = Instantiate(gameOver) as GameOver;
            dialog.successful = false;
            dialog.reason = "PAUSED";
            dialog.self.SetParent(hud.gameObject.transform);
            dialog.self.anchoredPosition = new Vector3(0, 0, 0);

            Time.timeScale = 0; 
        }
    }
}
