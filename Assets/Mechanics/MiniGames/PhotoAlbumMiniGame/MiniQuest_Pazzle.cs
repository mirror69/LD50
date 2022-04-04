using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniQuest_Pazzle : MiniQuest
{
    [SerializeField]
    private PazzleDestroyer pazzleDestroyer;

    public override void MiniQuestStart()
    {
        base.MiniQuestStart();

        pazzleDestroyer.MiniQuest = this;
        pazzleDestroyer.DestroyPhoto();
        //анимация перелистыывания?
    }
}
