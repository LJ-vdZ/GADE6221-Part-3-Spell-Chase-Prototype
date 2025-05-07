using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBeam : ObstaclePassedScore
{
    [SerializeField]
    float speed;
    public Vector3 rotateSpeed = new Vector3(100, 0, 0);

    public int decreasePerSecond = 10;
    public float collisionTimer = 0f;
    private bool currentlyColliding = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(rotateSpeed * Time.deltaTime);

        //get forward
        Vector3 forward = transform.forward;

        Vector3 direction = forward * speed;

        //take our direction / speed and actually move our object
        transform.position += direction * -1 * Time.deltaTime;

        if (Death.deathStatus == true)
        {
            speed = 0;
        }

        if (currentlyColliding )
        {
            collisionTimer += Time.deltaTime;

            if ( collisionTimer >= 1f )
            {
                int totalCollisionTime = Mathf.FloorToInt( collisionTimer );
                score -= decreasePerSecond * totalCollisionTime;
                collisionTimer -= totalCollisionTime;
                UpdateScoreInUI();
            }
        }

        else
        {
            collisionTimer = 0f;
        }

        currentlyColliding = false;     //Reset the boolean for next frame, and if still colliding then it will turn true again
    }

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.CompareTag("Energy beam"))
        {
            currentlyColliding = true;
        }
    }
}
