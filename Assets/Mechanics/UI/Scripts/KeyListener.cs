using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeyListener
{
    public abstract void CatchKey();
}

public class MenuKeyListener : KeyListener
{
    private UIEventMediator _uiEventMediator;
    private UIScreenController _uiScreenController;

    public void Init(UIEventMediator uiEventMediator, UIScreenController uiScreenController)
    {
        _uiEventMediator = uiEventMediator;
        _uiScreenController = uiScreenController;
    }

    public override void CatchKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIScreen currentScreen = _uiScreenController.CurrentScreen;
            if (currentScreen != null && currentScreen.ParentScreen != null)
            {
                _uiEventMediator.RequestReturnToPreviousScreen();
            }
            else
            {
                _uiEventMediator.RequestResumeGame();
            }
        }
    }
}

public class GameplayKeyListener : KeyListener
{
    private UIEventMediator _uiEventMediator;

    public void Init(UIEventMediator uiEventMediator)
    {
        _uiEventMediator = uiEventMediator;
    }

    public override void CatchKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uiEventMediator.RequestPauseGame();
        }
    }
}

public class CutsceneKeyListener : KeyListener
{
    private UIEventMediator _uiEventMediator;

    public void Init(UIEventMediator uiEventMediator)
    {
        _uiEventMediator = uiEventMediator;
    }

    public override void CatchKey()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _uiEventMediator.RequestSkipCutsceneLine();
        }
    }
}
