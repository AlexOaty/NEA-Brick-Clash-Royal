using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject Canvas;
    UnitsDatabase UnitTypes;
    UnitManager PlayerCastle;
    UnitManager EnemyCastle;
    Hand hand;
    bool BrickAdding;
    float CurrentTime;
    public float BrickNum;
    public Button Spawner;
    public GameObject[] Deck;
    public GameObject Tower;
    public GameObject EnemyUnit;
    public TextMeshProUGUI Bricks;
    public TextMeshProUGUI PlayerCastleHealth;
    public TextMeshProUGUI EnemyCastleHealth;
    public TextMeshProUGUI Timer;
    public static bool GameEnd;

    void Start()
    {
        GameEnd = false;
        GameObject[] Castles = GameObject.FindGameObjectsWithTag("Castle");
        foreach (GameObject Castle in Castles)
        {
            if (!Castle.GetComponent<UnitManager>().IsEnemy)
                PlayerCastle = Castle.GetComponent<UnitManager>();
            else
                EnemyCastle = Castle.GetComponent<UnitManager>();
        }
        hand = new Hand();
        UnitTypes = GameObject.FindGameObjectWithTag("UnitTypes").GetComponent<UnitsDatabase>();
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        BrickNum = 2;
        BrickAdding = true;
        CurrentTime = 10;
        StartCoroutine(BuildPhase());
        InvokeRepeating("EnemySpawner", 10, 8);
    }

    IEnumerator BuildPhase()
    {
        float x, y;
        x = -1.2f;
        y = -1;
        Button Newbutton = AddButton(x, y, Tower);
        Newbutton.GetComponentInChildren<TextMeshProUGUI>().text = $"Spawn Tower - 1 Brick";
        yield return new WaitForSeconds(10);
        BrickNum = 0;
        BrickAdding = false;
        StartCoroutine("StartCards");
    }
    void StartCards()
    {
        for (int i = 0; i<Deck.Length; i++)
        {
            hand.Add(Deck[i]);
        }
        StartCoroutine("AttackPhase");
    }

void AttackPhase()
    {
        GameObject NewUnit = null;
        int Cost = 0;
        float x, y;
        x = -1.2f;
        y = -1;

        for (int i = 0; i < 3; i++)
        {
            NewUnit = hand.GetData()[i];
            foreach (Unit unittype in UnitTypes.units)
            {
                if (unittype.name == NewUnit.name)
                {
                    Cost = unittype.Cost;
                }
            }
            Button Newbutton = AddButton(x, y, NewUnit);
            Newbutton.GetComponentInChildren<TextMeshProUGUI>().text = $"Spawn {NewUnit.name} - {Cost} Bricks";
            x += 1.3f;
        }
    }

    void EnemySpawner()
    {
        if (!GameEnd)
        {
            System.Random Rand = new System.Random();
            int Side = Rand.Next(2);
            if (Side == 1)
                Instantiate(EnemyUnit, new Vector3(-0.341f, 0.7f), Quaternion.identity);
            else
                Instantiate(EnemyUnit, new Vector3(0.41f, 0.6f), Quaternion.identity);
        }
    }

    Button AddButton(float x, float y, GameObject Unit)
    {
        Button Newbutton = Instantiate(Spawner, Camera.main.WorldToScreenPoint(new Vector3(x, y)), Quaternion.identity, Canvas.transform);
        Newbutton.GetComponent<UnitSpawner>().Unit = Unit;
        return Newbutton;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameEnd)
        {
            if (CurrentTime > 0)
            {
                CurrentTime -= Time.deltaTime;
            }
            Timer.text = CurrentTime.ToString();
            Bricks.text = "Bricks: " + BrickNum.ToString();
            PlayerCastleHealth.text = "Castle Health: " + PlayerCastle.Health.ToString();
            EnemyCastleHealth.text = "Castle Health: " + EnemyCastle.Health.ToString();
            if (BrickNum < 5 && !BrickAdding)
            {
                StartCoroutine("AddBrick");
            }
        }
    }

    IEnumerator AddBrick()
    {
        BrickAdding = true;
        yield return new WaitForSeconds(5);
        BrickNum++;
        BrickAdding = false;
    }

    public void MoveToBack(GameObject Button)
    {
        hand.Play(Button);
        StartCoroutine("AttackPhase");
    }
}
