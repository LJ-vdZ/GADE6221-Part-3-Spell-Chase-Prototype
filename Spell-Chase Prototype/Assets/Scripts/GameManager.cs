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
        //initialize Firebase
        refDatabase = FirebaseDatabase.DefaultInstance.RootReference;

        //load player data
        playerName = PlayerPrefs.GetString("CurrentPlayerName", "Unknown");
        gameNumber = PlayerPrefs.GetInt("GameNumber", 0);

        //ensure UI is initialised
        EndScreenUI.SetActive(false);
        ScoreText.enabled = true;
    }

    // Update is called once per frame
    void Update() // Checks for death and shows final score
    {
        if(Death.deathStatus == true) 
        {
            endGame();
            finalScore.text = "Score: " + score;
        }
        if(reset == true)
        {
            EndScreenUI.SetActive(false);
            reset = false;
            deathStatus = false;
        }
    }

    public void endGame() //Brings up end game screen
    {
        ScoreText.enabled = false;
        EndScreenUI.SetActive(true); 
    }

    public void restartGame() //Function to reset entire level of game
    {
        reset = true;


        //increment game number for next game
        PlayerPrefs.SetInt("GameNumber", gameNumber + 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit() //Function to exit the application
    {
        Application.Quit();
    }

 
}
