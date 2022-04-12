using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����, ������������ ��� ���������.
/// ������ ������� ��������� �� UI-������, ������� ������ ���������� ��� ���������.
/// </summary>
public class GameOverScreen : UIScreen
{
    [SerializeField]
    [Tooltip("������ ��������")]
    private Button restartButton = null;

    [SerializeField]
    [Tooltip("������ �������� ����")]
    private Button mainMenuButton = null;

    public override void Init(GameSettings gameSettings, UIEventMediator uiEventMediator)
    {
        base.Init(gameSettings, uiEventMediator);
        restartButton.onClick.AddListener(_uiEventMediator.RequestStartNewGame);
        mainMenuButton.onClick.AddListener(_uiEventMediator.RequestMainMenu);

        HideButtons();
    }

    public override void SetActive(bool active)
    {
        base.SetActive(active);

        if (active)
        {
            Invoke(nameof(ShowButtonsSmooth), _gameSettings.UISettings.GameOverButtonsShowDelay);
        }
        else
        {
            HideButtons();
        }
    }
}
