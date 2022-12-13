using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitMovement : MonoBehaviour
{
    UnitBehaviour mUnit;
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
    Sprite Sprite;

    // Start is called before the first frame update
    void Start()
    {
        Unit = gameObject;
        Rigidbody = GetComponent<Rigidbody2D>();
        mUnit = new UnitBehaviour(Unit, Rigidbody, speed, IsEnemy, Health, Damage, AttackSpeed, KnockBack);
        mUnit.FindPath();
        TextMeshPro TMP = new TextMeshPro();
        TMP.text = "Hello";
        Sprite = GetComponent<Sprite>();
        HealthChange = Health;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActiveAndEnabled)
        { 
            if (Health <= 0)
                gameObject.SetActive(false);

            if (!mUnit.EndOfPath && !mUnit.FightingUnit)
            {
                mUnit.FollowPath();
                mUnit.FightingUnit = mUnit.CheckEnemies();
            }
            else if (!mUnit.FightingUnit && mUnit.EndOfPath)
            {
                mUnit.FightingUnit = mUnit.CheckEnemies();

                if (!mUnit.FightingUnit)
                    mUnit.FightingUnit = mUnit.CheckBuildings();
            }
            else if (mUnit.FightingUnit && !AttackUnitRun)
                if (AttackSpeed != -1)
                    StartCoroutine(AttackUnit());
            //Knockback
            if(Health < HealthChange)
            {
                UnitMovement Enemy = (UnitMovement)mUnit.CurrentOpponent.GetComponent("UnitMovement");
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
        Debug.Log($"{Unit.tag} Attacking");
        UnitMovement Enemy = (UnitMovement)mUnit.CurrentOpponent.GetComponent("UnitMovement");
        Enemy.Health -= Damage;
        if (Vector3.Distance(Unit.transform.position, Enemy.transform.position) > 0.3 || Enemy.Health <= 0)
            mUnit.FightingUnit = false;
        else
            yield return new WaitForSeconds(AttackSpeed);
        AttackUnitRun = false;
    }
}
