using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PazzleDestroyer : MonoBehaviour
{
    [SerializeField]
    private MiniQuest miniQuest;

    private int readyParts = -1;
    private int partsCount;

    private void Start()
    {
        int photoPartsCount = transform.childCount;

        for (int i = 0; i < photoPartsCount; i++)
        {
            
            PazzlePartMovement tmp = transform.GetChild(i).gameObject.AddComponent<PazzlePartMovement>();

            tmp.SetPazzleDestroyer(this);
            tmp.SaveStartPosition();
            tmp.SetRandomPosition();
        }

        partsCount = photoPartsCount;
        readyParts = 0;
    }

    public void AddReadyPartToCount()
    {
        readyParts++;

        if (readyParts == partsCount)
        {
            miniQuest.MiniQuestEnded();
        }
    }
}
