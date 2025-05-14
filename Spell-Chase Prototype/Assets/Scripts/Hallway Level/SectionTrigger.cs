using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject hallwaySection;
    public GameObject bossHallway;

    //check player score
    private int playerScore;

    //private float bossBattleTime = 0f; //when player reaches a certain score before boss battle

    //private float bossBattleDuration; //when player reaches a certain score during boss battle

    public static bool isBossBattle = false;

    public GameObject[] floatingPlatforms;

    private bool hasTriggered = false;

    private void Start()
    {
        //set boss battle to false and boss battle time to zero at start of game
        //bossBattleTime = 0f;
        isBossBattle = false;
    }

    private void Update()
    {
        //get player's score from ObstaclePassScore
        playerScore = ObstaclePassedScore.score;
        
        //if the boss battle is not happening yet, check player score
        if (isBossBattle == false)
        {
            if (playerScore >= 30)   //if player score reaches or bigger than 30, initiate boss battle
            {
                //boss battle is true
                isBossBattle = true;


                Debug.Log("isBossBattle is set to true");

            }
        }

        //boss battle is happening
        if(isBossBattle == true && playerScore >= 40)
        {
            //if player score reaches or bigger than 100, stop generating boss battle hallway
            isBossBattle = false;

            //start spawning obstacles again
            spawner2.spawnObstacle = true;

        }

    }

    //check if player collides with invisible wall
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            
            
            //collision of both Box Collider and Character Controls are detected. Two hallway sections are generated as a result.
            //make sure only one trigger happens, not two. We only want to generate one hallway
            if (!hasTriggered && isBossBattle == false)
            {
                hasTriggered = true;    //a trigger occured
                
                //spawn new section of hallway
                //indicate where to spawn new hallway
                Instantiate(hallwaySection, new Vector3(7, 6, 43), Quaternion.identity);   //there is no rotation
            
            }
            
            if (!hasTriggered && isBossBattle == true) //spawn boss hallway
            {
                Instantiate(bossHallway, new Vector3(7, 6, 43), Quaternion.identity);
                
                Debug.Log("Boss hallway generated");

                hasTriggered = true;    //a trigger occured
            }
        }
    }

    //when player exits trigger, must rest hasTrigger to false. Otherwise, the map will only regenerate once. We want it to regenerate every time a trigger is hit.
    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Trigger")) 
        {
            hasTriggered = false;

        }

    }
}



