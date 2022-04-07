using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameSettings GameSettings;
    [SerializeField]
    private UIScreenController UIScreenController;
    [SerializeField]
    private KeyPressController KeyPressController;
    [SerializeField]
    private PlayableDirector StartTimeline;
    [SerializeField]
    private TextInCutScene TextInCutScene;
    [SerializeField]
    private AudioSource MainMenuMusic;

    [SerializeField]
    private Image fadeScreenImage;

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
        StartCoroutine(GameStarting());
    }

    private IEnumerator GameStarting()
    {
        fadeScreenImage.gameObject.SetActive(true);
        MainMenuMusic.DOFade(0, 1);
        fadeScreenImage.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        UIScreenController.HideCurrentScreen();
        StartTimeline.Play();
        TextInCutScene.gameObject.SetActive(true);
        TextInCutScene.OnStartCutSceneEnded += LoadNewGame;
    }

    private void LoadNewGame()
    {
        SceneManager.LoadScene(GameSettings.SceneSettings.GameSceneName);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(GameSettings.SceneSettings.MainMenuSceneName);
    }
}
