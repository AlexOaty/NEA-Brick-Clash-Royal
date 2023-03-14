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
    public GameObject[] hand;
    public GameObject Tower;
    public UnitManager PlayerCastle;
    public UnitManager EnemyCastle;
    public TextMeshProUGUI Bricks;
    public TextMeshProUGUI PlayerCastleHealth;
    public TextMeshProUGUI EnemyCastleHealth;

    void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        BrickNum = 2;
        BrickAdding = true;
        StartCoroutine(BuildPhase());
    }

    IEnumerator BuildPhase()
    {
        float x, y;
        x = -1.2f;
        y = -1;
        Button Newbutton = Instantiate(Spawner, Camera.main.WorldToScreenPoint(new Vector3(x, y)), Quaternion.identity, Canvas.transform);
        Newbutton.GetComponent<UnitSpawner>().Unit = Tower;
        Newbutton.GetComponentInChildren<TextMeshProUGUI>().text = $"Spawn Tower - 1 Bricks";
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
        UnitsDatabase UnitTypes = GameObject.FindGameObjectWithTag("UnitTypes").GetComponent<UnitsDatabase>();
        foreach (GameObject Unit in hand)
        {
            foreach (Unit unittype in UnitTypes.units)
            {
                if (unittype.name == Unit.name)
                {
                    Button Newbutton = Instantiate(Spawner, Camera.main.WorldToScreenPoint(new Vector3(x, y)), Quaternion.identity, Canvas.transform);
                    Newbutton.GetComponent<UnitSpawner>().Unit = Unit;
                    Newbutton.GetComponentInChildren<TextMeshProUGUI>().text = $"Spawn {Unit.name} - {unittype.Cost} Bricks";
                }
            }
            x += 1.3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
