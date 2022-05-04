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
    private GameSoundController SoundController;
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

    private AsyncOperation loadingGameSceneOperation;

    private void Start()
    {
        _uiEventMediator = new UIEventMediator();
        SoundController.Init(_uiEventMediator);
        UIScreenController.Init(GameSettings, _uiEventMediator);

        UIScreenController.ShowBlackScreen();
        Initializer.InitializationCompleted += StartGame;
        Initializer.Instance.Init();
    }

    private void OnDisable()
    {
        Initializer.InitializationCompleted -= StartGame;
    }

    private void StartGame()
    {
        Initializer.InitializationCompleted -= StartGame;

        _uiEventMediator.QuitRequested += Quit;
        _uiEventMediator.StartNewGameRequested += StartNewGame;
        _uiEventMediator.MainMenuRequested += LoadMainMenu;

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
        StartTimeline.Play();   

        yield return new WaitForSeconds(3.5f);

        loadingGameSceneOperation = SceneManager.LoadSceneAsync(GameSettings.SceneSettings.GameSceneName);
        loadingGameSceneOperation.allowSceneActivation = false;

        UIScreenController.HideCurrentScreen();
        TextInCutScene.gameObject.SetActive(true);
        TextInCutScene.OnStartCutSceneEnded += LoadNewGame;
    }

    private void LoadNewGame()
    {
        loadingGameSceneOperation.allowSceneActivation = true;
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(GameSettings.SceneSettings.MainMenuSceneName);
    }
}
