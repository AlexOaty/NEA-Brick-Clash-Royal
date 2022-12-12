using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class UnitBehaviour
{
    protected GameObject Unit;
    protected GameObject PathToFollow;
    protected Rigidbody2D rb;
    protected float speed;
    bool IsEnemy;
    //bool IsBuilding;
    GameObject[] Paths;
    GameObject[] PathBounds;
    GameObject Bottom;
    GameObject Top;
    bool OnPath;
    public bool EndOfPath;
    public bool FightingUnit;
    //public bool FightingBuilding;
    GameObject[] Opponents;
    public GameObject CurrentOpponent;
    GameObject[] Buildings;
    public float Health;
    public float Damage;
    public float AttackSpeed;
    public float kBack;

    public UnitBehaviour(GameObject Unit, Rigidbody2D rb, float speed, bool IsEnemy, float Health, float Damage, float AttackSpeed, float kBack)
    {
        this.Unit = Unit;
        this.rb = rb;
        this.speed = speed;
        this.IsEnemy = IsEnemy;
        this.Health = Health;
        this.Damage = Damage;
        this.AttackSpeed = AttackSpeed;
        this.kBack = kBack;
    }

    public void FollowPath()
    {
        EndOfPath = false;
        float distance;
        Vector3 direction;
        GameObject target;

        //Checks if unit is on path, if the unit is on the path it will move along the path. If not, it will move to their end of the path
        if (!OnPath)
        {
            if (!IsEnemy)
                target = Bottom;
            else
                target = Top;
            distance = Vector3.Distance(Unit.transform.position, target.transform.position);
            if (distance > 0.05)
            {
                direction = target.transform.position - Unit.transform.position;
                rb.AddForce(direction / distance * speed);
            }
            else
                OnPath = true;
        }
        else
        {
            if (!IsEnemy)
                target = Top;
            else
                target = Bottom;
            distance = Vector3.Distance(Unit.transform.position, target.transform.position);
            direction = target.transform.position - Unit.transform.position;
            rb.AddForce(direction / distance * speed);
            if (distance < 0.1)
            {
                EndOfPath = true;
            }
        }

    }

    public void FindPath()
    {
        // Finds the two path objects in the arena and finds the closest path to the unit, which the unit will follow
        Paths = GameObject.FindGameObjectsWithTag("Path");
        float PathDistance0 = Vector3.Distance(Unit.transform.position, Paths[0].transform.position);
        float PathDistance1 = Vector3.Distance(Unit.transform.position, Paths[1].transform.position);
        if (PathDistance0 < PathDistance1)
        {
            PathToFollow = Paths[0];
            PathBounds = GameObject.FindGameObjectsWithTag("Left");
            FindBounds();
        }
        else
        {
            PathToFollow = Paths[1];
            PathBounds = GameObject.FindGameObjectsWithTag("Right");
            FindBounds();
        }
    }
    private void FindBounds()
    {
        //Finds the top and bottom of the path
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
    public bool CheckEnemies()
    {
        //Checks the units area for any enemy units and stops the unit if they are in range
        bool InRange = false;
        if (IsEnemy)
            Opponents = GameObject.FindGameObjectsWithTag("UnitPlayer");
        else
            Opponents = GameObject.FindGameObjectsWithTag("UnitEnemy");
        try
        {
            CurrentOpponent = Opponents[0];

        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
        foreach (GameObject opponent in Opponents)
        {
            //UnitBehaviour unitBehaviour = opponent.GetComponent<UnitBehaviour>();
            float CheckDist = Vector3.Distance(Unit.transform.position, opponent.transform.position);
            if (CheckDist <= 0.3 && opponent != Unit)
                InRange = true;
        }
        if (InRange == false)
            return false;
        else
        {
            foreach (GameObject opponent in Opponents)
            {
                float CheckDist = Vector3.Distance(Unit.transform.position, opponent.transform.position);
                float CurrentDist = Vector3.Distance(Unit.transform.position, CurrentOpponent.transform.position);
                if (CheckDist < CurrentDist && opponent != Unit)
                {
                    FightingUnit = true;
                    CurrentOpponent = opponent;
                }
            }
        }
        return true;
    }

    public bool GetIsEnemy()
    {
        return IsEnemy;
    }

    public bool CheckBuildings()
    {
        float distance;
        Vector3 direction;
        Buildings = GameObject.FindGameObjectsWithTag("Building");
        try
        {
            CurrentOpponent = Buildings[0];
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
        foreach (GameObject Building in Buildings)
        {
            float CheckDist = Vector3.Distance(Unit.transform.position, Building.transform.position);
            float CurrentDist = Vector3.Distance(Unit.transform.position, CurrentOpponent.transform.position);
            if (CheckDist < CurrentDist && Building != Unit)
            {
                CurrentOpponent = Building;
            }
        }
        distance = Vector3.Distance(Unit.transform.position, CurrentOpponent.transform.position);
        if (distance > 0.3)
        {
            direction = CurrentOpponent.transform.position - Unit.transform.position;
            rb.AddForce(direction / distance * speed);
            return false;
        }
        else
        {
            FightingUnit = true;
            return true;
        }


    }



    //public void AttackUnit()
    //{
    //    Debug.Log($"{Unit.tag} Attacking");
    //}
}