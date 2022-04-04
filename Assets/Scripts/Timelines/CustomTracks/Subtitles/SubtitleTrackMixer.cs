using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        if (!text)
        {
            return;
        }

        string currentText = "";
        float currentAlpha = 0;

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0)
            {
                ScriptPlayable<SubtitleBehaviour> inputPlayable =
                    (ScriptPlayable<SubtitleBehaviour>)playable.GetInput(i);

                SubtitleBehaviour input = inputPlayable.GetBehaviour();
                if (input.subtitleText != "")
                {
                    currentText = input.subtitleText;
                }
                else
                {
                    currentText = text.text;
                }
                
                currentAlpha = inputWeight;
            }
        }

        text.text = currentText;
        Color textColor = text.color;
        textColor.a = currentAlpha;
        text.color = textColor;
    }
}
