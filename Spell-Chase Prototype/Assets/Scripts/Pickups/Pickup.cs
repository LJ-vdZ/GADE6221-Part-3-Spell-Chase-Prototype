using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : ObstaclePassedScore   //inherit from ObstaclePassedScore
{
    public int increaseSpeed = 8;
    private float speedCooldownTime = 10f;
    public bool isSpeeding = false;

    // Start is called before the first frame update
    //change to new void Start(). Hiding/overriding base method. 
    void Start()
    {
        //call Start method from ObstaclePassedScore script to update UI
        //base.Start();   //base refers to ObstaclePassedScore class
        UpdateScoreInUI();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();   //base refers to ObstaclePassedScore class


        //check if player is using speedup
        if (isSpeeding == true)
        {
            speedCooldownTime -= Time.deltaTime;    //use count down to end pickup effect

            //if count down reaches zero, stop effect of pickup, speed set back to normal
            if (speedCooldownTime <= 0f)
            {
                isSpeeding = false;

                MoveHallway.hallwaySpeed = 5;

                speedCooldownTime = 10f;    //set count down back to 10s
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //if player collides with pickup
        {
            //check which pickup tag player collided with
            if (this.gameObject.CompareTag("GreenPotion"))   //green pickup tag
            {
                ObstaclePassedScore.score = score + 10; //boost player score

                UpdateScoreInUI();

                Destroy(gameObject);    

            }

            //player collides with speed potion
            if (this.gameObject.CompareTag("Speed Pickup"))
            {
                Destroy(gameObject);

                MoveHallway.hallwaySpeed *= increaseSpeed;  //boost "player" forward speed by increasing hallway speed 

                isSpeeding = true;  //need boolean check for cooldown
            }


        }
    }
}
//REFERENCES
//Microfsoft Learn, 2023. new modifier (C# Reference). [online] Available at: <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-modifier> [Accessed 21 March 2025].
//Geeks for Geeks, 2025. C# Method Overriding. [online] Available at: <https://www.geeksforgeeks.org/c-sharp-method-overriding/>[Accessed 21 March 2025].