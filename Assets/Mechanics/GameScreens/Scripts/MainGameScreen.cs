using System;
using UnityEngine;

public class MainGameScreen : GameScreen
{
    [SerializeField]
    private ClickHandler _clickHandler;

    public Action<DestinationPoint> DestinationPointClicked;

    public void SetEnabledControl(bool enabled)
    {
        _clickHandler.enabled = enabled;
    }

    private void OnEnable()
    {
        _clickHandler.DestinationPointClicked += OnDestinationPointClickedd;
    }

    private void OnDisable()
    {
        _clickHandler.DestinationPointClicked -= OnDestinationPointClickedd;
    }

    private void OnDestinationPointClickedd(DestinationPoint destinationPoint)
    {
        DestinationPointClicked?.Invoke(destinationPoint);
    }
}
