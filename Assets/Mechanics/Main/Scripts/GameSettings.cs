using UnityEngine;

[CreateAssetMenu(menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [field: SerializeField]
    public TimeSettings TimeSettings { get; private set; }

    [field: SerializeField]
    public SceneSettings SceneSettings { get; private set; }

    [field: SerializeField]
    public CameraSettings CameraSettings { get; private set; }

    [field: SerializeField]
    public SoundSettings SoundSettings { get; private set; }

    [field: SerializeField]
    public UISettings UISettings { get; private set; }
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

[System.Serializable]
public struct SceneSettings
{
    public string MainMenuSceneName;
    public string GameSceneName;
}

[System.Serializable]
public struct CameraSettings
{
    public float BadItemZoomInTime;
    public float BadItemZoomOutTime;
    public float GoodEndingCameraZoomTime;
    public float GoodEndingCameraMoveSpeed;
}

[System.Serializable]
public struct SoundSettings
{
    public float MainMusicFadeOutTimeAfterWin;
    public float LoseMusicDelayAfterBlacking;
}

[System.Serializable]
public struct UISettings
{
    public float ButtonsFadeDuration;
    public float CreditsMoveSpeed;
    public float GameOverShowDelay;
    public float CreditsButtonsShowDelay;
    public float GameOverButtonsShowDelay;
}
