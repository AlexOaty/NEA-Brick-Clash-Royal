using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Hand
{
    List<GameObject> Data;
    GameObject UnitPlayed;
    int Index;

    public Hand()
    {
        Data = new List<GameObject>();
        Index = 0;
        UnitPlayed = null;
    }

    public void Play(GameObject Unit)
    {
        UnitPlayed = Unit;
        for (int i = 0; i < Data.Count; i++)
        {
            if (Data[i] == Unit)
            {
                Data[i] = null;
                Index = i;
                RemoveCard();
            }
        }
    }

    public void RemoveCard()
    {
        for (int i = Index; i < Data.Count; i++)
        {
            if (i != Data.Count - 1)
                Data[i] = Data[i + 1];
            else
            {
                Data[i] = UnitPlayed;
            }
        }
    }

    public void Add(GameObject Card)
    {
        Data.Add(Card);
    }

    public List<GameObject> GetData()
    {
        return Data;
    }
}
