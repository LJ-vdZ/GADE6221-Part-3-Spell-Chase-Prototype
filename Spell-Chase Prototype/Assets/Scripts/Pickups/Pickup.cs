using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using System;

public class Pickup : MonoBehaviour   //inherit from ObstaclePassedScore
{
    public int increaseSpeed = 2;
    private float speedCooldown = 40f;
    public static bool isSpeeding;
    public static bool isImmune;
    private float immunityTimer = 10f;
 
    public Death death;

    public PickupBar pickupBar;

    //to display pickup type in UI
    public Text pickupText;

    public static event Action GreenPotionPickup;
    public static event Action BluePotionPickup;
    public static event Action RedPotionPickup;



    // Start is called before the first frame update 
    /*void Start()
    {
        //pickupBar = FindObjectOfType<PickupBar>();
        
        //UpdateScoreInUI();

        isSpeeding = false;

        pickupText = GameObject.Find("PickupType").GetComponent<Text>();

        pickupText.text = "";
        
        //pickupBar = FindObjectOfType<PickupBar>();

    }*/

    //[System.Obsolete]
    // Update is called once per frame
    /*void Update()
    {
        //base.Update();   //base refers to ObstaclePassedScore class


        //check if player is immune
        if (isImmune == true)
        {
            immunityTimer -= Time.deltaTime;    //use count down to end pickup effect
            
            pickupBar.sliderValue(immunityTimer);
            
            //if count down reaches zero, stop effect of pickup, get rid of immunity

            if (immunityTimer <= 0f)
            {
                isImmune = false;
                //death.enabled = true;

                pickupBar = FindObjectOfType<PickupBar>();

                //get fill colour of slider
                Image fillImage = pickupBar.slider.fillRect.GetComponent<Image>();
                
                fillImage.color = Color.clear;

                pickupBar.setMaxSlider(immunityTimer);

                immunityTimer = 10f;    //set count down back to 10s
            }

        }
        
        //UpdateScoreInUI();
    }*/

    //[System.Obsolete]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("GreenPotion"))
            {
                GreenPotionPickup?.Invoke();
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("BluePotion"))
            {
                BluePotionPickup?.Invoke();
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("RedPotion"))
            {
                RedPotionPickup?.Invoke();
                Destroy(gameObject);
            }
        }
        /*if (other.gameObject.CompareTag("Player")) //if player collides with pickup
        {
            pickupBar = FindObjectOfType<PickupBar>();

            //get fill colour of slider
            Image fillImage = pickupBar.slider.fillRect.GetComponent<Image>();
            
            //check which pickup tag player collided with
            if (gameObject.CompareTag("GreenPotion") && isSpeeding == false && isImmune == false)   //green pickup tag
            {
                Destroy(gameObject);
                
                pickupBar.setMaxSlider(speedCooldown);
                
                ObstaclePassedScore.score = ObstaclePassedScore.score + 10; //boost player score

                pickupText.text = "Score Booster!";

                //change slider colour to green
                fillImage.color = Color.green;

            }

            //player collides with speed potion
            if (gameObject.CompareTag("BluePotion") && isImmune == false)   //speed boost
            {
                pickupBar = FindObjectOfType<PickupBar>();

                //make sure player can't pickup another speed potion while one is already active
                if (isSpeeding == false) 
                {

                    Destroy(gameObject);

                    pickupBar.setMaxSlider(speedCooldown);

                    GetComponent<MoveHallway>();  //use GetComponent() functionality

                    MoveHallway.ApplySpeed(increaseSpeed);//boost "player" forward speed by increasing hallway speed

                    isSpeeding = true;  //need boolean check for cooldown

                    pickupText.text = "Super Speed!";

                    //change slider colour to blue
                    fillImage.color = Color.blue;

                }
                else //is already speeding, just destroy pickup, do not activate effect
                {
                    Destroy(gameObject);

                }

            }

            //player collides with immunity potion
            if (gameObject.CompareTag("RedPotion") && isImmune == false) ////when "Obstacle" hit, destroy obstacle
            {

                Destroy(gameObject); 
                
                
                
                
                PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();

                if (playerStatus != null && playerStatus.isImmune == false)
                {
                    
                    playerStatus.isImmune = true;
                    
                    death.enabled = false;
                    pickupText.text = "Immunity!";

                    pickupBar.setMaxSlider(immunityTimer);

                    //change slider colour to red
                    fillImage.color = Color.red;
                }
                
                isImmune = playerStatus.isImmune;

            }

            Canvas.ForceUpdateCanvases(); //force Canvas to redraw


        }*/
    }
    
}
//REFERENCES
//Microsoft Learn, 2023. new modifier (C# Reference). [online] Available at: <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-modifier> [Accessed 21 March 2025].
//Geeks for Geeks, 2025. C# Method Overriding. [online] Available at: <https://www.geeksforgeeks.org/c-sharp-method-overriding/>[Accessed 21 March 2025].
//All Things Game Dev, 2022. What Exactly Is Get Component - Unity Tutorial. [video online] Available at: <https://youtu.be/n-bA2jUTW8k>{Accessed 21 Match 2025].