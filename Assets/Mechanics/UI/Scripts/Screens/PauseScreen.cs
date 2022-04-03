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
    [Tooltip("Кнопка выхода из игры")]
    private Button quitButton = null;

    public override void Init(UIEventMediator uiEventMediator)
    {
        base.Init(uiEventMediator);
        resumeButton.onClick.AddListener(_uiEventMediator.RequestResumeGame);
        quitButton.onClick.AddListener(_uiEventMediator.RequestQuit);
    }
}
