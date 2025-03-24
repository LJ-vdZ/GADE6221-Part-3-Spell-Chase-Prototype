using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    //public method to get a specific waypoint index
    public Transform GetWaypoint(int waypointIndex)
    {
        //passing index of 1 = first waypoint, passing index of 2 = second waypoint, etc.

        return transform.GetChild(waypointIndex);

    }

    //need to get the next waypoint index on the path
    public int GetNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex;

        nextWaypointIndex = currentWaypointIndex + 1;

        //check if the platform is back at its starting point
        if (nextWaypointIndex == transform.childCount)      //childCount is the number of child class, aka waypoints, in the parent class. There are three
        {
            //set the next index back to zero
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }
}
