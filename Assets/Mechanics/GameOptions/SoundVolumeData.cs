using UnityEngine;

/// <summary>
/// Класс для хранения и управления значением параметра громкости звука
/// </summary>
public class SoundVolumeData
{
    /// <summary>
    /// Значение громкости, соответствующее выключенному звуку
    /// </summary>
    public const float OffVolume = -80;

    /// <summary>
    /// Минимальное значение громкости
    /// </summary>
    public const float MinVolume = -30;

    /// <summary>
    /// Максимальное значение громкости
    /// </summary>
    public const float MaxVolume = 0;

    /// <summary>
    /// Начальное значение громкости
    /// </summary>
    public const float DefaultVolume = -4;

    /// <summary>
    /// Текущее значение громкости
    /// </summary>
    public float Volume { get; private set; }

    /// <summary>
    /// Значение громкости, приведённое к диапазону [0;1]
    /// </summary>
    public float NormalizedVolume => Mathf.InverseLerp(MinVolume, MaxVolume, Volume);

    public SoundVolumeData()
    {
        SetVolume(DefaultVolume);
    }
    
    /// <summary>
    /// Установить значение громкости
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume)
    {
        Volume = volume;
        if (Volume <= MinVolume)
        {
            Volume = OffVolume;
        }
        else if (Volume > MaxVolume)
        {
            Volume = MaxVolume;
        }
    }

    /// <summary>
    /// Установить значение громкости по нормализованному значению
    /// </summary>
    /// <param name="normalizedVolume"></param>
    public void SetNormalizedVolume(float normalizedVolume)
    {
        float volume = MinVolume + normalizedVolume * (MaxVolume - MinVolume);
        SetVolume(volume);
    }
}
