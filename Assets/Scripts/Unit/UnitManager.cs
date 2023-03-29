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
    GameObject unit;
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
    UnitsDatabase UnitTypes;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.FindGameObjectWithTag("HealthText");
        TextCanvas = FindObjectOfType<Canvas>();
        unit = gameObject;
        Rigidbody = GetComponent<Rigidbody2D>();
        UnitTypes = GameObject.FindGameObjectWithTag("UnitTypes").GetComponent<UnitsDatabase>();
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
        mUnit.unit = unit;
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
            foreach (Unit unittype in UnitTypes.units)
            {
                if (unittype.name == gameObject.name.Split()[0] && unittype.name == "Castle")
                {
                    GameManager.GameEnd = true;
                    GameObject gameend = Instantiate(text, Camera.main.WorldToScreenPoint(Vector3.zero), Quaternion.identity);
                    gameend.transform.parent = TextCanvas.transform;
                    gameend.GetComponent<TextMeshProUGUI>().fontSize = 60;
                    gameend.GetComponent<TextMeshProUGUI>().fontWeight = TMPro.FontWeight.Bold;
                    gameend.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
                    GameObject[] AllUnits = GameObject.FindGameObjectsWithTag("Unit");
                    foreach (GameObject Unit in AllUnits)
                        Destroy(Unit);

                    if (!IsEnemy)
                    {
                        gameend.GetComponent<TextMeshProUGUI>().text = "YOU LOSE";
                        gameend.GetComponent<TextMeshProUGUI>().color = Color.red;
                        Debug.Log("YOU LOST");
                    }
                    else
                    {
                        gameend.GetComponent<TextMeshProUGUI>().text = "YOU WIN";
                        gameend.GetComponent<TextMeshProUGUI>().color = Color.green;
                        Debug.Log("YOU WIN");
                    }

                }
            }
            gameObject.SetActive(false);
        }

        if (unit.tag == "Building")
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
                    mUnit.SetFighting(mUnit.AttackCastle());
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

    public static void SayOuch()
    {
        Debug.Log("I was hit");
    }

    IEnumerator AttackUnit()
    {
        AttackUnitRun = true;
        UnitManager Victim = (UnitManager)mUnit.GetCurrentOpponent().GetComponent("UnitManager");
        if (Unit.GetDistance(unit.transform.position, Victim.transform.position) > 0.3 || Victim.Health <= 0)
            mUnit.SetFighting(false);
        else
        {
            yield return new WaitForSeconds(mUnit.AttackSpeed);
            Debug.Log($"{unit.tag} Attacking");
            Victim.Health -= mUnit.Damage;
            UnitManager.SayOuch();
        }
        AttackUnitRun = false;
    }
}
