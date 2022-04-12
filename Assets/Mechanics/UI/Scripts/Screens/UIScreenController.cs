using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Контроллер игровых экранов (сринов).
/// Ведёт список UI-экранов игры, отображает и скрывает их.
/// Добавляется отдельным дочерним объектом в GameManager.
/// </summary>
public class UIScreenController : MonoBehaviour
{
    [SerializeField, Tooltip("Экран окончания игры")]
    private UIScreen gameOverScreen = null;

    [SerializeField, Tooltip("Экран паузы")]
    private UIScreen pauseScreen = null;

    [SerializeField, Tooltip("Экран главного меню")]
    private UIScreen mainMenuScreen = null;

    [SerializeField, Tooltip("Экран настроек")]
    private UIScreen optionsScreen = null;

    [SerializeField, Tooltip("Экран титров")]
    private UIScreen creditsScreen = null;

    [SerializeField, Tooltip("Затемняющий экран")]
    private UIScreen blackoutScreen = null;

    public UIScreen CurrentScreen => GetCurrentScreen();

    /// <summary>
    /// Список всех экранов
    /// </summary>
    private readonly List<UIScreen> screens = new List<UIScreen>();

    /// <summary>
    /// Скрыть текущий экран
    /// </summary>
    public void HideCurrentScreen()
    {
        SetActiveScreen(null);
    }

    public void ShowMainMenuScreen()
    {
        SetActiveScreen(mainMenuScreen);
    }

    /// <summary>
    /// Показать экран окончания игры
    /// </summary>
    public void ShowGameOverScreen()
    {
        SetActiveScreen(gameOverScreen);
    }

    /// <summary>
    /// Показать экран паузы
    /// </summary>
    public void ShowPauseScreen()
    {
        SetActiveScreen(pauseScreen);
    }

    public void ShowCreditsScreen()
    {
        SetActiveScreen(creditsScreen);
    }

    /// <summary>
    /// Показать указанный экран как дочерний
    /// </summary>
    /// <param name="childScreen"></param>
    public void ShowChildScreen(UIScreen childScreen)
    {
        if (childScreen != null)
        {
            childScreen.SetParent(CurrentScreen);
            SetActiveScreen(childScreen);
        }
    }

    /// <summary>
    /// Показать родительский экран текущего экрана
    /// </summary>
    public void ShowParentScreen()
    {
        if (CurrentScreen != null && CurrentScreen.ParentScreen != null)
        {
            SetActiveScreen(CurrentScreen.ParentScreen);
        }
    }

    public void ShowBlackScreen()
    {
        SetActiveScreen(blackoutScreen);
    }

    public void Init(GameSettings gameSettings, UIEventMediator uiEventMediator)
    {
        screens.Add(gameOverScreen);
        screens.Add(pauseScreen);
        screens.Add(optionsScreen);
        screens.Add(mainMenuScreen);
        screens.Add(blackoutScreen);
        screens.Add(creditsScreen);

        InitScreens(gameSettings, uiEventMediator);

        SetActiveScreen(null);

        uiEventMediator.ReturnToPreviousScreenRequested += ShowParentScreen;
        uiEventMediator.ShowChildScreenRequested += ShowChildScreen;
    }

    private void InitScreens(GameSettings gameSettings, UIEventMediator uiEventMediator)
    {
        foreach (var item in screens)
        {
            item.Init(gameSettings, uiEventMediator);
        }
    }
    /// <summary>
    /// Получить текущий активный экран
    /// </summary>
    /// <returns></returns>
    private UIScreen GetCurrentScreen()
    {
        foreach (var item in screens)
        {
            if (item.IsActive)
            {
                return item;
            }
        }
        return null;
    }
    /// <summary>
    /// Показать указанный экран. Одновременно может быть активен только один экран,
    /// поэтому все остальные будут скрыты.
    /// </summary>
    /// <param name="screen"></param>
    private void SetActiveScreen(UIScreen screen)
    {
        foreach (var item in screens)
        {
            item.SetActive(item == screen);
        }
    }

}
