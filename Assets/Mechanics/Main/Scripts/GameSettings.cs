using UnityEngine;

[CreateAssetMenu(menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [field: SerializeField]
    public TimeSettings TimeSettings { get; private set; }
}

[System.Serializable]
public struct TimeSettings
{
    public float NormalTimeSpeed;
    public int SecondsToDeath;
    
    [Header("Speed up death settings")]
    public int MaxTimeOfBadItemUse;
    public int TimeAfterShortBadItemUse;
    public int TimeAfterLongBadItemUse;
    public float BadProgressionDivider;

    [Header("Slow down death settings")]
    public int MinTimeAfterGoodItemUse;

    [Header("Win settings")]
    public int SecondsToWin;
    public int GoodItemCountToWin;
    public int WinDelayAfterLastItemUse;
}
