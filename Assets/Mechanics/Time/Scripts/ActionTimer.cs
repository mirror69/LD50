using System;

public class ActionTimer
{
    private Action _callback;
    private TimeLogic _timeLogic;
    private TimeState _timeState;
    
    public int SecondsLeft { get; private set; }

    public ActionTimer(TimeLogic timeLogic, TimeState timeState, Action callback)
    {
        _timeLogic = timeLogic;
        _timeState = timeState;
        _callback = callback;
    }

    public void StartTimer(int secondsLeft)
    {
        StopTimer();
        SecondsLeft = secondsLeft;
        _timeLogic.IngameSecondPassed += OnIngameSecondPassed;
    }

    public void StopTimer()
    {
        _timeLogic.IngameSecondPassed -= OnIngameSecondPassed;
    }

    private void OnIngameSecondPassed()
    {
        SecondsLeft--;
        if (SecondsLeft <= 0)
        {
            _callback?.Invoke();
            StopTimer();
        }
    }
}
