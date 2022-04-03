using System;

public class TimeLogic
{
    public event Action IngameSecondPassed;

    public float TimeSpeed { get; private set; }

    private TimeState _timeState;
    private TimeService _timeService;
    private TimeSettings _timeSettings;

    private float _secondsPassed;
    private bool _isTicking;

    public void Init(TimeSettings timeSettings, TimeState timeState, TimeService timeService)
    {
        Stop();

        _timeState = timeState;
        _timeService = timeService;
        _timeSettings = timeSettings;
        TimeSpeed = _timeSettings.NormalTimeSpeed;
    }

    public void Start()
    {
        if (_timeService != null && !_isTicking)
        {
            _timeService.SecondPassed += OnSecondPassed;
            _isTicking = true;
        }
    }

    public void Stop()
    {
        if (_timeService != null && _isTicking)
        {
            _timeService.SecondPassed -= OnSecondPassed;
            _isTicking = false;
        }
    }

    private void OnSecondPassed()
    {
        _secondsPassed += TimeSpeed;
        IngameSecondPassed?.Invoke();
    }
}
