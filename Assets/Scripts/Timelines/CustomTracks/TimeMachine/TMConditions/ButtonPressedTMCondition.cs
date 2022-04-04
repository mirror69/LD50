using UnityEngine;

/// <summary>
/// Условие TimeMachine трека. Проверяет, была ли нажата указанная клавиша
/// </summary>
public class ButtonPressedTMCondition : TMCondPolled
{
	[SerializeField]
	private KeyCode ButtonKeyCode;

	protected override bool EvaluateCondition()
    {
		return Input.GetKeyUp(ButtonKeyCode);
	}
}
