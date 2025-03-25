using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject hallwaySection;

    //check if player collides with invisible wall
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            //spawn new section of hallway
            //indicate where to spawn new hallway
            Instantiate(hallwaySection, new Vector3(7, 6, 43), Quaternion.identity);   //there is no rotation
        }
    }
}
