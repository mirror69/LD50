using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Сохраняемые в долговременном хранилище настройки игры
/// </summary>
public class StoredGameDataManager : Singleton<StoredGameDataManager>
{
    /// <summary>
    /// Параметры начала игры
    /// </summary>
    private IntroOptions introOptions = null;

    /// <summary>
    /// Параметры звуков
    /// </summary>
    private SoundOptions soundOptions = null;

    public static IntroOptions IntroOptions
    {
        get
        {
            if (Instance.introOptions == null)
            {
                Instance.introOptions = new IntroOptions();
                Instance.introOptions.LoadFromStorage();
            }
            return Instance.introOptions;
        }
    }

    public static SoundOptions SoundOptions
    {
        get
        {
            if (Instance.soundOptions == null)
            {
                Instance.soundOptions = new SoundOptions();
                Instance.soundOptions.LoadFromStorage();
            }
            return Instance.soundOptions;
        }
    }
}
