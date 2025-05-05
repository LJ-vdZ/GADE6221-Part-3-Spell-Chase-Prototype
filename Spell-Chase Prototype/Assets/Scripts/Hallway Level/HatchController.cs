using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchController : MonoBehaviour
{
    [SerializeField]
    private float openAngle = 90f; //angle to open downwards
    
    [SerializeField]
    private float openSpeed = 4f;   //speed of opening/closing

    [SerializeField]
    private Vector3 rotationAxis = Vector3.left; //rotate about x-axis. same as (x, y, z). 

    [SerializeField]
    private float minDelay = 1f;   //min delay before open/close

    [SerializeField]
    private float maxDelay = 4f;   //max delay before open/close

    //rotation variables
    private Quaternion closedRotation;
    private Quaternion openRotation;

    //bool it check if hatch is open or closed
    private bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // starting rotation of closed hatch
        closedRotation = transform.rotation;

        //rotation of open hatch
        openRotation = closedRotation * Quaternion.Euler(rotationAxis * openAngle); //rotate 90 degrees about rotationAxis (x) from starting rotation in unity

        //start hatch open and close cycle
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
            //delay cycle for random amount of time before opening/closing hatch
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));


            if (!isOpen)    //if the hatch is not open, then open the hatch
            {
                yield return StartCoroutine(RotateHatch(closedRotation, openRotation));

                //hatch is open
                isOpen = true;
            }
            else    //hatch is open, close hatch
            {
                yield return StartCoroutine(RotateHatch(openRotation, closedRotation));

                //hatch is not open
                isOpen = false;
            }
        }
    }

    private IEnumerator RotateHatch(Quaternion startingPos, Quaternion endPos)
    {
        float timePassed = 0f;

        float duration = openAngle / (openSpeed * 90f); //how long it will take for hatch to close based on hatch angle and closing/opening speed

        while (timePassed < duration)
        {
            transform.rotation = Quaternion.Slerp(startingPos, endPos, timePassed / duration);

            //increase time passed
            timePassed += Time.deltaTime;

            yield return null;
        }

        //final end rotation position
        transform.rotation = endPos;
    }
}
//Unity Documentation, [s.a.]. Vector3.left. [online] Available at: <https://docs.unity3d.com/530/Documentation/ScriptReference/Vector3-left.html>[Accessed 05 May 2025].
//Unity Documentation, [s.a.]. Quaternion.Euler. [online] Available at: <https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Quaternion.Euler.html>[Accessed 05 May 2025].
//https://youtu.be/g_HaeU8SJd0
//https://stackoverflow.com/questions/64258574/what-is-an-ienumerator-in-c-sharp-and-what-is-it-used-for-in-unity
//https://docs.unity3d.com/6000.1/Documentation/ScriptReference/MonoBehaviour.StartCoroutine.html
