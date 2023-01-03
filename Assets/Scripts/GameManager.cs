using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float BrickNum;
    bool BrickAdding;
    public UnitManager PlayerCastle;
    public UnitManager EnemyCastle;
    public TextMeshProUGUI Bricks;
    public TextMeshProUGUI PlayerCastleHealth;
    public TextMeshProUGUI EnemyCastleHealth;
    
    void Start()
    {
        BrickNum = 0;
        BrickAdding = false;
    }

    // Update is called once per frame
    void Update()
    {
        Bricks.text = ("Bricks: " + BrickNum.ToString());
        PlayerCastleHealth.text = ("Castle Health: " + PlayerCastle.Health.ToString());
        if(BrickNum <5 && !BrickAdding) 
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
