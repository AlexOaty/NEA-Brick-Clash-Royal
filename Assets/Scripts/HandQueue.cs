using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandQueue
{
    public GameObject[] Data;
    public int Front;
    int Rear;
    int Size;

    public HandQueue(int Size)
    {
        Front = -1;
        Rear = -1;
        this.Size = Size;
        Data = new GameObject[Size];
    }

    public void Add(GameObject NewData)
    {
        if (CheckIsFull())
        {
            Debug.Log("Queue Full");
        }
        else if (CheckIsEmpty())
        {
            Front = 0;
            Rear = 0;
            Data[Rear] = NewData;
        }
        else if (Rear == Size-1)
        {
                Rear = 0;
                Data[Rear] = NewData;
        }
        else
        {
            Rear++;
            Data[Rear] = NewData;
        }
    }

    bool CheckIsFull()
    {
        if (Front == Rear + 1 || (Front == 0 && Rear == Size - 1))
            return true;
        else
            return false;
    }

    bool CheckIsEmpty()
    {
        if (Front == -1 && Rear == -1)
            return true;
        else 
            return false;
    }

    public void Up()
    {
        if (Front == Size - 1)
        {
            Front = 0;
            Rear++;
        }
        else if (Rear == Size - 1)
        {
            Rear = 0;
            Front++;
        }
        else
        {
            Front++;
            Rear++;
        }
    }
}
