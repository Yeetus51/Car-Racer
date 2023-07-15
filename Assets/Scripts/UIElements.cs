using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; 
using UnityEngine;

public class UIElements : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    CarController carController;

    [SerializeField]
    TextMeshProUGUI speed;

    [SerializeField]
    RawImage speedHandle;

    [SerializeField]
    RawImage rpmHandle;

    [SerializeField]
    TextMeshProUGUI timeLeftText;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI closeCallText;

    [SerializeField]
    TextMeshProUGUI speedingText;

    [SerializeField]
    TextMeshProUGUI wrongWayText;

    [SerializeField]
    Image timeLeftImage;

    [SerializeField]
    Image scoreImage;

    [SerializeField]
    Image nosBar;

    [SerializeField]
    Image nosCountBar;

    [SerializeField]
    Image nosActive;

    [SerializeField]
    GameOver gameOver;



    public float levelTime = 50;
    public float levelScore = 1000; 

    float kmh;
    public float distance;

    public float score;

    public float speedingScore;
    public float wrongWayScore;

    float timeBarMultiplier;
    float scoreBarMultiplier;
    float nosBarMultiplier;
    float nosCountBarMultiplier; 


    int closeCallCombo;
    public int maxCloseCallCombo;
    float comboTime = 3;
    float comboColorAlpha;
    Vector2 comboFallRate;
    float comboRotateRate;
    Vector2 comboOriginalPos;
    Vector2 comboPos;
    float comboRot;
    float comboScale;


    [SerializeField]
    Vector2 rangeFall;

    [SerializeField]
    Vector2 rangeRotate;



    void Start()
    {
        GameInfo.psUiElements = this;
        GameInfo.psSelf.SetUi(); 


        comboOriginalPos = new Vector2(closeCallText.transform.position.x, closeCallText.transform.position.y);
        timeBarMultiplier = 12.04f / levelTime;
        scoreBarMultiplier = 12.04f / levelScore;
        nosBarMultiplier = 2.78f / 3;
        nosCountBarMultiplier = 3.61f / 12;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        ScoreCalculator();
        CloseCallCombo();
        Speeding();
        WrongWay();
        Nos();
    }

    void Update()
    {
        Speedometer();
        TimeLeftIndicator();

        if(score > levelScore && !carController.finished && !carController.crashed && !carController.timeOut)
        {
            carController.finished = true;

            GameOver dialog = Instantiate(gameOver) as GameOver;
            dialog.successful = true;
            dialog.reason = "FINISHED";
            dialog.self.SetParent(this.gameObject.transform);
            dialog.self.anchoredPosition = new Vector3(0, 0, 0);
        }


        if (levelTime < 0 && !carController.finished && !carController.crashed && !carController.timeOut)
        {
            carController.timeOut = true;

            GameOver dialog = Instantiate(gameOver) as GameOver;
            dialog.successful = true;
            dialog.reason = "TIMEOUT";
            dialog.self.SetParent(this.gameObject.transform);
            dialog.self.anchoredPosition = new Vector3(0, 0, 0);
        }

    }

    void Speeding()
    {
        if (kmh > 80)
        {
            speedingScore += 0.5f;
            score += 0.5f;
        }
        speedingText.text = "Speeding Points : " + speedingScore.ToString("0000");

    }

    void Nos()
    {
        float nosCapacity = carController.nosTimer;
        if (carController.nosTimer < 0) nosCapacity = Mathf.Abs(carController.nosTimer) / 2;
        nosCapacity *= nosBarMultiplier;
        nosCapacity = Mathf.Clamp(nosCapacity, 0, 2.78f);

        float nosNumberOfTanks = carController.nosTanks * nosCountBarMultiplier;
        int nosActiveLED = carController.nosTimer < -6 ? 1 : 0;
        if (carController.nosTanks < 1)
        {
            nosActiveLED = 0;
            nosCapacity = 0; 
        }

        nosBar.transform.localScale = new Vector3(nosCapacity, 0.875f, 1);
        nosCountBar.transform.localScale = new Vector3(nosNumberOfTanks, 0.415f, 1);
        nosActive.color = new Color(0, 1, 0, nosActiveLED);
    }

    void WrongWay()
    {
        if (carController.transform.position.x < -1)
        {
            wrongWayScore += 0.5f;
            score += 0.5f; 
        }
        wrongWayText.text = "Oncoming Points : " + wrongWayScore.ToString("0000");
    }

    void CloseCallCombo()
    {
        if (carController.closeCall())
        {
            closeCallCombo++;
            comboTime = 3;
            comboPos = new Vector3(comboOriginalPos.x, comboOriginalPos.y, 0);
            comboRot = 0;
            comboScale = 1;
            comboColorAlpha = 1;
            score += closeCallCombo * 100; 

            closeCallText.text = "Close call ! " + " +" + (closeCallCombo*100).ToString();
            
            comboFallRate.x = Random.Range(rangeFall.x/2, rangeFall.y);
            comboFallRate.y = Random.Range(Mathf.Abs(rangeFall.x), rangeFall.y);
            comboRotateRate = Random.Range(rangeRotate.x, rangeRotate.y);
        }

        comboTime -= Time.deltaTime;
        comboColorAlpha -= 0.02f;
        comboPos -= comboFallRate;
        comboRot -= comboRotateRate;
        comboScale -= 0.005f; 


        if (comboTime < 0)
        {
            if(maxCloseCallCombo < closeCallCombo) maxCloseCallCombo = closeCallCombo;
            closeCallCombo = 0;
            Debug.Log("Max combo : " + maxCloseCallCombo); 
        }
        closeCallText.color = new Color(1,1,1, comboColorAlpha);
        closeCallText.transform.position = new Vector3(comboPos.x, comboPos.y, 0);
        closeCallText.transform.rotation = new Quaternion(0, 0, comboRot, 180);
        closeCallText.transform.localScale = new Vector3(comboScale, comboScale, comboScale);
    }




    void TimeLeftIndicator()
    {
        levelTime -= Time.deltaTime;
        timeLeftText.text = new string("Time Left : ") + levelTime.ToString("00.00");

        timeLeftImage.transform.localScale = new Vector3(Mathf.Clamp(levelTime * timeBarMultiplier,0, 12.04f), 0.15f, 0);
    }

    void ScoreCalculator()
    {
        score += carController.speed;
        distance += carController.speed;
        scoreText.text = new string("Score : ") + score.ToString("000000.0");

        scoreImage.transform.localScale = new Vector3(Mathf.Clamp(score * scoreBarMultiplier,0,12.04f), 0.15f, 0);
    }


    void Speedometer()
    {
        kmh = carController.speed * 35;
        float kmhHandle = carController.speed * -35.5f;
        float revHandle = kmhHandle * 2 % 70;


        speedHandle.transform.rotation = Quaternion.Euler(0, 0, kmhHandle);
        rpmHandle.transform.rotation = Quaternion.Euler(0, 0, revHandle);


        speed.text = Mathf.RoundToInt(kmh).ToString();
    }
}

