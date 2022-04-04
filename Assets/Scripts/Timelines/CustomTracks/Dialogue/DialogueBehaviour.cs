using System;
using UnityEngine.Playables;

[Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
	public DialogueMode mode;
	public string characterName;
    public string dialogueLine;
    public int dialogueSize;

	public bool hasToPause = false;

	private bool clipPlayed = false;

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		if(!clipPlayed
			&& info.weight > 0f)
		{
            if (mode == DialogueMode.CharacterSpeech)
            {
				UIManager.Instance.ShowCharacterSpeechPanel(characterName, dialogueLine, hasToPause);
			}
            else
            {
				UIManager.Instance.ShowTutorialPanel(characterName, dialogueLine, hasToPause);
			}

			clipPlayed = true;
		}
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		UIManager.Instance.HideAll();

		clipPlayed = false;
	}

	public enum DialogueMode
    {
		CharacterSpeech,
		Tutorial
    }
}
