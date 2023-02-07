using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class UnitManager : MonoBehaviour
{
    public Unit mUnit;
    GameObject Unit;
    Rigidbody2D Rigidbody;
    public bool IsEnemy;
    float HealthChange;
    bool AttackUnitRun;
    public string UnitType;
    public float Health;
    SpriteRenderer spriteRenderer;
    public TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        Unit = gameObject;
        Rigidbody = GetComponent<Rigidbody2D>();
        UnitsDatabase UnitTypes = GameObject.FindGameObjectWithTag("UnitTypes").GetComponent<UnitsDatabase>();
        foreach (Unit unit in UnitTypes.units)
        {
            if (unit.name == UnitType)
            {
                mUnit = ScriptableObject.CreateInstance<Unit>();
                mUnit.speed = unit.speed;
                mUnit.Damage = unit.Damage;
                mUnit.AttackSpeed = unit.AttackSpeed;
                mUnit.Health = unit.Health;
                mUnit.KnockBack = unit.KnockBack;
            }
        }
        mUnit.IsEnemy = IsEnemy;
        mUnit.unit = Unit;
        mUnit.rb = Rigidbody;
        Health = mUnit.Health;
        HealthChange = Health;
        mUnit.FindPath();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (IsEnemy)
        {
            spriteRenderer.color = Color.red;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //unit death
        if (Health <= 0)
            gameObject.SetActive(false);
        if (Unit.tag == "Building")
        {
            if (!mUnit.GetFighting())
                mUnit.SetFighting(mUnit.CheckEnemies());
            else if (!AttackUnitRun)
                StartCoroutine(AttackUnit());
        }
        else
        {

            //follow path
            if (!mUnit.GetEndOfPath() && !mUnit.GetFighting())
            {
                mUnit.FollowPath();
                mUnit.SetFighting(mUnit.CheckEnemies());
            }
            //Check for enemies or buildings
            else if (!mUnit.GetFighting() && mUnit.GetEndOfPath())
            {
                mUnit.SetFighting(mUnit.CheckEnemies());

                if (!mUnit.GetFighting())
                    mUnit.SetFighting(mUnit.CheckBuildings());
            }
            //Attack enemy unit
            else if (mUnit.GetFighting() && !AttackUnitRun)
                if (mUnit.AttackSpeed != -1)
                    StartCoroutine(AttackUnit());
            //Knocks the player back when they lose health
            if (Health < HealthChange)
            {
                UnitManager Enemy = (UnitManager)mUnit.GetCurrentOpponent().GetComponent("UnitManager");
                if (mUnit.IsEnemy)
                    Rigidbody.AddForce((Rigidbody.transform.position + new Vector3(0, 1, 0)) * Enemy.mUnit.KnockBack);
                else
                    Rigidbody.AddForce((Rigidbody.transform.position - new Vector3(0, 1, 0)) * Enemy.mUnit.KnockBack);
                HealthChange = Health;
                Instantiate(Text, transform.position, Quaternion.identity);
                Text.text = (HealthChange - Health).ToString();
                Text.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
            }
        }

    }

    IEnumerator AttackUnit()
    {
        AttackUnitRun = true;
        UnitManager Enemy = (UnitManager)mUnit.GetCurrentOpponent().GetComponent("UnitManager");
        if (Vector3.Distance(Unit.transform.position, Enemy.transform.position) > 0.3 || Enemy.Health <= 0)
            mUnit.SetFighting(false);
        else
        {
            yield return new WaitForSeconds(mUnit.AttackSpeed);
            Debug.Log($"{Unit.tag} Attacking");
            Enemy.Health -= mUnit.Damage;
        }
        AttackUnitRun = false;
    }
}
