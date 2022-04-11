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

    [SerializeField]
    private Image MainMenuButtonImage;
    [SerializeField]
    private TMP_Text MainMenuButtonText;

    public override void Init(GameSettings gameSettings, UIEventMediator uiEventMediator)
    {
        base.Init(gameSettings, uiEventMediator);
        mainMenuButton.onClick.AddListener(_uiEventMediator.RequestMainMenu);

        
        Vector3 position = MovingPanel.anchoredPosition;
        position.y -= GetComponentInParent<RectTransform>().rect.height;
        MovingPanel.anchoredPosition = position;

        mainMenuButton.gameObject.SetActive(false);
    }

    public override void SetActive(bool active)
    {
        base.SetActive(active);

        if (active)
        {
            Invoke(nameof(SetActiveMenuButton), 3);
            MovingPanel.DOMoveY(MovingPanel.rect.height, 160 / _gameSettings.UISettings.CreditsMoveSpeed);
        }
    }

    private void SetActiveMenuButton()
    {
        Color color = MainMenuButtonText.color;
        color.a = 0;
        MainMenuButtonText.color = color;

        color = MainMenuButtonImage.color;
        color.a = 0;
        MainMenuButtonImage.color = color;
        mainMenuButton.gameObject.SetActive(true);

        MainMenuButtonText.DOFade(1, 3);
        MainMenuButtonImage.DOFade(1, 3);
    }
}
