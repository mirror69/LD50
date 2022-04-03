using UnityEngine;

public class GameData
{
    public int GoodObjectsCount { get; private set; }
    public int BadObjectsCount { get; private set; }
    public InteractableItem CurrentInteractingItem { get; private set; }

    public int CurrentTimeOfBadItemUse =>
        (int)Mathf.Ceil(_gameSettings.TimeSettings.MaxTimeOfBadItemUse 
            / Mathf.Pow(_gameSettings.TimeSettings.BadProgressionDivider, BadObjectsCount));

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

    public void AddGoodObjectInteraction()
    {
        GoodObjectsCount++;
    }

    public void AddBadObjectInteraction()
    {
        BadObjectsCount++;

    }
}
