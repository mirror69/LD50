using System;
using UnityEngine;

public class GameData
{
    public int GoodItemCount { get; private set; }
    public int BadItemCount { get; private set; }
    public InteractableItem CurrentInteractingItem { get; private set; }

    public int CurrentTimeOfBadItemUse 
        => GetProgressionValueForBadCount(_gameSettings.TimeSettings.MaxTimeOfBadItemUse, BadItemCount);

    public int CurrentTimeAfterShortBadItemUse
        => GetProgressionValueForBadCount(_gameSettings.TimeSettings.TimeAfterShortBadItemUse, BadItemCount);

    public int CurrentTimeAfterLongBadItemUse
        => GetProgressionValueForBadCount(_gameSettings.TimeSettings.TimeAfterLongBadItemUse, BadItemCount);

    private GameSettings _gameSettings;
    
    public void Init(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }

    public void SetCurrentInteraction(InteractableItem item)
    {
        CurrentInteractingItem = item;
    }

    public void ResetCurrentInteraction()
    {
        CurrentInteractingItem = null;
    }

    public void AddGoodItemInteraction()
    {
        GoodItemCount++;
    }

    public void AddBadItemInteraction()
    {
        BadItemCount++;
    }

    private int GetProgressionValueForBadCount(float value, int count)
    {
        if (count < 0)
        {
            count = 0;
        }
        return (int)Mathf.Ceil(value / Mathf.Pow(_gameSettings.TimeSettings.BadProgressionDivider, count));
    }
}
