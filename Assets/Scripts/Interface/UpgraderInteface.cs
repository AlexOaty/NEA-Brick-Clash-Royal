using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgraderInteface : MonoBehaviour
{
    public List<Ability> abilities;
    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        Text.text = "Kight Upgrades:";
        foreach (Ability ability in abilities)
        {
            if (ability.purchased)
                Text.text += $"\n{ability.attribute} {ability.change * 100}%";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
