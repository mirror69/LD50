using System;
using UnityEngine;

public class MainGameScreen : GameScreen
{
    [SerializeField]
    private ClickHandler _clickHandler;

    public Action<DestinationPoint> DestinationPointClicked;

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
