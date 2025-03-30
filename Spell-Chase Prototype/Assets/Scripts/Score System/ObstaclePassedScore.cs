using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //added for UI Text

public class ObstaclePassedScore : Death    //need to check if player died on collision. Must not include score of obstacle player collides with. 
{
    //set starting score to zero
    public static int score = 0;   //use static so that this applies to all other triggers

    //to display score in UI
    public Text scoreText;
   
    //check if obstacle passed the player, aka went through the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            score = score + 1;  //increase score if obastacle passed player

            UpdateScoreInUI();  //every time player passes object, update UI

            Debug.Log("Score: " + score);
        }
    }

    

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       
        //don't include collision with obstacle in score
        if (Death.deathStatus == true)  
        {
            Debug.Log("Player died");
            score = score - 1;

            UpdateScoreInUI();
            
        }
    }

    //update score on UI
    protected void UpdateScoreInUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            
            Debug.Log("Score Updated: " + score); //check if it works
        }
        //else
        //{
        //    Debug.LogError("ScoreText UI not working");
        //}
    }

    // Start is called before the first frame update
    private void Start()      //changed to protected to allow inheritance
    {
        score = 0;
        //select ScoreText UI to display score in
        GameObject scoreTextObj = GameObject.Find("ScoreText");
        

        if (scoreTextObj != null)   //check if UI was found
        {
            scoreText = scoreTextObj.GetComponent<Text>();
        }
       

        UpdateScoreInUI();

    }

    // Update is called once per frame
    protected void Update()
    {
        UpdateScoreInUI();
    }

   
}
