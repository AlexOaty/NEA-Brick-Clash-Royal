using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    UnitBehaviour mUnit;
    GameObject Unit;
    Rigidbody2D Rigidbody;
    public float speed;
    public bool IsEnemy;
    public float Health;
    public float Damage;
    public float AttackSpeed;
    bool AttackUnitRun;

    // Start is called before the first frame update
    void Start()
    {
        Unit = gameObject;
        Rigidbody = GetComponent<Rigidbody2D>();
        mUnit = new UnitBehaviour(Unit, Rigidbody, speed, IsEnemy, Health, Damage, AttackSpeed);
        mUnit.FindPath();
        TextMeshPro TMP = new TextMeshPro();
        TMP.text = "Hello";

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Health <= 0)
            gameObject.SetActive(false);

        if (!mUnit.EndOfPath && !mUnit.Fighting)
        {
            mUnit.FollowPath();
            mUnit.Fighting = mUnit.CheckArea();
        }
        else if(mUnit.Fighting && !AttackUnitRun)
            StartCoroutine(AttackUnit());
    }

    IEnumerator AttackUnit()
    {
        AttackUnitRun = true;
        Debug.Log($"{Unit.tag} Attacking");
        yield return new WaitForSeconds(AttackSpeed);
        UnitMovement Enemy = (UnitMovement)mUnit.CurrentOpponent.GetComponent("UnitMovement");
        Enemy.Health -= Damage;
        if (!Enemy.isActiveAndEnabled)
            mUnit.Fighting = false;
        AttackUnitRun = false;
    }
}
