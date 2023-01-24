using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]

public class Ability : ScriptableObject
{
    public int Location;
    public string attribute;
    public int change;
    public int cost;
    public bool unlocked;
    public int left;
    public int right;
    public GameObject Button;
}
