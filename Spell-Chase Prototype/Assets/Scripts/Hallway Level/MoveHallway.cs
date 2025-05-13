using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveHallway : MonoBehaviour        
{
    //static to apply to all hallways including clones
    public static float hallwaySpeed = 5f;
    public static float speedCooldownTime = 40f;
    public static float originalSpeed = 5f;

    [SerializeField]
    public PickupBar pickupBar;

    public float maxSlider = 40f;

    [System.Obsolete]

    // Start is called before the first frame update
    void Start()
    {

        if(Pickup.isSpeeding == false) 
        {
            hallwaySpeed = 5f;

            speedCooldownTime = 40f;
        }

        pickupBar = FindObjectOfType<PickupBar>();

        pickupBar.setMaxSlider(maxSlider);

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

    public void EndSpeedBoost() 
    {
        if(Pickup.isSpeeding == true) 
        {
            speedCooldownTime -= Time.deltaTime;

            Debug.Log("Speed Cooldown count down started");

            if (speedCooldownTime <= 0f)
            {
                hallwaySpeed = originalSpeed;

                Pickup.isSpeeding = false;
                Debug.Log("Speed set to false");
                
                speedCooldownTime = 40f;
            }

            pickupBar.sliderValue(speedCooldownTime);
        }
    }
}
