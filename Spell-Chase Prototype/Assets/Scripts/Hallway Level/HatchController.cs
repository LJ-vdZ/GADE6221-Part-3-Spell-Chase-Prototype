using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchController : MonoBehaviour
{
    //get pivot point of hinge
    [SerializeField] 
    private Vector3 hingeOffset = new Vector3(-1f, 0f, 0f);
    
    //private Transform hatchMesh; //hatch to rotate

    [SerializeField] 
    private float openAngle = 90f; //angle to rotate when opening downward

    [SerializeField] 
    private float openDuration = 2f; //time taken to open

    [SerializeField] 
    private float closeDuration = 2f; //time taken to close

    [SerializeField] 
    private Vector2 randomTimeRange = new Vector2(3f, 10f); //random wait time between opening and closing

    private Quaternion closedRotation;

    //
    private Quaternion openRotation;

    //check if hatch is open or not
    //private bool isOpen = false;

    private Vector3 hingePoint;


    // Start is called before the first frame update
    void Start()
    {
        //store initial rotation of closed hatch
        closedRotation = transform.rotation;

        //rotate around local X-axis to open
        openRotation = Quaternion.Euler(openAngle, 0f, 0f) * closedRotation;

        //get hinge point in world to prevent it changing position
        hingePoint = transform.position + transform.TransformDirection(hingeOffset);


        // Start the random open/close cycle
        StartCoroutine(HatchCycle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator HatchCycle()
    {
        while (true)
        {
            //wait for random time before starting opena and close cycle for hatch
            float waitTime = Random.Range(randomTimeRange.x, randomTimeRange.y);
            yield return new WaitForSeconds(waitTime);

            //open hatch
            yield return StartCoroutine(RotateHatch(closedRotation, openRotation, openDuration));
            //isOpen = true;

            //delay to close hatch again
            yield return new WaitForSeconds(2f);

            //close hatch
            yield return StartCoroutine(RotateHatch(openRotation, closedRotation, closeDuration));
            //isOpen = false;
        }
    }

    private IEnumerator RotateHatch(Quaternion from, Quaternion to, float duration)
    {
        float timeDelayed = 0f;

        //keep hatch in position while rotating
        Vector3 initialPosition = transform.position;


        while (timeDelayed < duration)
        {
            //get current angle
            float currentAngle = timeDelayed / duration;

            Quaternion currentRotation = Quaternion.Slerp(from, to, currentAngle);
            
            //get angle difference for RotateAround
            float angle = Quaternion.Angle(from, currentRotation);
            
            if (angle > 0.001f) //avoid small numerical errors
            {
                //rotate around hinge point
                transform.RotateAround(hingePoint, transform.forward, angle);

                //prevent change in position
                transform.position = initialPosition + transform.TransformDirection(hingeOffset);
            }

            timeDelayed += Time.deltaTime;
            yield return null;
        }
        //ensure final rotation and position
        transform.rotation = to;
        transform.position = initialPosition + transform.TransformDirection(hingeOffset);
    }
}
//Unity Documentation, [s.a.]. Transform.RorateAround. [online] Available at: <https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Transform.RotateAround.html>[Accessed 03 May 2025].
//Unity Documentation, [s.a.]. Vector3.Slerp. [online] Available at: <https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Vector3.Slerp.html>[Accessed 03 May 2025].
//Unity Discussions, [s.a.]. IEnumerator. [online] Available at: <https://discussions.unity.com/t/ienumerator/892998>[Accessed 03 May 2025].