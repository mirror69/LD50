using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniQuest_DustPhoto : MiniQuest
{
    public override void MiniQuestStart()
    {
        Invoke(nameof(MiniQuestEnded), 3f);
    }
}