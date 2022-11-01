using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    UnitBehaviour mUnit;
    GameObject Unit;
    // Start is called before the first frame update
    void Start()
    {
        Unit = gameObject;
        mUnit = new UnitBehaviour(Unit);
        mUnit.Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
