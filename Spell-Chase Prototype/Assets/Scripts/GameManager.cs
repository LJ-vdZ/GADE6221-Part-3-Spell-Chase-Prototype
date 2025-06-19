using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject EndScreenUI;
    public bool reset = false;
    public Text finalScore;
    public Text ScoreText;

    // Pickup effect related variables
    public PickupBar pickupBar;
    public Text pickupText;

    public Death death; // reference to death script
    public MoveHallway moveHallway; // reference to MoveHallway script

    public int greenPotionScoreBoost = 10;
    public float bluePotionSpeedIncrease = 2f;
    public float speedCooldown = 40f;
    public float immunityCooldown = 10f;

    private bool isSpeeding = false;
    private bool isImmune = false;
    private float immunityTimer;
    private float speedTimer;

    public static GameManager Instance;

    private string playerName;
    private int gameNumber;
    private DatabaseReference refDatabase;

    [SerializeField] float Min;

    [SerializeField] float Max;

    public GameObject bossSpawner;
    public GameObject boss;
    private GameObject activeBoss;

    public GameObject spawnerOne, spawnerTwo, spawnerThree;
    public GameObject spawnerFour, spawnerFive, spawnerSix;

    private int completedLevels = 0;

    public DatabaseManager databaseManager;
    
    private int currentGameNumber = 0; //increment for each new game

    private int currentScore = 0;

    private bool hasDied = false; //new flag to prevent multiple death calls for OnPlayerDeath

    void Awake()
    {
        //singleton
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;

        gameNumber = PlayerPrefs.GetInt("GameNumber", 1);   //initialise game number

    }

    void Start()
    {
        Debug.Log("Scene restarted. GameManager Start() running.");

        if (pickupBar == null)
        {
            pickupBar = FindObjectOfType<PickupBar>();
        }
        
        //gameNumber = PlayerPrefs.GetInt("GameNumber", 1);   //initialise game number

        //ensure UI is initialised
        reset = false;

        EndScreenUI.SetActive(false);      // hide game over UI
        ScoreText.enabled = true;          // re-enable score display

        currentScore = 0;                  // reset score if needed
        finalScore.text = "";              // clear final score text

        hasDied = false;

        Time.timeScale = 1f;

        death.enabled = true;

        moveHallway.enabled = true;


    }

    // Update is called once per frame
    void Update() // Checks for death and shows final score
    {
        HandlePickupTimers();

        //if (Death.deathStatus == true && !hasDied) //check flag
        //{
        //    endGame();
        //    finalScore.text = "Score: " + score;
        //    currentGameNumber++;

        //}

        if (reset == true)
        {
            EndScreenUI.SetActive(false);
            reset = false;
            
        }

        //currentScore = score;
    }

    public void endGame() //Brings up end game screen
    {
        ScoreText.enabled = false;
        EndScreenUI.SetActive(true);

        //save data when player dies
        OnPlayerDeath();

    }

    public void restartGame() //Function to reset entire level of game
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        EndScreenUI.SetActive(false);

        hasDied = false;
        EndScreenUI.SetActive(false);
        ScoreText.enabled = true;
        finalScore.text = "";

        currentScore = 0;
        ObstaclePassedScore.score = 0;

        death.enabled = true;
        moveHallway.enabled = true;
    }

    public void quit() //Function to exit the application
    {
        ////save data when player dies
        //OnPlayerDeath();

        Application.Quit();
    }

    void OnEnable()
    {
        Pickup.GreenPotionPickup += HandleGreenPotionPickup;
        Pickup.BluePotionPickup += HandleBluePotionPickup;
        Pickup.RedPotionPickup += HandleRedPotionPickup;

        ObstaclePassedScore.ScoreIncreased += HandleScoreIncreased;
        ObstaclePassedScore.ScoreDecreased += HandleScoreDecreased;
        ObstaclePassedScore.PlayerCollision += HandlePlayerDeath;

        Boss.BossSpawned += HandleBossSpawn;
        Boss.BossDespawned += HandleBossDespawn;
        Boss.LevelIncreased += HandleLevelIncrease;
    }

    void OnDisable()
    {
        Pickup.GreenPotionPickup -= HandleGreenPotionPickup;
        Pickup.BluePotionPickup -= HandleBluePotionPickup;
        Pickup.RedPotionPickup -= HandleRedPotionPickup;

        ObstaclePassedScore.ScoreIncreased -= HandleScoreIncreased;
        ObstaclePassedScore.ScoreDecreased -= HandleScoreDecreased;
        ObstaclePassedScore.PlayerCollision -= HandlePlayerDeath;

        Boss.BossSpawned -= HandleBossSpawn;
        Boss.BossDespawned -= HandleBossDespawn;
        Boss.LevelIncreased -= HandleLevelIncrease;
    }

    private void HandleBossSpawn()
    {
        //Turn off normal spawners
        spawnerOne.SetActive(false);
        spawnerTwo.SetActive(false);
        spawnerThree.SetActive(false);

        //Turn on boss attack spawners
        spawnerFour.SetActive(true);
        spawnerFive.SetActive(true);
        spawnerSix.SetActive(true);

        /*float wantedX = transform.position.x + UnityEngine.Random.Range(Min, Max);    //transform.position.x is the x position of the spawner. Ensures obstacles spawn within the spawning range at the x position of spawner
        Vector3 position = new Vector3(wantedX, transform.position.y, transform.position.z);    //included z position so that obstacles spawn at z position of spawners
        Quaternion rotation = Quaternion.Euler(0, 180, 0);  // Rotates boss to face front*/

        // Use the bossSpawner's position and rotation
        Vector3 spawnPosition = bossSpawner.transform.position;
        Quaternion spawnRotation = Quaternion.Euler(0, 180f, 0);
        activeBoss = Instantiate(boss, spawnPosition, spawnRotation);
    }

    private void HandleBossDespawn()
    {
        if (activeBoss != null)
        {
            Destroy(activeBoss);
            activeBoss = null;
        }

        // Reset spawners
        spawnerOne.SetActive(true);
        spawnerTwo.SetActive(true);
        spawnerThree.SetActive(true);
        spawnerFour.SetActive(false);
        spawnerFive.SetActive(false);
        spawnerSix.SetActive(false);
    }

    private void HandleLevelIncrease()
    {
        completedLevels++;
    }

    public void OnPlayerDeath()
    {
        currentGameNumber++;

        Debug.Log("Before saving: " + currentGameNumber);

        //Debug.Log($"Saving - Score: {currentScore}, GameNumber: {currentGameNumber}");


        PlayerPrefs.SetInt("GameNumber", currentGameNumber); //save game number locally, PlayerPrefs to remember number
        PlayerPrefs.Save(); //make sure game number is saved

        Debug.Log("Saved to PlayerPrefs: " + PlayerPrefs.GetInt("GameNumber"));

        databaseManager.SaveGameData(currentScore, currentGameNumber);
        
    }

    private void HandleScoreIncreased(int score)
    {
        currentScore = score;
        ScoreText.text = "Score: " + currentScore;
    }

    private void HandleScoreDecreased(int score)
    {
        currentScore = score;
        ScoreText.text = "Score: " + currentScore;
    }

    private void HandlePlayerDeath()
    {
        if (!hasDied)
        {
            finalScore.text = "Score: " + currentScore;
            ScoreText.enabled = false;
            EndScreenUI.SetActive(true);
            hasDied = true;

            OnPlayerDeath();

        }
    }

    private void HandleGreenPotionPickup()
    {
        ObstaclePassedScore.score += greenPotionScoreBoost;
        UpdateScoreUI();

        pickupBar.setMaxSlider(speedCooldown);
        SetPickupUI("Score Booster!", Color.green);
    }

    private void HandleBluePotionPickup()
    {
        if (isImmune) return;  // Don't apply speed if immune (optional logic)

        if (!isSpeeding)
        {
            isSpeeding = true;
            speedTimer = speedCooldown;

            MoveHallway.ApplySpeed(bluePotionSpeedIncrease);

            pickupBar.setMaxSlider(speedCooldown);
            SetPickupUI("Super Speed!", Color.blue);
        }
    }

    private void HandleRedPotionPickup()
    {
        Debug.Log("Red Potion Event Triggered");
        if (!isImmune)
        {
            isImmune = true;
            immunityTimer = immunityCooldown;

            death.enabled = false;  // Disable death script during immunity

            pickupBar.setMaxSlider(immunityCooldown);
            SetPickupUI("Immunity!", Color.red);
        }
    }

    private void HandlePickupTimers()
    {
        if (isSpeeding)
        {
            speedTimer -= Time.deltaTime;
            pickupBar.sliderValue(speedTimer);

            if (speedTimer <= 0f)
            {
                isSpeeding = false;
                MoveHallway.ResetSpeed(); // You will need a method to reset speed to normal

                pickupBar.ClearSlider();
                ClearPickupUI();
            }
        }

        if (isImmune)
        {
            immunityTimer -= Time.deltaTime;
            Debug.Log($"Immunity timer: {immunityTimer}");
            pickupBar.sliderValue(immunityTimer);

            if (immunityTimer <= 0f)
            {
                isImmune = false;
                death.enabled = true;

                pickupBar.ClearSlider();
                ClearPickupUI();
            }
        }
    }

    public bool IsImmune()
    {
        return isImmune;
    }

    private void SetPickupUI(string text, Color color)
    {
        if (pickupText != null)
        {
            pickupText.text = text;
        }
        if (pickupBar != null)
        {
            Image fillImage = pickupBar.slider.fillRect.GetComponent<Image>();
            fillImage.color = color;
        }
    }

    private void ClearPickupUI()
    {
        if (pickupText != null)
        {
            pickupText.text = "";
        }
        if (pickupBar != null)
        {
            Image fillImage = pickupBar.slider.fillRect.GetComponent<Image>();
            fillImage.color = Color.clear;
        }
    }

    private void UpdateScoreUI()
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score: " + ObstaclePassedScore.score;
        }
    }

    //public void SetGameNumber(int gameNumber)
    //{
    //    currentGameNumber = gameNumber;
    //}


}
