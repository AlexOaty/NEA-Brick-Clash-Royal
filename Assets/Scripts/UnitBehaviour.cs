using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour 
{

    public GameObject Unit;

    public UnitBehaviour(GameObject Unit)
    {
        this.Unit = Unit;
    }

    public void Move()
    {
        Unit.transform.position = new Vector3(0f, 0f, 0f);
    }
}
