using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]

public class Ability : ScriptableObject
{
    public string attribute;
    public int change;
    public int cost;
    public bool unlocked;
}
