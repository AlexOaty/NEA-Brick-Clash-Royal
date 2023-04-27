using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Hand
{
    GameObject[] Data;
    GameObject UnitPlayed;
    int Index;

    public Hand()
    {
        Data = new GameObject[4];
        Index = 0;
        UnitPlayed = null;
    }

    public void Play(GameObject Unit)
    {
        UnitPlayed = Unit;
        for (int i = 0; i < Data.Length; i++)
        {
            if (Data[i] == Unit)
            {
                Data[i] = null;
                Index = i;
                MoveToBack();
            }
        }
    }

    public void MoveToBack()
    {
        for (int i = Index; i < Data.Length; i++)
        {
            if (i != Data.Length - 1)
                Data[i] = Data[i + 1];
            else
                Data[i] = UnitPlayed;
        }
    }

    public void CreateHand(GameObject[] hand)
    {
        Data = hand;
    }

    public GameObject[] GetData()
    {
        return Data;
    }
}
