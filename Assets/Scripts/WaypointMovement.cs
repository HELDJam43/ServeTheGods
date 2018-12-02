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
    public delegate void WaypointEvent();
    WaypointEvent onReachFirstWaypoint, onReachLastWaypoint;
    int size;
    public void StartMoving(Waypoint start, Waypoint end,WaypointEvent firstEvent,WaypointEvent lastEvent)
    {
        rBody = GetComponent<Rigidbody>();
        if (start == null)
            start = Waypoint.ClosestWaypointTo(transform.position);
        currentPath = Waypoint.CreatePath(start, end);
        size = currentPath.Size;
        updateMove = true;
        onReachFirstWaypoint = firstEvent;
        onReachLastWaypoint = lastEvent;
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
        if (currentPath.Size == size - 1 && onReachFirstWaypoint != null)
            onReachFirstWaypoint();
        if (currentPath.IsEmpty() && onReachLastWaypoint != null)
            onReachLastWaypoint();

    }
}
