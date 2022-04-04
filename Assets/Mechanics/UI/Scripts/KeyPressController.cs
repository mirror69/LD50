using System.Collections;
using UnityEngine;

public class KeyPressController : MonoBehaviour
{
    //private UIEventMediator _uiEventMediator;
    //private UIScreenController _uiScreenController;

    private MenuKeyListener _menuKeyListener;
    private GameplayKeyListener _gameplayKeyListener;
    private CutsceneKeyListener _cutsceneKeyListener;

    private KeyListener _currentKeyListener;

    public void Init(UIEventMediator uiEventMediator, UIScreenController uiScreenController)
    {
        _menuKeyListener = new MenuKeyListener();
        _gameplayKeyListener = new GameplayKeyListener();
        _cutsceneKeyListener = new CutsceneKeyListener();

        _menuKeyListener.Init(uiEventMediator, uiScreenController);
        _gameplayKeyListener.Init(uiEventMediator);
        _cutsceneKeyListener.Init(uiEventMediator);

        StartCoroutine(CatchingKeys());
    }

    public void SetMenuMode()
    {
        _currentKeyListener = _menuKeyListener;
    }

    public void SetGameplayMode()
    {
        _currentKeyListener = _gameplayKeyListener;
    }

    public void SetNotListeningMode()
    {
        _currentKeyListener = null;
    }

    public void SetCutsceneMode()
    {
        _currentKeyListener = _cutsceneKeyListener;
    }

    private IEnumerator CatchingKeys()
    {
        while (true)
        {
            _currentKeyListener?.CatchKey();
            yield return null;
        }
    }
}

//public class KeyListener : MonoBehaviour
//{
//    public enum ListenerMode
//    {
//        Gameplay,
//        Cutscene,
//        Menu
//    }

//    private ListenerMode currentMode;

//    public void SetMode(ListenerMode mode)
//    {
//        currentMode = mode;
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            OnEscapeKeyPressed();
//        }
//        else if (Input.GetKeyDown(KeyCode.Space))
//        {
//            OnSpaceKeyPressed();
//        }
//        else if (Input.GetKeyDown(KeyCode.R))
//        {
//            OnRKeyPressed();
//        }
//    }

//    private void OnEscapeKeyPressed()
//    {
//        UIScreen currentScreen = GameManager.Instance.Controller.ScreenController.CurrentScreen;
//        if (currentScreen != null && currentScreen.ParentScreen != null)
//        {
//            GameManager.Instance.Controller.ScreenController.ShowParentScreen();
//            return;
//        }

//        if (currentMode == ListenerMode.Gameplay)
//        {
//            if (GameManager.Instance.Controller.GamePaused)
//            {
//                GameManager.Instance.Controller.ResumeGame();
//            }
//            else
//            {
//                GameManager.Instance.Controller.PauseGame();
//            }
//        }
//        else
//        {
//            GameManager.Instance.Controller.InterruptCurrentCutscene();
//        }
//    }
//}
