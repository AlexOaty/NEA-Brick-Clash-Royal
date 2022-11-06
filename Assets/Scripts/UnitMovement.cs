using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    UnitBehaviour mUnit;
    GameObject Unit;
    GameObject Path;
    Rigidbody2D Rigidbody;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Unit = gameObject;
        Rigidbody = GetComponent<Rigidbody2D>();
        mUnit = new UnitBehaviour(Unit, Rigidbody, speed);
        Path = mUnit.FindPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Unit.transform.position, Path.transform.position) >= 0.1)
        {
            mUnit.FollowPath();
        }
    }
}
