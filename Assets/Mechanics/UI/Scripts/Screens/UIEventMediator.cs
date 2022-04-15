using System;

public class UIEventMediator
{
    public event Action StartNewGameRequested;
    public event Action QuitRequested;
    public event Action PauseGameRequested;
    public event Action ResumeGameRequested;
    public event Action SkipCutsceneLineRequested;
    public event Action ReturnToPreviousScreenRequested;
    public event Action<UIScreen> ShowChildScreenRequested;
    public event Action MainMenuRequested;
    public event Action<string> ApplyToAudioMixerRequested;
    public event Action<bool> SetEnabledMinigameMusicRequested;

    internal void RequestStartNewGame()
    {
        StartNewGameRequested?.Invoke();
    }

    internal void RequestShowChildScreen(UIScreen childScreen)
    {
        ShowChildScreenRequested?.Invoke(childScreen);
    }

    internal void RequestQuit()
    {
        QuitRequested?.Invoke();
    }

    internal void RequestPauseGame()
    {
        PauseGameRequested?.Invoke();
    }

    internal void RequestResumeGame()
    {
        ResumeGameRequested?.Invoke();
    }

    internal void RequestMainMenu()
    {
        MainMenuRequested?.Invoke();
    }

    internal void RequestReturnToPreviousScreen()
    {
        ReturnToPreviousScreenRequested?.Invoke();
    }

    internal void RequestApplyToAudioMixer(string paramName)
    {
        ApplyToAudioMixerRequested?.Invoke(paramName);
    }

    internal void RequestSkipCutsceneLine()
    {
        SkipCutsceneLineRequested?.Invoke();
    }

    internal void RequestSetEnabledMinigameMusic(bool enabled)
    {
        SetEnabledMinigameMusicRequested?.Invoke(enabled);
    }
}
