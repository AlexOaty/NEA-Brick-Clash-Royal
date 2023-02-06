using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    ResearchTree ResearchTree;
    public List<Ability> Abilities;
    List<Unit> units;
    UnitsDatabase unitsDatabase;
    // Start is called before the first frame update
    void Start()
    {
        unitsDatabase = GameObject.FindGameObjectWithTag("UnitTypes").GetComponent<UnitsDatabase>();
        units = unitsDatabase.units;
        BuildTree();
        ResearchTree = new ResearchTree(Abilities);
    }

    public void BuildTree()
    {
        for (int i = 0; i < Abilities.Count; i++)
        {
            if (Abilities[i].LeftID == "-1")
            {
                Abilities[i].left = -1;
            }
            if (Abilities[i].RightID == "-1")
            {
                Abilities[i].right = -1;
            }
            for (int j = 0; j < Abilities.Count; j++)
            {
                if (Abilities[i].LeftID == Abilities[j].ID)
                    Abilities[i].left = j;

                else if (Abilities[i].RightID == Abilities[j].ID)
                    Abilities[i].right = j;
            }
        }
    }

    public void Purchase(string Ability)
    {
        if(ResearchTree.Purchase(Ability))
        {

        }
    }
}
