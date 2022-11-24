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
    public bool IsEnemy;
    public float Health;
    public float Damage;
    public float AttackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Unit = gameObject;
        Rigidbody = GetComponent<Rigidbody2D>();
        mUnit = new UnitBehaviour(Unit, Rigidbody, speed, IsEnemy, Health, Damage, AttackSpeed);
        Path = mUnit.FindPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mUnit.EndOfPath && !mUnit.Fighting)
        {
            mUnit.FollowPath();
            mUnit.Fighting = mUnit.CheckArea();
        }
        else if (mUnit.Fighting)
            mUnit.AttackUnit();
        
    }
}
