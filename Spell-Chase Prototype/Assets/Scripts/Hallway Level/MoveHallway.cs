using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveHallway : MonoBehaviour        
{
    //[SerializeField]
    public static float hallwaySpeed = 5f;
    public static float speedCooldownTime;
    public static float originalSpeed = 5f;

    [SerializeField]
    public static PickupBar pickupBar;

    // Start is called before the first frame update
    void Start()
    {
        speedCooldownTime = 5f;

        pickupBar = FindObjectOfType<PickupBar>();
        
        pickupBar.setMaxSlider(speedCooldownTime);
    }

    // Update is called once per frame
    void Update()
    {
        //move hallways bit by bit along x-axis at set speed
        transform.position += new Vector3(0, 0, -hallwaySpeed) * Time.deltaTime;

        //stop hallway when death is true
        if(Death.deathStatus == true) 
        {
            hallwaySpeed = 0;
        }

        EndSpeedBoost();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroyer")) //anything that is not the player must be destroyed
        {

            Destroy(gameObject);    //destroy hallway and everything in it when it collides with invisible destroy wall
        }

    }

    public static void ApplySpeed(float increaseSpeed)
    {

        hallwaySpeed = hallwaySpeed * increaseSpeed;

        Debug.Log("Hallway Speed increased to " + hallwaySpeed);
    }

    public static void EndSpeedBoost() 
    {
        if(Pickup.isSpeeding == true) 
        {
            speedCooldownTime -= Time.deltaTime;

            Debug.Log("Speed Cooldown count down started");

            pickupBar.sliderValue(speedCooldownTime);

            if (speedCooldownTime == 0f)
            {
                hallwaySpeed = originalSpeed;

                Pickup.isSpeeding = false;
                Debug.Log("Speed set to false");

                speedCooldownTime = 5f;
            }
        }
    }
}
