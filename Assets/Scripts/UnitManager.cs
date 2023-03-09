using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.PlayerLoop;
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
    GameObject text;
    GameObject healthtext;
    Canvas TextCanvas;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.FindGameObjectWithTag("HealthText");
        TextCanvas = FindObjectOfType<Canvas>();
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
                mUnit.Cost = unit.Cost;
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
        {
            if (healthtext != null)
                Destroy(healthtext);
            gameObject.SetActive(false);
        }

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
        }

    }

    void Update()
    {
        if (Health < HealthChange)
        {
            StartCoroutine(DisplayDamage());
            UnitManager Enemy = (UnitManager)mUnit.GetCurrentOpponent().GetComponent("UnitManager");
            if (mUnit.IsEnemy)
                Rigidbody.AddForce((Rigidbody.transform.position + new Vector3(0, 1, 0)) * Enemy.mUnit.KnockBack);
            else
                Rigidbody.AddForce((Rigidbody.transform.position - new Vector3(0, 1, 0)) * Enemy.mUnit.KnockBack);
            HealthChange = Health;
        }
    }

    

    IEnumerator DisplayDamage()
    {
        Debug.Log("Display");
        if (healthtext != null)
        {
            Destroy(healthtext);
        }
        healthtext = Instantiate(text, Vector3.zero, Quaternion.identity);
        healthtext.transform.SetParent(TextCanvas.transform, false);
        healthtext.transform.position = Camera.main.WorldToScreenPoint(Rigidbody.transform.position);
        healthtext.GetComponent<TextMeshProUGUI>().text = (Health - HealthChange).ToString();
        healthtext.GetComponent<TextMeshProUGUI>().color = Color.red;
        yield return new WaitForSeconds(1);
        Destroy(healthtext);
        Debug.Log("Destroy");
    }

    public void SayOuch()
    {
        Debug.Log("I was hit");
    }

    IEnumerator AttackUnit()
    {
        AttackUnitRun = true;
        UnitManager Victim = (UnitManager)mUnit.GetCurrentOpponent().GetComponent("UnitManager");
        if (Vector3.Distance(Unit.transform.position, Victim.transform.position) > 0.3 || Victim.Health <= 0)
            mUnit.SetFighting(false);
        else
        {
            yield return new WaitForSeconds(mUnit.AttackSpeed);
            Debug.Log($"{Unit.tag} Attacking");
            Victim.Health -= mUnit.Damage;
            Victim.SayOuch();
        }
        AttackUnitRun = false;
    }
}
