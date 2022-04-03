using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : UIScreen
{
    [SerializeField]
    [Tooltip("Кнопка главного меню")]
    private Button mainMenuButton = null;
    [SerializeField]
    private Transform MovingPanel;
    [SerializeField]
    private Transform StartPoint;
    [SerializeField]
    private Transform EndPoint;

    public override void Init(UIEventMediator uiEventMediator)
    {
        base.Init(uiEventMediator);
        mainMenuButton.onClick.AddListener(_uiEventMediator.RequestMainMenu);
    }

    public override void SetActive(bool active)
    {
        MovingPanel.position = StartPoint.position;

        base.SetActive(active);

        MovingPanel.DOMoveY(EndPoint.position.y, 60);
    }
}
