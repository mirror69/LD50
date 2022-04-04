using TMPro;
using UnityEngine;

/// <summary>
/// Панель диалога.
/// Используется для отображения речи персонажа или подсказок игры.
/// </summary>
public class DialoguePanel : MonoBehaviour
{
	[SerializeField, Tooltip("Имя персонажа")]
	private TextMeshProUGUI charNameText = null;

	[SerializeField, Tooltip("Текст диалога")]
	private TextMeshProUGUI dialogueLineText = null;

	[SerializeField, Tooltip("Сообщение о возможности пропустить диалог")]
	private GameObject skipMessage;

	/// <summary>
	/// Активировать/деактивировать панель диалога
	/// </summary>
	/// <param name="active"></param>
	public void SetActive(bool active)
    {
		gameObject.SetActive(active);
	}

	/// <summary>
	/// Установить параметры панели диалога
	/// </summary>
	/// <param name="charName">Имя персонажа</param>
	/// <param name="dialogueLine">Текст диалога</param>
	/// <param name="canSkip">Можно ли пропустить диалог</param>
	public void SetDialogueParameters(string charName, string dialogueLine, bool canSkip)
    {
		charNameText.SetText(charName);
		dialogueLineText.SetText(dialogueLine);

		skipMessage.SetActive(canSkip);
	}
}
