using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class SubtitleBehaviour : PlayableBehaviour
{
    [TextArea]
    public string subtitleText;

    //public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    //{
    //    TextMeshProUGUI text = playerData as TextMeshProUGUI;
    //    text.text = subtitleText;
    //    Color textColor = text.color;
    //    textColor.a = info.weight;
    //    text.color = textColor;
    //}
}
