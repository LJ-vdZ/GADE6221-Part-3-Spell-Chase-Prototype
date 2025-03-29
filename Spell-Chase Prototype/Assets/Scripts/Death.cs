using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Death : MoveHallway    //Inherit from MoveHallway. Need to stop hallway movement if player death is true. Set hallwayspeed to 0.
{   
    [SerializeField] GameObject player;
    Animator playerAnim;

    //public GameObject EndScreenUI;
    public GameManager GameManager;
    protected static bool deathStatus = false;

    private float startingYPos;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        hallwaySpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //when character's Character Controller hits obstacle. Not when Box Collider hits obstacles.
    private void OnControllerColliderHit(ControllerColliderHit collision)   
    {
        //Debug.Log("Collision detected");

        //if (/*gameObject.GetComponent<PlayerMovement>() != null*/ collision.gameObject.tag == "Player")
        //{
        //    player.GetComponent<PlayerMovement>().enabled = false;

        //    deathStatus = true;
        //    GameManager.endGame();
        //}

        //destroy player on collision
        if (collision.gameObject.CompareTag("Obstacle"))    //when "Obstacle" hit
        {
            Debug.Log("Collision set to true");
            deathStatus = true;

            playerAnim.SetBool("Die", true);
            hallwaySpeed = 0;   //hallay stops moving. Giving illusion that player stopped moving forward on collision

            player.GetComponent<PlayerMovement>().enabled = false;

           

            GameManager.endGame();

            //Destroy(player); //----> this works
        }
    
        //player.GetComponent<PlayerMovement>().enabled = false;


    }
    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Check if colliding object has the "Player" tag
        {
            Debug.Log("Object hit the player!");
            player.GetComponent<PlayerMovement>().enabled = false;
            EndScreenUI.SetActive(true);
        }
    }*/
}
