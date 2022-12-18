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
    public void SpawnUnit()
    {
        Instantiate(Unit, new Vector3(-0.069f, -0.338f, 0), Quaternion.identity);
    }
}
