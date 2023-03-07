using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class UnitSpawner : MonoBehaviour
{
    int BrickNum;
    UnitManager unitManager;
    GameManager gameManager;
    public GameObject Unit;
    public bool isActive = false;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        unitManager = Unit.GetComponent<UnitManager>();
        UnitsDatabase UnitTypes = GameObject.FindGameObjectWithTag("UnitTypes").GetComponent<UnitsDatabase>();
        foreach (Unit UnitType in UnitTypes.units)
        {
            if (UnitType.name == unitManager.name)
                BrickNum = UnitType.Cost;
        }
    }
    void Update()
    {
        if (isActive)
            Invoke("Spawn", 0f);
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Spawn()
    {
        Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            isActive = false;
            if (gameManager.BrickNum >= BrickNum)
            {
                Instantiate(Unit, new Vector3(MousePosition.x, MousePosition.y, 0f), Quaternion.identity);
                gameManager.BrickNum -= BrickNum;
            }
        }
    }
}
