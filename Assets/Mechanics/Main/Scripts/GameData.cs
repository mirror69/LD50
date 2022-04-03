using UnityEngine;

public class GameData
{
    public int GoodItemCount { get; private set; }
    public int BadItemCount { get; private set; }
    public InteractableItem CurrentInteractingItem { get; private set; }

    public int CurrentTimeOfBadItemUse =>
        (int)Mathf.Ceil(_gameSettings.TimeSettings.MaxTimeOfBadItemUse 
            / Mathf.Pow(_gameSettings.TimeSettings.BadProgressionDivider, BadItemCount));

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
}
