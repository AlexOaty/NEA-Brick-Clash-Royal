using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    ResearchTree ResearchTree;
    public List<Ability> Abilities;
    List<Unit> units;
    UnitsDatabase unitsDatabase;
    Ability ability;
    GameObject[] ButtonsGO;
    TextMeshProUGUI[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        unitsDatabase = GameObject.FindGameObjectWithTag("UnitTypes").GetComponent<UnitsDatabase>();
        units = unitsDatabase.units;
        BuildTree();
        ResearchTree = new ResearchTree(Abilities);
        ButtonsGO = GameObject.FindGameObjectsWithTag("Tree");
        for (int i = 0; i < ButtonsGO.Length; i++)
        {
            buttons[i] = ButtonsGO[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Update()
    {

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
        ability = ResearchTree.Purchase(Ability);
        if(ability != null)
        {
            foreach (Unit unit in units)
            {
                if (unit.name == "Knight")
                {
                    if (ability.attribute == "AttackSpeed")
                    {
                        unit.AttackSpeed *= (1 - ability.change);
                    }
                    else if (ability.attribute == "Health")
                    {
                        unit.Health *= (1 + ability.change);
                    }
                    else if (ability.attribute == "Speed")
                    {
                        unit.speed *= (1 + ability.change);
                    }
                }
            }
        }
    }
}
