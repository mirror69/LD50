using System;
using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private TimeService TimeService;

    private GameSettings _gameSettings;
    private GameData _gameData;

    private TimeState _timeState;
    private TimeLogic _timeLogic;

    private ActionTimer _deathTimer;
    private ActionTimer _winTimer;

    private DebugView _debugView;

    public event Action DeathTimeOver;
    public event Action WinTimeReached;

    public bool IsWinTimeReached() => _deathTimer.SecondsLeft > 0 && _winTimer.SecondsLeft <= 0;

    public void Init(GameSettings gameSettings, GameData gameData, DebugView debugView)
    {
        _gameSettings = gameSettings;
        _gameData = gameData;
        _debugView = debugView;

        _timeState = new TimeState();
        _timeLogic = new TimeLogic();

        _deathTimer = new ActionTimer(_timeLogic, _timeState, DeathTimeOver);
        _winTimer = new ActionTimer(_timeLogic, _timeState, WinTimeReached);

        _timeLogic.Init(gameSettings.TimeSettings, _timeState, TimeService);
        TimeService.SecondPassed += RefreshDebugView;
    }

    public void StartTime()
    {
        _timeLogic.Start();
        _deathTimer.StartTimer(_gameSettings.TimeSettings.SecondsToDeath);
        _winTimer.StartTimer(_gameSettings.TimeSettings.SecondsToWin);
        RefreshDebugView();
    }

    public void StopTime()
    {
        _timeLogic.Stop();
        _deathTimer.StopTimer();
        _winTimer.StopTimer();
    }

    public void PauseTime()
    {
        _timeLogic.Stop();
    }

    public void ResumeTime()
    {
        _timeLogic.Start();
        RefreshDebugView();
    }

    public void StartGoodInteraction()
    {
        PauseTime();
    }

    public void StartBadInteraction()
    {
        _deathTimer.StartTimer(_gameData.CurrentTimeOfBadItemUse);
        RefreshDebugView();
    }

    public void StopGoodInteraction()
    {
        int newTime = _gameSettings.TimeSettings.MinTimeAfterGoodItemUse;
        if (_deathTimer.SecondsLeft >= _gameSettings.TimeSettings.MinTimeAfterGoodItemUse)
        {
            newTime += _deathTimer.SecondsLeft;
        }
        StartTimer(newTime);
    }

    public void StopBadInteraction()
    {
        int newTime;
        if (_deathTimer.SecondsLeft >= _gameData.CurrentTimeOfBadItemUse / 2)
        {
            newTime = _gameData.CurrentTimeAfterShortBadItemUse;
        }
        else
        {
            newTime = _gameData.CurrentTimeAfterLongBadItemUse;
        }

        StartTimer(newTime);
    }

    private void StartTimer(int timeLeft)
    {
        Debug.Log($"Time left: {timeLeft}");

        _timeLogic.Start();
        _deathTimer.StartTimer(timeLeft);
        RefreshDebugView();
    }

    private void RefreshDebugView()
    {
        StartCoroutine(RefreshingDebugView());
    }

    private IEnumerator RefreshingDebugView()
    {
        yield return null;
        _debugView.SetTime(_timeState, _deathTimer.SecondsLeft);
    }
}
