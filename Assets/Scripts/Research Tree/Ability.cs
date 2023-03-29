using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]

public class Ability : ScriptableObject
{
    public string ID;
    public string attribute;
    public float change;
    public int cost;
    public bool unlocked;
    public bool purchased;
    public string LeftID;
    public string RightID;
    public int left;
    public int right;
}
