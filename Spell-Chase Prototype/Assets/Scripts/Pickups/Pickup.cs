using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

public class Pickup : ObstaclePassedScore   //inherit from ObstaclePassedScore
{
    public int increaseSpeed = 2;
    //public float speedCooldownTimeBar = 20f;
    public static bool isSpeeding;
    public bool isImmune = false;
    private float immunityTimer = 40f;


    private GameObject barObject; //PickupBar GameObject
 
    public Death death;

    public VisualEffect poof;

    [SerializeField]
    public PickupBar pickupBar;

    //to display pickup type in UI
    public Text pickupText;


    // Start is called before the first frame update 
    void Start()
    {
        pickupBar = FindObjectOfType<PickupBar>();
        
        UpdateScoreInUI();

        isSpeeding = false;

        pickupText = GameObject.Find("PickupType").GetComponent<Text>();

        pickupText.text = "";

        //barObject.SetActive(false);

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();   //base refers to ObstaclePassedScore class

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

                pickupText.text = "Score Booster!";

                Canvas.ForceUpdateCanvases(); // Force Canvas to redraw

                Destroy(gameObject);

            }

            //player collides with speed potion
            if (gameObject.CompareTag("BluePotion"))   //speed boost
            {
                //make sure player can't pickup another speed potion while one is already active
                if(isSpeeding == false) 
                {
                    Destroy(gameObject);
                    
                    GetComponent<MoveHallway>();  //use GetComponent() functionality

                    MoveHallway.ApplySpeed(increaseSpeed);//boost "player" forward speed by increasing hallway speed

                    Pickup.isSpeeding = true;  //need boolean check for cooldown

                    pickupText.text = "Super Speed!";

                }
                else //is already speeding, just destroy pickup, do not activate effect
                {
                    Destroy(gameObject);

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

                pickupText.text = "Immunity!";

                barObject.SetActive(true);

            }
        }
    }
}
//REFERENCES
//Microsoft Learn, 2023. new modifier (C# Reference). [online] Available at: <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-modifier> [Accessed 21 March 2025].
//Geeks for Geeks, 2025. C# Method Overriding. [online] Available at: <https://www.geeksforgeeks.org/c-sharp-method-overriding/>[Accessed 21 March 2025].
//https://youtu.be/n-bA2jUTW8k