using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Waypoints;
public class WaypointMovement : MonoBehaviour
{

    Path currentPath;
    bool updateMove;
    Rigidbody rBody;
    float speed = 2;
    float closeEnough = .25f;
    public void StartMoving(Waypoint start, Waypoint end)
    {
        rBody = GetComponent<Rigidbody>();
        if (start == null)
            start = Waypoint.ClosestWaypointTo(transform.position);
        currentPath = Waypoint.CreatePath(start, end);
        updateMove = true;
    }

    public void StopMoving()
    {
        updateMove = false;
        rBody.velocity = Vector3.zero;
    }
    void Update()
    {
        if (!updateMove) return;
        if (currentPath.IsEmpty())
        {
            StopMoving();
            return;
        }
        Waypoint target = currentPath.Peek();
        Vector3 direction = -transform.position + target.transform.position;
        direction.y = 0;
        //direction.Normalize();
        if (direction.magnitude < closeEnough)
            HandleReachedWaypoint();
        else
        {
            Vector3 targetVel = direction.normalized * speed;
            targetVel.y = rBody.velocity.y;
            rBody.velocity = targetVel;
        }
    }
    public void HandleReachedWaypoint()
    {
        Waypoint justReached = currentPath.Peek();
        currentPath.Pop();
        if (justReached.waypointReachedEvent != null)
            justReached.waypointReachedEvent();
    }
}
