using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject hallwaySection;
    public GameObject bossHallway;

    private float bossBattleTime = 0f;

    private float bossBattleDuration = 10f;

    private bool isBossBattle = false;

    public GameObject[] floatingPlatforms;

    private bool hasTriggered = false;

    private void Start()
    {
        //set boss battle to false and boss battle time to zero at start of game
        bossBattleTime = 0f;
        isBossBattle = false;
    }

    private void Update()
    {
        //if the boss battle is not happening yet, increase time(will change this to score later)
        if (isBossBattle == false)
        {
            bossBattleTime += Time.deltaTime;

            if (bossBattleTime >= 5f)   //if player reached a certain time (score), initiate boss battle
            {
                //boss battle is true
                isBossBattle = true;

                //assign duration to boss battle time so it can be decreased (will change this so it score is checked)
                bossBattleTime = bossBattleDuration;
            }
        }
        else  //boss battle is happening
        {
            bossBattleTime -= Time.deltaTime;   //decrease time (check player score)
            if (bossBattleTime <= 0f)
            {
                isBossBattle = false;
                bossBattleTime = 0f;
            }
        }

    }

    //check if player collides with invisible wall
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            //collision of both Box Collider and Character Controls are detected. Two hallway sections are generated as a result.
            //make sure only one trigger happens, not two. We only want to generate one hallway
            if (other.gameObject.CompareTag("Trigger") && !hasTriggered && isBossBattle == false)
            {
                hasTriggered = true;    //a trigger occured

                //spawn new section of hallway
                //indicate where to spawn new hallway
                Instantiate(hallwaySection, new Vector3(7, 6, 43), Quaternion.identity);   //there is no rotation
            
            }
            else //spawn boss hallway
            {
                hasTriggered = true;    //a trigger occured

                Instantiate(bossHallway, new Vector3(-50, 2, 2), Quaternion.identity);
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



