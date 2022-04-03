using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniQuest : MonoBehaviour
{
    public static event Action<MiniQuest> OnMiniQuestEnded;

    public abstract void MiniQuestStart();

    public virtual void MiniQuestEnded()
    {
        gameObject.SetActive(false);
        OnMiniQuestEnded?.Invoke(this);
    }
}
