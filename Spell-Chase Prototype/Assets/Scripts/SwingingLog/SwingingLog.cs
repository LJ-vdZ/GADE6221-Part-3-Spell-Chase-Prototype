using UnityEngine;

public class SwingingLog : MonoBehaviour
{
    [SerializeField]
    private float maxAngle = 30f; //max swing angle of log

    [SerializeField]
    private float swingSpeed = 1f; //speed of swing

    [SerializeField]
    private Vector3 swingAxis = Vector3.forward; //axis of rotation (z-axis)

    private Quaternion initialRotation;

    private float swingTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //store initial rotation of pivot
        initialRotation = transform.rotation;


    }

    // Update is called once per frame
    void Update()
    {
        ////calculate angle using sine wave to get pendulum-like motion for swinging
        //float angle = maxAngle * Mathf.Sin(Time.timeSinceLevelLoad * swingSpeed);

        ////apply rotation around axis
        //transform.rotation = initialRotation * Quaternion.AngleAxis(angle, swingAxis);

        swingTimer += Time.deltaTime;
        float angle = maxAngle * Mathf.Sin(swingTimer * swingSpeed);
        transform.rotation = initialRotation * Quaternion.AngleAxis(angle, swingAxis);
    }
}
