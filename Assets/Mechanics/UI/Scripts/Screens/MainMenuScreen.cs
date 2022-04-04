using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Экран главного меню.
/// Скрипт следует добавлять на UI-панель, которая должна появляться при входе в игру.
/// </summary>
public class MainMenuScreen : UIScreen
{
    [SerializeField]
    [Tooltip("Кнопка начала новой игры")]
    private Button playButton = null;

    [SerializeField]
    [Tooltip("Кнопка выхода из игры")]
    private Button quitButton = null;
    
    public override void Init(UIEventMediator uiEventMediator)
    {
        base.Init(uiEventMediator);
        //playButton.onClick.AddListener(() => GameManager.Instance.Controller.StartNewGame());
        //quitButton.onClick.AddListener(() => GameManager.Instance.Controller.Quit());
        playButton.onClick.AddListener(_uiEventMediator.RequestStartNewGame);
        quitButton.onClick.AddListener(_uiEventMediator.RequestQuit);
    }
}
