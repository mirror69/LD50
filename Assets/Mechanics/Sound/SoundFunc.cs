using UnityEngine;

/// <summary>
/// Общие функции для работы со звуком
/// </summary>
public static class SoundFunc
{
    /// <summary>
    /// Воспроизвести звуковую дорожку
    /// </summary>
    /// <param name="audioSource"></param>
    public static void Play(AudioSource audioSource)
    {
        if (audioSource == null || !audioSource.isActiveAndEnabled)
        {
            return;
        }

        if (audioSource.isPlaying)
        {
            return;
        }

        audioSource.Play();
    }

    /// <summary>
    /// Остановить воспроизведение звуковой дорожки
    /// </summary>
    /// <param name="audioSource"></param>
    public static void Stop(AudioSource audioSource)
    {
        if (audioSource == null)
        {
            return;
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    /// <summary>
    /// Установить время, с которого нужно воспроизводить звук
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="time"></param>
    public static void SetTime(AudioSource audioSource, float time)
    {
        if (audioSource == null || audioSource.clip.length > time)
        {
            return;
        }
        audioSource.time = time;
    }
}
