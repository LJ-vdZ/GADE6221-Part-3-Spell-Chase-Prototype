using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject hallwaySection;
    public GameObject bossHallway;
    public GameObject forestSection;

    public int levelNum;

    private GameManager gameManager;


    //check player score
    private int playerScore;

    //private float bossBattleTime = 0f; //when player reaches a certain score before boss battle

    //private float bossBattleDuration; //when player reaches a certain score during boss battle

    public static bool isBossBattle = false;

    private bool hasFoughtBoss = false;

    public GameObject[] floatingPlatforms;

    private bool hasTriggered;

    ////check boss battles for looping
    //private bool foughtBossHallway = false;
    //private bool foughtBossForest = false;

    ////use score milestones to trigger boss hallway during loop
    //private int lastBossScoreTrigger = 0;



    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //set boss battle to false and boss battle time to zero at start of game
        //bossBattleTime = 0f;
        isBossBattle = false;

        levelNum = 0;
    }

    /*private void Update()
    {
        //get player's score from ObstaclePassScore
        playerScore = ObstaclePassedScore.score;
        
        //if the boss battle is not happening yet, check player score
        if (isBossBattle == false && hasFoughtBoss == false && playerScore >= 30)//if player score reaches or bigger than 30, initiate boss battle
        {
            //boss battle is true
            isBossBattle = true;
            
            Debug.Log("isBossBattle is set to true");
        }

        //boss battle is happening
        if(isBossBattle == true && playerScore >= 100)
        {
            //if player score reaches or bigger than 100, stop generating boss battle hallway
            isBossBattle = false;

            hasFoughtBoss = true; //prevent retriggering

            levelNum = levelNum + 1;

            //start spawning obstacles again
            //spawner2.spawnObstacle = true;

            foreach (var spawner in spawners)
            {
                spawner.RestartSpawning(); //restart each spawner
            }

        }

    }*/

    //check if player collides with invisible wall
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger") && !hasTriggered)
        {
            hasTriggered = true;

            if (GameManager.isBossBattle)
            {
                // Spawn boss hallway during boss battle only
                Instantiate(bossHallway, new Vector3(7, 6, 43), Quaternion.identity);
            }
            else if (gameManager != null && gameManager.completedLevels >= 1 || ObstaclePassedScore.score >= 100)
            {
                // After boss defeated, spawn forest hallway for level 2+
                Instantiate(forestSection, new Vector3(-0.24f, 1.3f, 53.902f), Quaternion.identity);
            }
            else
            {
                // Normal hallway spawn before boss battle
                Instantiate(hallwaySection, new Vector3(7, 6, 43), Quaternion.identity);
            }


            //hasTriggered = true;

            ////get current score and previous score to check for score milestone
            //int currentScore = ObstaclePassedScore.score;
            //int scoreSinceLastBoss = currentScore - lastBossScoreTrigger;

            ////check if both boss battles happened
            //if (foughtBossHallway && foughtBossForest)
            //{
            //    //randomly pick new environment
            //    int randomChoice = Random.Range(0, 2);
            //    if (randomChoice == 0)  //if zero, instantiate hallwaySection prefab
            //    {
            //        Instantiate(hallwaySection, new Vector3(7, 6, 43), Quaternion.identity);
            //    }
            //    else //else instantiate forestSection prefab
            //    {
            //        Instantiate(forestSection, new Vector3(-0.24f, 1.3f, 53.902f), Quaternion.identity);
            //    }

            //}
            //else if (GameManager.isBossBattle || ObstaclePassedScore.score >= 30)
            //{
            //    //spawn boss hallway during boss battle only
            //    Instantiate(bossHallway, new Vector3(7, 6, 43), Quaternion.identity);
                
            //    foughtBossHallway = true;
            //}
            //else if (gameManager != null && gameManager.completedLevels >= 1)
            //{
            //    //after boss defeated, spawn forest hallway for level 2+
            //    Instantiate(forestSection, new Vector3(-0.24f, 1.3f, 53.902f), Quaternion.identity);

            //    foughtBossForest = true;
            //}
            //else
            //{
            //    //normal hallway spawn before boss battle
            //    Instantiate(hallwaySection, new Vector3(7, 6, 43), Quaternion.identity);
            //}
        }
        /*if (other.gameObject.CompareTag("Trigger"))
        {
            
            //collision of both Box Collider and Character Controls are detected. Two hallway sections are generated as a result.
            //make sure only one trigger happens, not two. We only want to generate one hallway
            if (!hasTriggered && levelNum == 0 && isBossBattle == false)
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

            if (!hasTriggered && isBossBattle == false && levelNum == 1)
            {
                hasTriggered = true;    //a trigger occured

                //spawn forest environment 
                //indicate where to spawn new section of map
                Instantiate(forestSection, new Vector3(-0.24f, 1.3f, 53.902f), Quaternion.identity);   //there is no rotation

            }
        }*/
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



