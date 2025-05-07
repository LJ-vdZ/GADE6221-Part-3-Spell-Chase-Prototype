using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveHallway : MonoBehaviour        
{
    [SerializeField]
    public static float hallwaySpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move hallways bit by bit along x-axis at set speed
        transform.position += new Vector3(0, 0, -hallwaySpeed) * Time.deltaTime;



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroyer")) //anything that is not the player must be destroyed
        {

            Destroy(gameObject);    //destroy hallway and everything in it when it collides with invisible destroy wall
        }

    }

    public void ApplySpeed(float increaseSpeed)
    {

        hallwaySpeed = hallwaySpeed * increaseSpeed;

        Debug.Log("Hallway Speed increased to " + hallwaySpeed);
    }

    public void EndSpeedBoost(float originalSpeed) 
    {
        hallwaySpeed = originalSpeed;
    }
}
