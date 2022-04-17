using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PazzleDestroyer : MonoBehaviour
{
    [HideInInspector]
    public MiniQuest MiniQuest;

    public int readyParts = -1;
    public int partsCount;
    private bool isDestroyed;

    public int MinSortingLayer { get; private set; }

    public void DestroyPhoto ()
    {
        if (isDestroyed)
            return;

        int photoPartsCount = transform.childCount;
        MinSortingLayer = int.MaxValue;
        for (int i = 0; i < photoPartsCount; i++)
        {
            PazzlePartMovement tmp = transform.GetChild(i).gameObject.AddComponent<PazzlePartMovement>();

            tmp.Init(this);
            tmp.SetRandomPosition();

            if (tmp.SpriteSortingLayer < MinSortingLayer)
            {
                MinSortingLayer = tmp.SpriteSortingLayer;
            }
        }

        partsCount = photoPartsCount;
        readyParts = 0;

        isDestroyed = true;
    }

    public void AddReadyPartToCount()
    {
        readyParts++;

        if (readyParts == partsCount)
        {
            MiniQuest.MiniQuestEnded();
        }
    }
}
