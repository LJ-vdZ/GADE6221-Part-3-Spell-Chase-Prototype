using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

public class Pickup : ObstaclePassedScore   //inherit from ObstaclePassedScore
{
    public int increaseSpeed = 2;
    public float speedCooldown = 40f;
    public static bool isSpeeding;
    public static bool isImmune;
    public float immunityTimer = 10f;

    private GameObject barObject; //PickupBar GameObject
 
    public Death death;

    [SerializeField]
    public PickupBar pickupBar;

    //to display pickup type in UI
    public Text pickupText;


    [System.Obsolete]

    // Start is called before the first frame update 
    void Start()
    {
        pickupBar = FindObjectOfType<PickupBar>();
        
        UpdateScoreInUI();

        isSpeeding = false;

        pickupText = GameObject.Find("PickupType").GetComponent<Text>();

        pickupText.text = "";


    }

    // Update is called once per frame
    void Update()
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
                death.enabled = true;

                //get fill colour of slider
                Image fillImage = pickupBar.slider.fillRect.GetComponent<Image>();
                fillImage.color = Color.clear;

                immunityTimer = 10f;    //set count down back to 10s
            }

        }
        /*if (immunityTimer < 0f)
        {
            isImmune = false;
            death.enabled = true;
            immunityTimer = 10f;    //set count down back to 10s

            //get fill colour of slider
            Image fillImage = pickupBar.slider.fillRect.GetComponent<Image>();
            fillImage.color = Color.clear;
        }*/

        UpdateScoreInUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        //get fill colour of slider
        Image fillImage = pickupBar.slider.fillRect.GetComponent<Image>();

        if (other.gameObject.CompareTag("Player")) //if player collides with pickup
        {
            //check which pickup tag player collided with
            if (gameObject.CompareTag("GreenPotion") && isSpeeding == false && isImmune == false)   //green pickup tag
            {
                score = score + 10; //boost player score

                UpdateScoreInUI();

                pickupText.text = "Score Booster!";

                //change slider colour to green
                fillImage.color = Color.green;

                Canvas.ForceUpdateCanvases(); //force Canvas to redraw

                Destroy(gameObject);

            }

            //player collides with speed potion
            if (gameObject.CompareTag("BluePotion") && isImmune == false)   //speed boost
            {
                //make sure player can't pickup another speed potion while one is already active
                if(isSpeeding == false) 
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
            if (gameObject.CompareTag("RedPotion") /*&& isImmune == false*/) ////when "Obstacle" hit, destroy obstacle
            {
                PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();

                if (playerStatus != null && playerStatus.isImmune == false)
                {
                    playerStatus.isImmune = true;
                    death.enabled = false;
                    pickupText.text = "Immunity!";
                    //change slider colour to red
                    fillImage.color = Color.red;
                    pickupBar.setMaxSlider(immunityTimer);
                }
                
                isImmune = playerStatus.isImmune;


                Destroy(gameObject);
                
                //immunityTimer = 10f;
                
                /*pickupBar.setMaxSlider(immunityTimer);

                isImmune = true;

                /*if (death == null)
                {
                    death = GameObject.FindWithTag("Player").GetComponent<Death>();
                }*/
                
                /*death.enabled = false;


                pickupText.text = "Immunity!";

                //change slider colour to red
                fillImage.color = Color.red;*/


            }

            
        }
    }
    
}
//REFERENCES
//Microsoft Learn, 2023. new modifier (C# Reference). [online] Available at: <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-modifier> [Accessed 21 March 2025].
//Geeks for Geeks, 2025. C# Method Overriding. [online] Available at: <https://www.geeksforgeeks.org/c-sharp-method-overriding/>[Accessed 21 March 2025].
//https://youtu.be/n-bA2jUTW8k