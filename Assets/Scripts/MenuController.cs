using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    //Outlets
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject levelMenu;
    public GameObject highScoresMenu;

    public Text[] nameTexts;
    public Text[] scoreTexts;
    public string[] scoreIds = { "score1", "score2", "score3", "score4", "score5" };

    //Methods
    private void Awake()
    {
        instance = this;
        Hide();
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++) {
            //string theScoreText = "score" + i;
            scoreTexts[i].text = PlayerPrefs.GetString(scoreIds[i], MenuController.instance.scoreTexts[i].text);
        }
    }

    private void Update()
    {
        if (optionsMenu.activeSelf == true)
        {
            if (Input.GetKey(KeyCode.P))
            {
                ResetScore();
            }
        }
        for (int i = 0; i < 5; i++)
        {
            //string theScoreText = "score" + i;
            scoreTexts[i].text = PlayerPrefs.GetString(scoreIds[i], MenuController.instance.scoreTexts[i].text);
        }


    }

    void SwitchMenu(GameObject menu) {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);
        highScoresMenu.SetActive(false);


        menu.SetActive(true);
    }

    public void ShowMainMenu() {
        SwitchMenu(mainMenu);
    }
    public void ShowOptionsMenu() {
        SwitchMenu(optionsMenu);
    }
    public void ShowLevelMenu() {
        SwitchMenu(levelMenu);
    }
    public void ShowHighScoresMenu() {
        SwitchMenu(highScoresMenu);

    }
    public void Show() {
        GameController.instance.isPaused = true;
        ShowMainMenu();
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void Hide() {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        //if(PlayerController.instance.GetComponent<PlayerController>() != null) {
            //GameController.instance.isPaused = false;
        //}
    }

    public void ResetScore() {
        PlayerPrefs.DeleteKey("Score");
        GameController.instance.score = 0;
        PlayerPrefs.DeleteKey("Money");
        GameController.instance.money = 0;
        PlayerPrefs.DeleteKey("Health");
        GameController.instance.health = 200f;
    }

    public void SaveHighScore() {
        int highScore = GameController.instance.score;
        int temp = 0;
        for (int i = 0; i < 5; i++) {
            string currScoreText = scoreTexts[i].text;

            int currScore = int.Parse(currScoreText);
            if ((highScore > currScore) && (highScore > 0)) {
                //string theScoreId = "score" + i;
                temp = int.Parse(PlayerPrefs.GetString(scoreIds[i], highScore.ToString()));
                PlayerPrefs.SetString(scoreIds[i], highScore.ToString());
                //scoreTexts[i].text = highScore.ToString();
                highScore = 0;
                GameController.instance.updateText.text = "Your high score has been saved";
                Debug.Log("Saved high score");
            }
            if((temp > 0) && ((i+1) < 5)) {

                //int id = i + 1;
                //string theScoreId = "score" + id;
                int newTemp = int.Parse(PlayerPrefs.GetString(scoreIds[i+1], highScore.ToString()));
                PlayerPrefs.SetString(scoreIds[i+1], temp.ToString());
                //scoreTexts[i].text = temp.ToString();
                temp = newTemp;
            }

        }
        if (highScore > 0) {
            GameController.instance.updateText.text = "Try again next time";
            Debug.Log("Score was not high enough to become a high score");
        }
    }

    public void LoadLevel(int i) {
        if (i == 0) {
            SceneManager.LoadScene("SampleScene");
        }
        else if (i == 1) {
            SceneManager.LoadScene("Level2");
        }
        else {
            SceneManager.LoadScene("TestZoneRPGCharacter");
            Debug.Log("Load Level 3");
        }
    }
}
