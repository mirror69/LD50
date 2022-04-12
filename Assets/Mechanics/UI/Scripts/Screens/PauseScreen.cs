using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Экран паузы.
/// Скрипт следует добавлять на UI-панель, которая должна появляться, когда игра ставится на паузу.
/// </summary>
public class PauseScreen : UIScreen
{
    [SerializeField]
    [Tooltip("Кнопка возврата в игру")]
    private Button resumeButton = null;

    [SerializeField]
    [Tooltip("Кнопка выхода в меню")]
    private Button mainMenuButton = null;

    public override void Init(GameSettings gameSettings, UIEventMediator uiEventMediator)
    {
        base.Init(gameSettings, uiEventMediator);
        resumeButton.onClick.AddListener(_uiEventMediator.RequestResumeGame);
        mainMenuButton.onClick.AddListener(_uiEventMediator.RequestMainMenu);
    }
}
