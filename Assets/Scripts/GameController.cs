using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public enum Perspective {
        FPS,
        Overview,
        ThirdPerson
    }

    //Outlets
    public GameObject enemy;
    public GameObject characterFPS;
    public GameObject characterOverview;
    public GameObject character;
    public GameObject padlockClosed;
    public GameObject padlockOpen;

    public GameObject FreeLookCamera;

    //Configuration
    public float enemySpawnDelay;
    public Transform[] spawnPoints;
    public Text scoreUI;
    public Text moneyText;
    public Text updateText;
    public Image[] heartImages;
    public int scoreNeeded;

    //State Tracking
    public float health;
    public float healthMax;
    public bool canFight;
    public List<Enemy> enemies;
    public int numberOfEnemies;
    public bool reachedFinalDest;
    public float timeSinceLastEnemy;
    public int score;
    public int money;
    public Perspective currentPerspective;
    public bool boxing;
    public bool isPaused;
    public int maxNumberOfEnemies;
    public bool obtainedKey;


    public Color[] heartColors;
    

    void Awake() {
        instance = this;
    }

    private void Start()
    {
        padlockOpen.SetActive(false);
        padlockClosed.SetActive(true);
        score = PlayerPrefs.GetInt("Score", GameController.instance.score);
        money = PlayerPrefs.GetInt("Money", GameController.instance.money);
        scoreUI.text = score.ToString();
        moneyText.text = "$" + money.ToString();
        health = PlayerPrefs.GetFloat("Health", GameController.instance.health);
        SwitchToThirdPerson();
        updateText.text = "Are you ready?";
        Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        numberOfEnemies = 1;
    }

    void UpdateDisplay() {
        scoreUI.text = GameController.instance.score.ToString();
        moneyText.text = "$" + GameController.instance.money.ToString();
        float currHealth = health / healthMax;
        if (currHealth < 0.67)
        {
            heartColors[2].a = 0;

            if (currHealth < 0.34)
            {
                heartColors[1].a = 0;
                if (currHealth == 0)
                {
                    heartColors[0].a = 0;
                }
                else
                {
                    heartColors[0].a = 1;
                }
            }
            else
            {
                heartColors[1].a = 1;
            }
        }
        else
        {
            heartColors[2].a = 1;
        }
        for (int i = 0; i < 3; i++) {
            heartImages[i].color = heartColors[i];
        }
    }

    public void EarnPoints(int pointAmount) {
        //score += pointAmount;
        //money += pointAmount;
        GameController.instance.money += pointAmount;
        PlayerPrefs.SetInt("Money", GameController.instance.money);
        GameController.instance.score += pointAmount;
        PlayerPrefs.SetInt("Score", GameController.instance.score);
    }

    // Update is called once per frame
    void Update()
    {

        if (reachedFinalDest == true)
        {
            PlayerController.instance.Win();
            return;
        }

        timeSinceLastEnemy += Time.deltaTime;
        if(timeSinceLastEnemy >= enemySpawnDelay) {
            timeSinceLastEnemy = 0;
            enemySpawnDelay += 0.9f;
            if(enemySpawnDelay < 1f) {
                enemySpawnDelay = 1f;
            }
            if (numberOfEnemies < maxNumberOfEnemies)
            {
                ++numberOfEnemies;
                Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if(currentPerspective == Perspective.Overview) {
                SwitchToFPS();
            } else if(currentPerspective == Perspective.FPS) {
                SwitchToThirdPerson();
            }
            else if(currentPerspective == Perspective.ThirdPerson) {
                SwitchToOverview();
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            MenuController.instance.Show();
        }

        UpdateDisplay();

        if (currentPerspective == Perspective.Overview) {
            if (Input.GetMouseButtonDown(0))
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch); //may need to change
                //change money
                //make player invisible to enemies
            }
        }



    }

    void SwitchToOverview() {
        currentPerspective = Perspective.Overview;
        Vector3 currPosition = character.transform.position;

        characterFPS.SetActive(false);
        FreeLookCamera.SetActive(false);
        character.SetActive(true);
        character.transform.position = currPosition;
        characterOverview.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void SwitchToFPS() {
        currentPerspective = Perspective.FPS;
        Vector3 currPosition = character.transform.position;
        characterOverview.SetActive(false);
        FreeLookCamera.SetActive(false);
        character.SetActive(false);
        characterFPS.SetActive(true);
        characterFPS.transform.position = currPosition;
        Cursor.visible = false;
    }

    void SwitchToThirdPerson()
    {
        currentPerspective = Perspective.ThirdPerson;
        Vector3 currPosition = characterFPS.transform.position;
        characterFPS.SetActive(false);
        characterOverview.SetActive(false);
        FreeLookCamera.SetActive(true);
        character.SetActive(true);
        character.transform.position = currPosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void updateFinalDest()
    {
        if (obtainedKey) {
            if (score > scoreNeeded) {
                padlockClosed.SetActive(false);
                padlockOpen.SetActive(true);
                updateText.text = "You have unlocked the door";
                reachedFinalDest = true;
            }
            else {
                updateText.text = "You need more points to unlock";
            }
        }


    }

    public void updateFightAbility()
    {
        canFight = true;
    }
}
