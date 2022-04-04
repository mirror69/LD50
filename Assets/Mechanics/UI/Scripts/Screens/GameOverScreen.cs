using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ёкран, по€вл€ющийс€ при проигрыше.
/// —крипт следует добавл€ть на UI-панель, котора€ должна по€вл€тьс€ при проигрыше.
/// </summary>
public class GameOverScreen : UIScreen
{
    [SerializeField]
    [Tooltip(" нопка рестарта")]
    private Button restartButton = null;

    [SerializeField]
    [Tooltip(" нопка главного меню")]
    private Button mainMenuButton = null;

    public override void Init(UIEventMediator uiEventMediator)
    {
        base.Init(uiEventMediator);
        restartButton.onClick.AddListener(_uiEventMediator.RequestStartNewGame);
        mainMenuButton.onClick.AddListener(_uiEventMediator.RequestMainMenu);
    }
}
