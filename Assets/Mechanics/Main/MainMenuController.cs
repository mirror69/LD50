using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameSettings GameSettings;
    [SerializeField]
    private UIScreenController UIScreenController;
    [SerializeField]
    private KeyPressController KeyPressController;

    private UIEventMediator _uiEventMediator;

    void Start()
    {
        _uiEventMediator = new UIEventMediator();

        _uiEventMediator.QuitRequested += Quit;
        _uiEventMediator.StartNewGameRequested += StartNewGame;
        _uiEventMediator.MainMenuRequested += LoadMainMenu;

        UIScreenController.Init(_uiEventMediator);
        UIScreenController.ShowMainMenuScreen();

        KeyPressController.Init(_uiEventMediator, UIScreenController);
        KeyPressController.SetMenuMode();
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void StartNewGame()
    {
        SceneManager.LoadScene(GameSettings.SceneSettings.GameSceneName);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(GameSettings.SceneSettings.MainMenuSceneName);
    }
}
