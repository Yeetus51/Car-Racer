using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    RectTransform container; 

    [SerializeField]
    public RectTransform self; 

    UIElements uiInfo;

    [SerializeField]
    TextMeshProUGUI windowInfo; 

    [SerializeField]
    TextMeshProUGUI levelCompletionPoints;

    [SerializeField]
    TextMeshProUGUI distanceScore;

    [SerializeField]
    TextMeshProUGUI speedingScore;

    [SerializeField]
    TextMeshProUGUI closeCallScore;

    [SerializeField]
    TextMeshProUGUI wrongWayScore;

    [SerializeField]
    TextMeshProUGUI totalScore;


    [SerializeField]
    Button menuButton;

    [SerializeField]
    Button restartButton;

    [SerializeField]
    Button continueButton;


    public bool successful;
    public string reason;

    float delay;
    float frames;
    float time;

    float delay2 = 50;
    float frames2; 
    float time2;

    float containerPosMult;
    float containerSclMult;
    float textPosMult;
    float textTrnsMult;



    public GameOver(bool pSuccessful, string pReason)
    {
        successful = pSuccessful;
        reason = pReason;
    }
    // REASONS : Crashed , Finished , Paused


    // Start is called before the first frame update
    void Start()
    {
        containerPosMult = 500 / 9.5f;
        containerSclMult = 1 / 9.5f * Screen.width/1920;
        textPosMult = 50 / 9.5f;
        textTrnsMult = 1 / 9.5f;

        if(reason != "PAUSED")
        {
            container.anchoredPosition = new Vector3(0, 500, 0);
            container.transform.localScale = new Vector3(0, 0, 0);
            levelCompletionPoints.color = new Color(0, 0, 0, 0);
            distanceScore.color = new Color(0, 0, 0, 0);
            speedingScore.color = new Color(0, 0, 0, 0);
            closeCallScore.color = new Color(0, 0, 0, 0);
            wrongWayScore.color = new Color(0, 0, 0, 0);
            totalScore.color = new Color(0, 0, 0, 0);
        }





        if (GetComponentInParent<UIElements>() != null)
        {
            uiInfo = GetComponentInParent<UIElements>();
        }


        switch (reason)
        {

            case "CRASHED":
                windowInfo.text = "You Crashed ! Try AGAIN ! ";
                windowInfo.color = new Color(1, 0, 0);

                delay = 2f; 


                break;

            case "FINISHED":
                windowInfo.text = "Level Complete !  Great Job ! ";
                windowInfo.color = new Color(0, 1, 0);


                int levelCompletion = Mathf.RoundToInt(uiInfo.levelScore);
                int distance = Mathf.RoundToInt(uiInfo.distance / 50);
                int speeding = Mathf.RoundToInt(uiInfo.distance / 10);
                int closeCall = Mathf.RoundToInt(uiInfo.maxCloseCallCombo * 10);
                int wrongWay = Mathf.RoundToInt(uiInfo.wrongWayScore / 3);
                int total = levelCompletion + distance + speeding + closeCall + wrongWay;

                levelCompletionPoints.text = "Level completion : " + levelCompletion + "$";
                distanceScore.text = "Distance : " + distance + "$";
                speedingScore.text = "Speeding : " + speeding + "$";
                closeCallScore.text = "Max combo : " + closeCall + "$";
                wrongWayScore.text = "Wrong way : " + wrongWay + "$";
                totalScore.text = "Total : " + total + "$";

                GameInfo.psPlayerMoney += total;
                if(GameInfo.psCurrentLevel == GameInfo.psMaxLevel) GameInfo.psMaxLevel += 1;

                delay = 0.5f; 

                break;

            case "PAUSED":
                windowInfo.text = "Game Paused ";
                windowInfo.color = new Color(0.7f, 0.7f, 0.7f);

                delay = 0; 

                break;

            case "TIMEOUT":
                windowInfo.text = "You ran out of Time!";
                windowInfo.color = new Color(1, 0, 0);
                break;

        }

        menuButton.onClick.AddListener(DestroyOnClick);
        restartButton.onClick.AddListener(DestroyOnClick);
        continueButton.onClick.AddListener(DestroyOnClick);
        menuButton.onClick.AddListener(GoToMenu);
        restartButton.onClick.AddListener(Restart);
        continueButton.onClick.AddListener(Continue);
    }

    void DestroyOnClick()
    {
        Destroy(this.gameObject);
    }

    void GoToMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("StartScreen");
    }

    void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); 
    }
    void Continue()
    {
        if (reason == "PAUSED") Time.timeScale = 1;
        else SceneManager.LoadScene("ModeSelection");
    }

    void Animations()
    {
        // text start pos = (137,171)
        // text movementY = 52



        frames++;
        time += Mathf.Exp(-0.1f * frames);


        float containerPos = time * containerPosMult;
        float containerScl = time * containerSclMult;

        container.anchoredPosition = new Vector3(0, 500 - containerPos, 0); 
        container.transform.localScale = new Vector3(containerScl, containerScl, containerScl);

        Debug.Log("BRHSGHJADs  Should be 1 :  " + containerScl);

        if(delay < -0.5f)
        {
            frames2++;
            time2 += Mathf.Exp(-0.1f * frames2);

            float textPos = time2 * textPosMult;
            float textTransparency = time2 * textTrnsMult;

            levelCompletionPoints.rectTransform.anchoredPosition = new Vector3(137, 171 - textPos, 0);
            distanceScore.rectTransform.anchoredPosition = new Vector3(137, 171 - textPos * 2, 0);
            speedingScore.rectTransform.anchoredPosition = new Vector3(137, 171 - textPos * 3, 0);
            closeCallScore.rectTransform.anchoredPosition = new Vector3(137, 171 - textPos * 4, 0);
            wrongWayScore.rectTransform.anchoredPosition = new Vector3(137, 171 - textPos * 5, 0);
            totalScore.rectTransform.anchoredPosition = new Vector3(137, 171 - textPos * 6, 0);

            levelCompletionPoints.color = new Color(0, 1, 0, textTransparency);
            distanceScore.color = new Color(0, 1, 0, textTransparency);
            speedingScore.color = new Color(0, 1, 0, textTransparency);
            closeCallScore.color = new Color(0, 1, 0, textTransparency);
            wrongWayScore.color = new Color(0, 1, 0, textTransparency);
            totalScore.color = new Color(0, 1, 0, textTransparency);
        }




    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && reason == "PAUSED")
        {
            DestroyOnClick();
            Time.timeScale = 1;
        }
    }


    private void FixedUpdate()
    {
        delay -= Time.deltaTime;

        if(delay < 0 && reason != "PAUSED")
        {
            Animations();
        }



    }

}
