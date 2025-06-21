using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    Animator playerAnim;
    private CharacterController controller;

    //set distance between lanes
    public float laneDistance = 1.5f;

    //set speed of movement
    public float moveSpeed = 10f;

    //set jumping force. 10f to overcome a bit of the gravity effect
    public float jumpForce = 10f;

    //set force of gravity, must be stronger so player comes back down after jump 
    public float gravity = 20f;

    private float verticalVelocity;

    //lane 0 is Left. Lane 1 is Middle. Lane 2 is Right.
    //starting lane is Middle lane --> Lane 1
    private int targetLane = 1;

    private float startingXPos; //store starting X position
    private float startingYPos;     //store starting X position

    //timer for rolling movement on click. Roll will last 0.5 seconds
    private float rollDuration = 0.1f; 
    private float rollTimer = 0f;

    //sound for pickup collection
    [SerializeField]
    AudioSource speaker;

    [SerializeField]
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        
        playerAnim = GetComponent<Animator>();
        //control of character from the start
        controller = GetComponent<CharacterController>();

        //initial X and Y is set as the x and y positions of the character in the scene
        startingXPos = transform.position.x;
        startingYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        //player has not moved yet. Set moveDirection to Vector3.zero
        Vector3 moveDirection = Vector3.zero;

        //check if player stays within available lanes
        //target lane can't be smaller than 0 or bigger than 2
        //there are only 3 lanes --> Lane 0, Lane 1, and Lane 2
        //left and right movements
        if (Input.GetKeyDown(KeyCode.LeftArrow)  && targetLane > 0)
        {
            speaker.PlayOneShot(audioClips[3]); //play whoosh

            targetLane = targetLane - 1;    //move to left. Left = 0
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && targetLane < 2)
        {
            speaker.PlayOneShot(audioClips[3], 0.5f); //play whoosh

            targetLane = targetLane + 1;    //move to right. Right = 2
        }

        //calculate target position
        //player only moves about y-axis(up and down) or x-axis(left or right)
        float targetX = startingXPos + (targetLane - 1) * laneDistance;             //startingXPos ensures player is centred with middle lane
        float currentX = Mathf.MoveTowards(transform.position.x, targetX, Time.deltaTime * moveSpeed);
        moveDirection.x = (targetX - transform.position.x) * moveSpeed;

        //Jumping Movement
        //check if player is on the ground. 
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))  //key is clicked    
            {
                speaker.PlayOneShot(audioClips[3]); //play whoosh 

                verticalVelocity = jumpForce;
                playerAnim.SetBool("Jump", true);
            }

            
        }
        else //not on ground
        {
            verticalVelocity -= gravity * Time.deltaTime;   //bring player back down to the ground

            if (verticalVelocity <= 0)
            {
                playerAnim.SetBool("Jump", false);  //stop animation
                
            }
        }

        //Crouching Movement
        if (Input.GetKeyDown(KeyCode.DownArrow) && controller.isGrounded)    //when key is clicked
        {
            speaker.PlayOneShot(audioClips[3]); //play whoosh. 

            //reduce player height
            controller.height = 1f;
            playerAnim.SetBool("Roll", true);
            
            rollTimer = rollDuration;
        }
        else if (rollTimer > 0) //if player is already rolling
        {
            //start count down for roll
            rollTimer -= Time.deltaTime;
            
            //if the timer has reached 0 and player is on the ground, stop roll
            if (rollTimer <= 0 && controller.isGrounded)
            {
                //bring back to normal height
                controller.height = 2f;
                playerAnim.SetBool("Roll", false); //stop roll when back to normal height
                
            }

           
        }

        //assign movement to variable and perform movement
        moveDirection.y = verticalVelocity;
        controller.Move(moveDirection * Time.deltaTime);

        //ensure player is at correct y-position when not jumping
        if (controller.isGrounded && verticalVelocity <= 0)
        {
            transform.position = new Vector3(transform.position.x, startingYPos, transform.position.z);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GreenPotion"))
        {
            speaker.PlayOneShot(audioClips[0]);
        }
        else if (other.CompareTag("BluePotion"))
        {
            speaker.PlayOneShot(audioClips[1]);
        }
        else if (other.CompareTag("RedPotion")) 
        {
            speaker.PlayOneShot(audioClips[2]);
        }
    }

    //public void OnScoreBoosterSound()
    //{
    //    PlaySound(0);
    //}

    //public void OnSpeedBoosterSound()
    //{
    //    PlaySound(1);
    //}

    //public void OnImmunitySound()
    //{

    //    PlaySound(2);
    //}



    //pickup audio
    public void PlaySound(int i)
    {
        speaker.Stop();

        speaker.clip = audioClips[i];
        speaker.Play(0);
    }
}
//REFERENCING
//Little, M., 2024. Utilizing Mathf.Lerp in Unity. [online] Available at: <https://medium.com/@little_michael101/utilizing-mathf-lerp-in-unity-8085a1dddd3c#:~:text=Long%20story%20short%2C%20Mathf.,typically%20done%20inside%20a%20coroutine.> [Accessed 22 March 2025].
//Unity, [s.a.]. Mathf.MoveTowards. [online] Available at: <https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Mathf.MoveTowards.html> [Accessed 23 March 2025].
//Unity Document,[s.a.]. CharacterController.Move. [online] Available at: <https://docs.unity3d.com/6000.0/Documentation/ScriptReference/CharacterController.Move.html> [Accessed 22 March 2025].