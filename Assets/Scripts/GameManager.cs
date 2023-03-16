using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject Canvas;
    public float BrickNum;
    bool BrickAdding;
    public Button Spawner;
    UnitsDatabase UnitTypes;
    public GameObject[] hand;
    public GameObject Tower;
    public GameObject EnemyUnit;
    public UnitManager PlayerCastle;
    public UnitManager EnemyCastle;
    public TextMeshProUGUI Bricks;
    public TextMeshProUGUI PlayerCastleHealth;
    public TextMeshProUGUI EnemyCastleHealth;
    public TextMeshProUGUI Timer;
    float CurrentTime;

    void Start()
    {
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
        StartCoroutine("AttackPhase");
    }

    void AttackPhase()
    {
        float x, y;
        x = -1.2f;
        y = -1;
        foreach (GameObject Unit in hand)
        {
            foreach (Unit unittype in UnitTypes.units)
            {
                if (unittype.name == Unit.name)
                {
                    Button Newbutton = AddButton(x, y, Unit);
                    Newbutton.GetComponentInChildren<TextMeshProUGUI>().text = $"Spawn {Unit.name} - {unittype.Cost} Bricks";
                }
            }
            x += 1.3f;
        }
    }

    void EnemySpawner()
    {
        Instantiate(EnemyUnit, new Vector3(-0.341f, 0.7f), Quaternion.identity);
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
        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
        }
        Timer.text = CurrentTime.ToString();
        Bricks.text = "Bricks: " + BrickNum.ToString();
        PlayerCastleHealth.text = "Castle Health: " + PlayerCastle.Health.ToString();
        if(BrickNum<5 && !BrickAdding) 
        {
            StartCoroutine("AddBrick");
        }
    }

    IEnumerator AddBrick()
    {
        BrickAdding = true;
        yield return new WaitForSeconds(5);
        BrickNum++;
        BrickAdding = false;
    }
}
