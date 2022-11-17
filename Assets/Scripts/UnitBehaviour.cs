using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitBehaviour
{

    protected GameObject Unit;
    protected GameObject PathToFollow;
    protected Rigidbody2D rb;
    protected float speed;
    protected bool IsEnemy;
    GameObject[] Paths;
    GameObject[] PathBounds;
    GameObject Bottom;
    GameObject Top;
    bool OnPath;
    public bool EndOfPath;
    public bool Fighting;
    GameObject[] Opponents;
    GameObject CurrentOpponent;

    public UnitBehaviour(GameObject Unit, Rigidbody2D rb, float speed, bool IsEnemy)
    {
        this.Unit = Unit;
        this.rb = rb;
        this.speed = speed;
        this.IsEnemy = IsEnemy;
    }

    public void FollowPath()
    {
        EndOfPath = false;
        float distance;
        Vector3 direction;
        GameObject target;
        if (!OnPath)
        {
            if (!IsEnemy)
                target = Bottom;
            else
                target = Top;
            distance = Vector3.Distance(Unit.transform.position, target.transform.position);
            if (distance > 0.1)
            {
                direction = target.transform.position - Unit.transform.position;
                rb.AddForce(direction * speed);
            }
            else
            {
                OnPath = true;
            }
        }
        else
        {
            if (!IsEnemy)
                target = Top;
            else
                target = Bottom;
            distance = Vector3.Distance(Unit.transform.position, target.transform.position);
            direction = target.transform.position - Unit.transform.position;
            rb.AddForce(direction * speed);
            if (distance < 0.1)
            {
                EndOfPath = true;
            }
        }

    }

    public GameObject FindPath()
    {
        Paths = GameObject.FindGameObjectsWithTag("Path");
        float PathDistance0 = Vector3.Distance(Unit.transform.position, Paths[0].transform.position);
        float PathDistance1 = Vector3.Distance(Unit.transform.position, Paths[1].transform.position);
        if (PathDistance0 < PathDistance1)
        {
            PathToFollow = Paths[0];
            PathBounds = GameObject.FindGameObjectsWithTag("Left");
            FindBounds();
            return Paths[0];
        }
        else
        {
            PathToFollow = Paths[1];
            PathBounds = GameObject.FindGameObjectsWithTag("Right");
            FindBounds();
            return Paths[1];
        }
    }
    private void FindBounds()
    {
        if (PathBounds[0].transform.position.y < 0)
        {
            Top = PathBounds[1];
            Bottom = PathBounds[0];
        }
        else
        {
            Top = PathBounds[0];
            Bottom = PathBounds[1];
        }
    }
    public bool CheckArea()
    {
        bool InRange = false;
        Opponents = GameObject.FindGameObjectsWithTag("Unit");
        CurrentOpponent = Opponents[0];
        foreach (GameObject opponent in Opponents)
        {
            float CheckDist = Vector3.Distance(Unit.transform.position, opponent.transform.position);
            if (CheckDist <= 0.3 && opponent != Unit)
            {
                InRange = true;
            }
        }
        if (InRange == false)
        {
            return false;
        }
        else
        {
            foreach (GameObject opponent in Opponents)
            {
                float CheckDist = Vector3.Distance(Unit.transform.position, opponent.transform.position);
                float CurrentDist = Vector3.Distance(Unit.transform.position, CurrentOpponent.transform.position);
                if (CheckDist < CurrentDist && opponent != Unit)
                    CurrentOpponent = opponent;
            }
        }
        return true;
    }
    public void AttackUnit()
    {

    }
}
