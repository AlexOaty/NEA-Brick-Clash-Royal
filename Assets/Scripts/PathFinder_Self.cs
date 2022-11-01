using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder_Self : MonoBehaviour
{
    Seeker seeker;
    Rigidbody2D rb;
    public Vector3 target;
    public float speed = 100f;
    public float pointDistance = 3f;
    bool EndOfPath = false;
    int currentWaypoint = 0;
    Path path;
    Vector3 NewTarget;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }
    
    void UpdatePath()
    {
        if (rb.position.y <= -0.15)
	    {
            if (rb.position.x <= 0)
                NewTarget = new Vector3(-0.3f, -0.15f, 0f);

            else
                NewTarget = new Vector3(0.4f, -0.15f, 0f);
	    }
        else
	    {
            NewTarget = target;
	    }

        path = seeker.StartPath(rb.position, NewTarget);
        currentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            EndOfPath = true;
            return;
        }
        else
        {
            EndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position);
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < pointDistance)
        {
            currentWaypoint++;
        }
    }
}
