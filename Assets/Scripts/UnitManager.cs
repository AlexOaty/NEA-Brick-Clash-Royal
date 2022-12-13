using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitManager : MonoBehaviour
{
    Unit mUnit;
    GameObject Unit;
    Rigidbody2D Rigidbody;
    public float speed;
    public bool IsEnemy;
    public float Health;
    float HealthChange;
    public float Damage;
    public float AttackSpeed;
    bool AttackUnitRun;
    public float KnockBack;

    // Start is called before the first frame update
    void Start()
    {
        Unit = gameObject;
        Rigidbody = GetComponent<Rigidbody2D>();
        if (Unit.tag == "Building")
        {
            mUnit = new Unit(Unit, Rigidbody, 0, IsEnemy, Health, Damage, AttackSpeed, KnockBack);
        }
        else
        {
            mUnit = new Unit(Unit, Rigidbody, speed, IsEnemy, Health, Damage, AttackSpeed, KnockBack);
            mUnit.FindPath();
        }
        HealthChange = Health;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //unit death
        if (Health <= 0)
            gameObject.SetActive(false);
        if (Unit.tag == "Building")
        {
            if (!mUnit.FightingUnit)
                mUnit.FightingUnit = mUnit.CheckEnemies();
            else if (!AttackUnitRun)
                StartCoroutine(AttackUnit());
        }
        else
        {

            //follow path
            if (!mUnit.EndOfPath && !mUnit.FightingUnit)
            {
                mUnit.FollowPath();
                mUnit.FightingUnit = mUnit.CheckEnemies();
            }
            //Check for enemies or buildings
            else if (!mUnit.FightingUnit && mUnit.EndOfPath)
            {
                mUnit.FightingUnit = mUnit.CheckEnemies();

                if (!mUnit.FightingUnit)
                    mUnit.FightingUnit = mUnit.CheckBuildings();
            }
            //Attack enemy unit
            else if (mUnit.FightingUnit && !AttackUnitRun)
                if (AttackSpeed != -1)
                    StartCoroutine(AttackUnit());
            //Knocks the player back when they lose health
            if (Health < HealthChange)
            {
                UnitManager Enemy = (UnitManager)mUnit.CurrentOpponent.GetComponent("UnitManager");
                if (IsEnemy)
                    Rigidbody.AddForce((Rigidbody.transform.position + new Vector3(0, 1, 0)) * Enemy.KnockBack);
                else
                    Rigidbody.AddForce((Rigidbody.transform.position - new Vector3(0, 1, 0)) * Enemy.KnockBack);
                HealthChange = Health;
            }
        }

    }

    IEnumerator AttackUnit()
    {
        AttackUnitRun = true;
        UnitManager Enemy = (UnitManager)mUnit.CurrentOpponent.GetComponent("UnitManager");
        if (Vector3.Distance(Unit.transform.position, Enemy.transform.position) > 0.3 || Enemy.Health <= 0)
            mUnit.FightingUnit = false;
        else
        {
            yield return new WaitForSeconds(AttackSpeed);
            Debug.Log($"{Unit.tag} Attacking");
            Enemy.Health -= Damage;
        }
        AttackUnitRun = false;
    }
}
