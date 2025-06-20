using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //added for UI Text
using System;

public class ObstaclePassedScore : MonoBehaviour    //need to check if player died on collision. Must not include score of obstacle player collides with. 
{
    public static event Action<int> ScoreIncreased;
    public static event Action<int> ScoreDecreased;
    public static event Action PlayerCollision;

    public static void RaisePlayerCollision()
    {
        PlayerCollision?.Invoke();
    }
    //set starting score to zero
    public static int score = 0;   //use static so that this applies to all other triggers

    //to display score in UI
    /*public Text scoreText;*/

    private Death death;
   
    //check if obstacle passed the player, aka went through the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("FloorTrap"))
        {
            score = score + 1;  //increase score if obastacle passed player

            //UpdateScoreInUI();  //every time player passes object, update UI
            ScoreIncreased?.Invoke(score);

            Debug.Log("Score: " + score);
        }
    }

    //private void //OnControllerColliderHit(ControllerColliderHit hit)
    //{

    //    //don't include collision with obstacle in score
    //    if (Death.deathStatus == true)
    //    {
    //        Debug.Log("Player died");
    //        score = score - 1;

    //        //UpdateScoreInUI();
    //        ScoreDecreased?.Invoke(score);
    //        PlayerCollision?.Invoke();

    //    }
    //}

    //update score on UI
    /*protected void UpdateScoreInUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            
            //(it does work) Debug.Log("Score Updated: " + score); //check if it works
        }
        
    }*/

    // Start is called before the first frame update
    private void Start()      //changed to protected to allow inheritance
    {
        death = FindObjectOfType<Death>();
        //score = 0;
        //select ScoreText UI to display score in
        GameObject scoreTextObj = GameObject.Find("ScoreText");
        

        /*if (scoreTextObj != null)   //check if UI was found
        {
            scoreText = scoreTextObj.GetComponent<Text>();
        }
       
        UpdateScoreInUI();*/

    }

    // Update is called once per frame
   /* protected void Update()
    {
        UpdateScoreInUI();
    }*/

   
}
