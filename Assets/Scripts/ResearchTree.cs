using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResearchTree : MonoBehaviour
{
    public List<Ability> abilities;
    GameObject[] Buttons;

    private void Start()
    {
        Buttons = GameObject.FindGameObjectsWithTag("Tree");
    }

    private void Update()
    {
        //foreach (var Button in Buttons)
        //{
        //    if (abilities[int.Parse(Button.name)].unlocked)
        //    {
        //        Button.SetActive(true);
        //    }
        //}
    }

    public void Purchase(int Location)
    {
        foreach (Ability ability in abilities)
        {
            if (ability.Location == Location)
            {
                abilities[ability.left].unlocked = true;
                abilities[ability.right].unlocked = true;


            }
        }
    }




}
