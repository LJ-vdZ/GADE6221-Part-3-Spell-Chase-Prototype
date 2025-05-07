using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Pickup : ObstaclePassedScore   //inherit from ObstaclePassedScore
{
    public int increaseSpeed = 2;
    private float speedCooldownTime = 3f;
    public bool isSpeeding = false;
    public bool isImmune = false;
    private float immunityTimer = 10f;
    private float originalSpeed = 5f;
    public float DontIncreaseSpeed = 1f;

    public Death death;

    public VisualEffect poof;

    // Start is called before the first frame update
    //change to new void Start(). Hiding/overriding base method. 
    void Start()

    {
        //call Start method from ObstaclePassedScore script to update UI
        //base.Start();   //base refers to ObstaclePassedScore class
        UpdateScoreInUI();

        //GetComponent<MoveHallway>();

        originalSpeed = MoveHallway.hallwaySpeed;

        isSpeeding = false;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();   //base refers to ObstaclePassedScore class


        //check if player is using speedup
        StopSpeedEffect();

        //check if player is immune
        if (isImmune == true)
        {
            immunityTimer -= Time.deltaTime;    //use count down to end pickup effect

            //if count down reaches zero, stop effect of pickup, get rid of immunity
            if (immunityTimer <= 0f)
            {
                isImmune = false;
                death.enabled = true;
                immunityTimer = 10f;    //set count down back to 10s
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //if player collides with pickup
        {
            
            //check which pickup tag player collided with
            if (gameObject.CompareTag("GreenPotion"))   //green pickup tag
            {
                score = score + 10; //boost player score

                UpdateScoreInUI();

                Destroy(gameObject);    

            }

            //player collides with speed potion
            if (gameObject.CompareTag("BluePotion"))   //speed boost
            {
                MoveHallway moveHallway = GetComponent<MoveHallway>();  //use GetComponent() functionality
                //make sure player can't pickup another speed potion while one is already active

                if(isSpeeding == false) 
                {
                    Destroy(gameObject);

                    moveHallway.ApplySpeed(increaseSpeed);//boost "player" forward speed by increasing hallway speed

                    isSpeeding = true;  //need boolean check for cooldown

                }
                else //is already speeding, just destroy pickup, do not activate effect
                {
                    Destroy(gameObject);

                    moveHallway.ApplySpeed(DontIncreaseSpeed);

                }

            }

            //player collides with immunity potion
            if(gameObject.CompareTag("RedPotion")) ////when "Obstacle" hit, destroy obstacle
            {
                death.enabled = false;
                
                Destroy(gameObject);
                if (poof != null)
                {
                    poof.Play();
                }
            }


        }
    }

    public void StopSpeedEffect()
    {
        if (isSpeeding == true)
        {
            speedCooldownTime -= Time.deltaTime;

            Debug.Log("Speed Cooldown count down started");

            if (speedCooldownTime <= 0f)
            {
                isSpeeding = false;

                GetComponent<MoveHallway>().EndSpeedBoost(originalSpeed);

                //moveHallway.hallwaySpeed = originalSpeed;

                speedCooldownTime = 3f;
            }
        }
    }
}
//REFERENCES
//Microsoft Learn, 2023. new modifier (C# Reference). [online] Available at: <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-modifier> [Accessed 21 March 2025].
//Geeks for Geeks, 2025. C# Method Overriding. [online] Available at: <https://www.geeksforgeeks.org/c-sharp-method-overriding/>[Accessed 21 March 2025].
//https://youtu.be/n-bA2jUTW8k