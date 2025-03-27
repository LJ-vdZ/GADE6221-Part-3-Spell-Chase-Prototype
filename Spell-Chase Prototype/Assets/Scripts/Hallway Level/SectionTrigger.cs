using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject hallwaySection;

    private bool hasTriggered = false;

    //check if player collides with invisible wall
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            //collision of both Box Collider and Character Controls are detected. Two hallway sections are generated as a result.
            //make sure only one trigger happens, not two. We only want to generate one hallway
            if (other.gameObject.CompareTag("Trigger") && !hasTriggered)
            {
                hasTriggered = true;    //a trigger occured

                //spawn new section of hallway
                //indicate where to spawn new hallway
                Instantiate(hallwaySection, new Vector3(7, 6, 43), Quaternion.identity);   //there is no rotation
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



