using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : UIScreen
{
    [SerializeField]
    [Tooltip("Кнопка главного меню")]
    private Button mainMenuButton = null;
    [SerializeField]
    private RectTransform MovingPanel;

    public override void Init(GameSettings gameSettings, UIEventMediator uiEventMediator)
    {
        base.Init(gameSettings, uiEventMediator);
        mainMenuButton.onClick.AddListener(_uiEventMediator.RequestMainMenu);

        
        Vector3 position = MovingPanel.anchoredPosition;
        position.y -= GetComponentInParent<RectTransform>().rect.height;
        MovingPanel.anchoredPosition = position;

        HideButtons();
    }

    public override void SetActive(bool active)
    {
        base.SetActive(active);

        if (active)
        {
            Invoke(nameof(ShowButtonsSmooth), _gameSettings.UISettings.CreditsButtonsShowDelay);
            MovingPanel.DOMoveY(MovingPanel.rect.height, 160 / _gameSettings.UISettings.CreditsMoveSpeed);
        }
        else
        {
            HideButtons();
        }
    }
}
