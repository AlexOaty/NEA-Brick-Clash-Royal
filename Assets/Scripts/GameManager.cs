using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float BrickNum;
    bool BrickAdding;
    public TextMeshProUGUI Bricks;
    
    void Start()
    {
        BrickNum = 0;
        BrickAdding = false;
    }

    // Update is called once per frame
    void Update()
    {
        Bricks.text = ("Bricks: " + BrickNum.ToString());
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
