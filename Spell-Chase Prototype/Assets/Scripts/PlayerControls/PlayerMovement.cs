using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    //set distance between lanes
    public float laneDistance = 3f;

    //set speed of movement
    public float moveSpeed = 10f;

    //set jumping force. 10f to overcome a bit of the gravity effect
    public float jumpForce = 10f;

    //set force of gravity, must be stronger so player comes back down after jump 
    public float gravity = 20f;

    private float verticalVelocity;

    //lane 0 is Left. Lane 1 is Middle. Lane 2 is Right.
    //starting lane is Middle lane --> Lane 1
    private int targetLane = 1;

    // Start is called before the first frame update
    void Start()
    {
        //control of character from the start
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //player has not moved yet. Set moveDirection to Vector3.zero
        Vector3 moveDirection = Vector3.zero;

        //check if player stayes within available lanes
        //target lane can't be smaller than 0 or bigger than 2
        //there are only 3 lanes --> Lane 0, Lane 1, and Lane 2
        //left and right movements
        if (Input.GetKeyDown(KeyCode.LeftArrow) && targetLane > 0)
        {
            targetLane = targetLane - 1;    //move to left. Left = 0
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && targetLane < 2)
        {
            targetLane = targetLane + 1;    //move to right. Right = 2
        }

        //calculate target position
        //player only moves about y-axis(up and down) or z-axis(left or right)
        float targetZ = (targetLane - 1) * laneDistance;
        float currentZ = Mathf.MoveTowards(transform.position.z, targetZ, Time.deltaTime * moveSpeed);
        moveDirection.z = (targetZ - transform.position.z) * moveSpeed;

        //Jumping Movement
        //check if player is on the ground. Game does not have double jumps :)
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                verticalVelocity = jumpForce;
            }
        }
        else //not on ground
        {
            verticalVelocity -= gravity * Time.deltaTime;   //bring player back down to the ground
        }

        //Crouching Movement
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //reduce player height
            controller.height = 1f;
        }
        else
        {
            //bring back to normal height
            controller.height = 2f;
        }

        //assign movement to variable and perform movement
        moveDirection.y = verticalVelocity;
        controller.Move(moveDirection * Time.deltaTime);

    }
}
//REFERENCING
//Little, M., 2024. Utilizing Mathf.Lerp in Unity. [online] Available at: <https://medium.com/@little_michael101/utilizing-mathf-lerp-in-unity-8085a1dddd3c#:~:text=Long%20story%20short%2C%20Mathf.,typically%20done%20inside%20a%20coroutine.> [Accessed 22 March 2025].
//Unity, [s.a.]. Mathf.MoveTowards. [online] Available at: <https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Mathf.MoveTowards.html> [Accessed 23 March 2025].