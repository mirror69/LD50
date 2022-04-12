using DG.Tweening;
using UnityEngine;

/// <summary>
/// Класс экрана игры. 
/// Экран представляет собой UI-панель, показывающуюся поверх окна игры 
/// при возникновении какого-либо игрового события. 
/// Это может быть экран главного меню, экран с информацией о пройденном уровне, экран Game over, и т.д.)
/// Данный скрипт должен быть добавлен на UI-панель экрана.
/// </summary>
public abstract class UIScreen : MonoBehaviour
{
    [SerializeField]
    [Tooltip("")]
    private ChildScreenButton[] childScreenButtons = null;

    [SerializeField]
    [Tooltip("")]
    private BackScreenButton backScreenButton = null;

    [SerializeField]
    [Tooltip("")]
    protected CanvasGroup buttonGroup = null;

    /// <summary>
    /// Активен ли экран в данный момент
    /// </summary>
    public bool IsActive => gameObject.activeSelf;

    /// <summary>
    /// Родительский экран, на который будет произведён переход после
    /// закрытия данного окна
    /// </summary>
    public UIScreen ParentScreen { get; private set; } = null;

    protected UIEventMediator _uiEventMediator;
    protected GameSettings _gameSettings;

    public virtual void Init(GameSettings gameSettings, UIEventMediator uiEventMediator)
    {
        _uiEventMediator = uiEventMediator;
        _gameSettings = gameSettings;

        if (childScreenButtons.Length > 0)
        {
            foreach (var item in childScreenButtons)
            {
                item.Init(_uiEventMediator);
            }
        }

        if (backScreenButton != null)
        {
            backScreenButton.Init(_uiEventMediator);
        }     
    }

    /// <summary>
    /// Показать/скрыть экран
    /// </summary>
    /// <param name="active"></param>
    public virtual void SetActive(bool active)
    {
        if (active != IsActive)
        {
            gameObject.SetActive(active);
        }
        if (!active)
        {
            ParentScreen = null;
        }
    }
    
    /// <summary>
    /// Установить родительский экран
    /// </summary>
    /// <param name="parentScreen"></param>
    public void SetParent(UIScreen parentScreen)
    {
        ParentScreen = parentScreen;
    }

    protected void HideButtons()
    {
        buttonGroup.alpha = 0;
        buttonGroup.gameObject.SetActive(true);
    }

    protected void ShowButtonsSmooth()
    {
        buttonGroup.gameObject.SetActive(true);
        buttonGroup.DOFade(1, _gameSettings.UISettings.ButtonsFadeDuration);
    }
}
