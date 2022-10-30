using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class PathFinder : MonoBehaviour
{
    public Transform end;
    public float speed;
    public float waypointDistance;
    int CurrentWaypoint = 0;
    Seeker seeker;
    Path path;
    Rigidbody2D rigidBody;
    bool reachedEndOfPath = false;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        seeker.StartPath(rigidBody.position, end.position, PathComplete);
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;

        if(CurrentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[CurrentWaypoint] - rigidBody.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rigidBody.AddForce(force);

        float distance = Vector2.Distance(rigidBody.position, path.vectorPath[CurrentWaypoint]);

        if (distance < waypointDistance)
        {
            CurrentWaypoint++;
        }
    }
    void PathComplete(Path newPath)
    {
        if (!newPath.error)
        {
            path = newPath;
            CurrentWaypoint = 0;
        }
    }
}
