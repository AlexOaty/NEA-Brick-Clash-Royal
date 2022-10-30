using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder_Self : MonoBehaviour
{
    Seeker seeker;
    Rigidbody2D rb;
    public Transform target;
    public float speed = 100f;
    public float pointDistance = 3f;
    bool EndOfPath = false;
    int currentWaypoint = 0;
    Path path;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        path = seeker.StartPath(rb.position, target.position);
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
