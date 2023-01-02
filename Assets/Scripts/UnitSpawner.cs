using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class UnitSpawner : MonoBehaviour
{
    public GameObject Unit;
    UnitManager unitManager;
    GameManager gameManager;
    public bool isActive = false;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        unitManager = Unit.GetComponent<UnitManager>();
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
        if (Input.GetMouseButton(0) && gameManager.BrickNum >= 1)
        {
            Instantiate(Unit, new Vector3(MousePosition.x, MousePosition.y, 0f), Quaternion.identity);
            isActive = false;
            gameManager.BrickNum--;
        }
    }
}
