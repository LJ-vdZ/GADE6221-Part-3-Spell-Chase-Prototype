using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //make field Serializable so we can change it in the inspector
    [SerializeField]
    //field for waypoint path
    private WaypointPath wayPointPath;   //NewBehviourScript == WayPointPath script

    //field to determine speed at which platform moves
    [SerializeField]
    private float platformSpeed;

    //a field to determine the waypoint the platform is moving towards
    private int nextWaypointIndex;

    //need to keep track of previous and target waypoint for smooth movement of platform 
    private Transform previousWaypoint;
    private Transform targetWaypoint;

    //how long it takes to get to waypoint
    private float timeToWaypoint;

    //how much time has currently past
    private float timePast;


    // Start is called before the first frame update
    void Start()
    {
        //call method to initialise everything
        TargetNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        //move between the two waypoints

        timePast += Time.deltaTime;

        float timePastPercentage;

        timePastPercentage = timePast / timeToWaypoint;

        //update position of platform using Lerp
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, timePastPercentage);  //this will change the position based on how much of the journey has past

        if (timePastPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    //method that will target each waypoint 
    private void TargetNextWaypoint()
    {
        //variable to store distance between waypoints;
        float distanceToWaypoint;

        //set previousWaypoint to the current target index 
        previousWaypoint = wayPointPath.GetWaypoint(nextWaypointIndex);

        //set target index to the next one on the path
        nextWaypointIndex = wayPointPath.GetNextWaypointIndex(nextWaypointIndex);

        //set target waypoint to waypoint at the new target index
        targetWaypoint = wayPointPath.GetWaypoint(nextWaypointIndex);

        //set timePast to zero
        timePast = 0;

        //get distance between waypoints
        distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);

        //use distance to calculate time to get to next waypoint. Speed equation v = d/t. therefore t = d/v.

        timeToWaypoint = distanceToWaypoint / platformSpeed;

    }
}
