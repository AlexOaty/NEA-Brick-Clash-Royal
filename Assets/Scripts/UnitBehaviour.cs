using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitBehaviour
{

    protected GameObject Unit;
    protected GameObject PathToFollow;
    protected Rigidbody2D rb;
    protected float speed;
    GameObject[] Paths;

    public UnitBehaviour(GameObject Unit, Rigidbody2D rb, float speed)
    {
        this.Unit = Unit;
        this.rb = rb;
        this.speed = speed;
    }

    public void FollowPath()
    {
        float distance = Vector3.Distance(Unit.transform.position, PathToFollow.transform.position);
        Vector3 direction = PathToFollow.transform.position - Unit.transform.position;
        rb.AddForce(direction * distance * speed);
    }

    public GameObject FindPath()
    {
        Paths = GameObject.FindGameObjectsWithTag("Path");
        float PathDistance0 = Vector3.Distance(Unit.transform.position, Paths[0].transform.position);
        float PathDistance1 = Vector3.Distance(Unit.transform.position, Paths[1].transform.position);
        if (PathDistance0 < PathDistance1)
        {
            PathToFollow = Paths[0];
            return Paths[0];
        }
        else
        {
            PathToFollow = Paths[1];
            return Paths[1];
        }
    }
}
