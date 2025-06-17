using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class GameManager : ObstaclePassedScore
{
    public GameObject EndScreenUI;
    public bool reset = false;
    public Text finalScore;
    public Text ScoreText;

    public static GameManager Instance;

    private string playerName;
    private int gameNumber;
    private DatabaseReference refDatabase;

    [SerializeField] float Min;

    [SerializeField] float Max;

    public GameObject boss;
    private GameObject activeBoss;

    public GameObject spawnerOne, spawnerTwo, spawnerThree;
    public GameObject spawnerFour, spawnerFive, spawnerSix;

    private int completedLevels = 0;

    public DatabaseManager databaseManager;
    
    private int currentGameNumber = 1; //increment for each new game

    private int currentScore = 0;

    private bool hasDied = false; //new flag to prevent multiple death calls for OnPlayerDeath

    void Awake()
    {
        //singleton
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        ////initialize Firebase
        //refDatabase = FirebaseDatabase.DefaultInstance.RootReference;

        ////load player data
        //playerName = PlayerPrefs.GetString("CurrentPlayerName", "Unknown");

        gameNumber = PlayerPrefs.GetInt("GameNumber", 1);   //initialise game number

        //ensure UI is initialised
        EndScreenUI.SetActive(false);
        ScoreText.enabled = true;

        hasDied = false; //reset on start
    }

    // Update is called once per frame
    void Update() // Checks for death and shows final score
    {
        if(Death.deathStatus == true && !hasDied) //checl flag
        {
            endGame();
            finalScore.text = "Score: " + score;
            currentGameNumber++;

        }
        if(reset == true)
        {
            EndScreenUI.SetActive(false);
            reset = false;
            deathStatus = false;
            hasDied = false; //reset flag
        }

        currentScore = score;
    }

    public void endGame() //Brings up end game screen
    {
        ScoreText.enabled = false;
        EndScreenUI.SetActive(true);

        //save data when player dies
        OnPlayerDeath();

        hasDied = true; //set flag to prevent more calls
    }

    public void restartGame() //Function to reset entire level of game
    {
        reset = true;

        ////save data when player dies
        //OnPlayerDeath();

        ////increment game number for next game
        //PlayerPrefs.SetInt("GameNumber", currentGameNumber);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit() //Function to exit the application
    {
        ////save data when player dies
        //OnPlayerDeath();

        Application.Quit();
    }

    void OnEnable()
    {
        Boss.BossSpawned += HandleBossSpawn;
        Boss.BossDespawned += HandleBossDespawn;
        Boss.LevelIncreased += HandleLevelIncrease;
    }

    void OnDisable()
    {
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

        //Turn on boss spawners
        spawnerFour.SetActive(true);
        spawnerFive.SetActive(true);
        spawnerSix.SetActive(true);

        float wantedX = transform.position.x + UnityEngine.Random.Range(Min, Max);    //transform.position.x is the x position of the spawner. Ensures obstacles spawn within the spawning range at the x position of spawner
        Vector3 position = new Vector3(wantedX, transform.position.y, transform.position.z);    //included z position so that obstacles spawn at z position of spawners
        Quaternion rotation = Quaternion.Euler(0, 180, 0);  // Rotates boss to face front
        Instantiate(boss, position, rotation);
    }

    private void HandleBossDespawn()
    {
        if (boss != null)
        {
            Destroy(boss);
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
        
        Debug.Log($"Saving - Score: {currentScore}, GameNumber: {currentGameNumber}");

        databaseManager.SaveGameData(currentScore, currentGameNumber);
        
    }

    //public void SetGameNumber(int gameNumber)
    //{
    //    currentGameNumber = gameNumber;
    //}


}
