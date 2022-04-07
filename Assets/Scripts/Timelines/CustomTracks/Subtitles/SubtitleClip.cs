using UnityEngine;
using UnityEngine.Playables;

public class SubtitleClip : PlayableAsset
{
    public SubtitleBehaviour template = new SubtitleBehaviour();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph, template);

        return playable;
    }
}
