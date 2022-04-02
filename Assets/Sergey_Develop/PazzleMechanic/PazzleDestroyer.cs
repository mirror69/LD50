using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PazzleDestroyer : MonoBehaviour
{
    public static int readyParts = -1;
    private int partsCount;

    private void Start()
    {
        PazzlePartMovement[] parts = transform.GetComponentsInChildren<PazzlePartMovement>();

        for (int i =0; i<parts.Length; i++)
        {
            parts[i].SaveStartPosition();
            parts[i].SetRandomPosition();
        }

        partsCount = parts.Length;
        readyParts = 0;
    }

    private void Update()
    {
        if (readyParts==partsCount)
        {
            Debug.Log("Победа");
            //Победа (событие или другой передающий управление элемент)
        }
    }
}
