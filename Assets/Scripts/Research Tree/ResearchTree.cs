using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class ResearchTree : ScriptableObject
{
    public List<Ability> Data;

    public ResearchTree(List<Ability> Data)
    {
        this.Data = Data;
    }

    public Ability Purchase(string Ability)
    {
        foreach (Ability ability in Data)
        {
            if (ability.unlocked && !ability.purchased && ability.ID == Ability)
            {
                ability.purchased = true;
                if (ability.left != -1)
                {
                    Data[ability.left].unlocked = true;
                    Debug.Log("Left Unlocked");
                }
                if (ability.right != -1)
                {
                    Data[ability.right].unlocked = true;
                    Debug.Log("Right Unlocked");
                }
                return ability;
            }
        }
        Debug.Log("Cannot Purchase Ability because it is not unlocked or has been purchased already");
        return null;
    }


}
