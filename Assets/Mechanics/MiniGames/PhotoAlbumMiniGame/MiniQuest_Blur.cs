using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniQuest_Blur : MiniQuest
{
    public override void MiniQuestStart()
    {
        base.MiniQuestStart();

        Invoke(nameof(MiniQuestEnded), 2f);
    }
}
