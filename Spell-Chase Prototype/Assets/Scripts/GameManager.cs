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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit() //Function to exit the application
    {
        Application.Quit();
    }

 
}
