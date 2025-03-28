using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveHallway : Death        ////need to check if player death is true. If true stop moving hallway. Set hallwayspeed to 0.
{
    //[SerializeField]
    private int hallwaySpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move platform bit by bit along the x-axis at set speed
        transform.position += new Vector3(0, 0, -hallwaySpeed) * Time.deltaTime;


        if(deathStatus == true) 
        {
            hallwaySpeed = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroyer")) //anything that is not the player must be destroyed
        {

            Destroy(gameObject);    //destroy hallway and everything in it when it collides with invisible destroy wall
        }

    }

    
}
