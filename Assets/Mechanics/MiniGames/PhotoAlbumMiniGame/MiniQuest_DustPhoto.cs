using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniQuest_DustPhoto : MiniQuest
{
    [SerializeField]
    private float PercentToWin;

    private void OnEnable()
    {
        EraseImageLayer.OnEraseImageEnded += EraseImageLayer_OnEraseImageEnded;
    }

    private void OnDisable()
    {
        EraseImageLayer.OnEraseImageEnded -= EraseImageLayer_OnEraseImageEnded;
    }

    private void EraseImageLayer_OnEraseImageEnded(EraseImageLayer obj)
    {
        MiniQuestEnded();
    }

    public override void MiniQuestStart()
    {
        base.MiniQuestStart();
        EraseImageLayer.PercentToWin = this.PercentToWin;
    }
}
