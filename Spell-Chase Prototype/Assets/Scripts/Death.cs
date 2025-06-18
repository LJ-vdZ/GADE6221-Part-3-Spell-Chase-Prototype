using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.VFX;
using System;

public class Death : MonoBehaviour    
{   
    [SerializeField] GameObject player;
    Animator playerAnim;
    public static int newScore;
    [SerializeField] GameObject spawner1;
    [SerializeField] GameObject spawner2;
    [SerializeField] GameObject spawner3;

    //public GameObject EndScreenUI;
    public GameManager GameManager;
    public static bool deathStatus = false;

    public VisualEffect poof;

    private float startingYPos;

    private PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();

        playerStatus = player.GetComponent<PlayerStatus>();

        //Need to stop hallway movement if player death is true. Set hallwayspeed to 0.
        //MoveHallway.hallwaySpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //when character's Character Controller hits obstacle. Not when Box Collider hits obstacles.
    private void OnControllerColliderHit(ControllerColliderHit collision)   
    {

        //Debug.Log("Collision detected");

        //destroy player on collision
        if (GameManager.Instance != null && GameManager.Instance.IsImmune() && collision.gameObject.CompareTag("Obstacle"))
        {
            if (poof != null)
            {
                VisualEffect effect = Instantiate(poof, collision.transform.position, Quaternion.identity);
                effect.Play();
            }

            Destroy(collision.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Obstacle"))    //when "Obstacle" hit 
        {
            spawner1.GetComponent<spawner2>().enabled = false;
            spawner2.GetComponent<spawner2>().enabled = false;
            spawner3.GetComponent<spawner2>().enabled = false;
            Debug.Log("Collision set to true");
            deathStatus = true;

            playerAnim.SetBool("Die", true);
            //MoveHallway.hallwaySpeed = 0;   //hallway stops moving. Giving illusion that player stopped moving forward on collision



            player.GetComponent<PlayerMovement>().enabled = false;



            //GameManager.endGame();
            ObstaclePassedScore.RaisePlayerCollision();
        }
        else if (collision.gameObject.CompareTag("TrenchDestroyer"))   //if player fell into the trench
        {
            Debug.Log("Collision set to true");
            deathStatus = true;

            playerAnim.SetBool("Die", true);
            //MoveHallway.hallwaySpeed = 0;   //hallway stops moving. Giving illusion that player stopped moving forward on collision

            player.GetComponent<PlayerMovement>().enabled = false;



            //GameManager.endGame();
            ObstaclePassedScore.RaisePlayerCollision();
        }


    }
}
