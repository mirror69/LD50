using UnityEngine;

/// <summary>
/// Менеджер диалоговых панелей.
/// </summary>
public class UIManager : Singleton<UIManager>
{
	[SerializeField, Tooltip("Панель речи персонажа")]
	private DialoguePanel characterSpeechPanel;

	[SerializeField, Tooltip("Панель подсказки игры")]
	private DialoguePanel tutorialPanel;

	/// <summary>
	/// Скрыть все панели
	/// </summary>
	public void HideAll()
    {
		characterSpeechPanel.SetActive(false);
		tutorialPanel.SetActive(false);
	}

	/// <summary>
	/// Показать панель речи
	/// </summary>
	/// <param name="charName"></param>
	/// <param name="dialogueLine"></param>
	/// <param name="canSkip"></param>
	public void ShowCharacterSpeechPanel(string charName, string dialogueLine, bool canSkip)
    {
		tutorialPanel.SetActive(false);
		ShowDialoguePanel(characterSpeechPanel, charName, dialogueLine, canSkip);
	}

	/// <summary>
	/// Показать панель подсказки
	/// </summary>
	/// <param name="charName"></param>
	/// <param name="dialogueLine"></param>
	/// <param name="canSkip"></param>
	public void ShowTutorialPanel(string charName, string dialogueLine, bool canSkip)
	{
		characterSpeechPanel.SetActive(false);
		ShowDialoguePanel(tutorialPanel, charName, dialogueLine, canSkip);
	}

	/// <summary>
	/// Показать диалоговую панель
	/// </summary>
	/// <param name="panel"></param>
	/// <param name="charName"></param>
	/// <param name="dialogueLine"></param>
	/// <param name="canSkip"></param>
	private void ShowDialoguePanel(DialoguePanel panel, string charName, string dialogueLine, bool canSkip)
    {
		panel.SetDialogueParameters(charName, dialogueLine, canSkip);
		panel.SetActive(true);
	}
}
