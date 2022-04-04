using System;
using UnityEngine;

/// <summary>
/// Параметры начала игры
/// </summary>
public class IntroOptions
{
    /// <summary>
    /// Флаги параметров начала игры
    /// </summary>
    [Flags]
    public enum IntroOptionsFlags
    {
        None = 0,
        PlayIntro = 1,
        PlayTutorial = 2,
        All = 3
    }

    /// <summary>
    /// Имя параметра для хранения в долгосрочном хранилище
    /// </summary>
    public const string IntroOptionsStoreName = "IntroOptions";

    /// <summary>
    /// Параметры начала игры
    /// </summary>
    private IntroOptionsFlags options = IntroOptionsFlags.All;

    /// <summary>
    /// Загрузить параметры из хранилища
    /// </summary>
    /// <returns></returns>
    public bool LoadFromStorage()
    {
        if (PlayerPrefs.HasKey(IntroOptionsStoreName))
        {
            options = (IntroOptionsFlags)PlayerPrefs.GetInt(IntroOptionsStoreName);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Получить параметр необходимости проигрывания обучения
    /// </summary>
    /// <returns></returns>
    public bool GetPlayTutorialOption()
    {
        return GetOption(IntroOptionsFlags.PlayTutorial);
    }

    /// <summary>
    /// Установить параметр необходимости проигрывания обучения
    /// </summary>
    /// <returns></returns>
    public void SetPlayTutorialOption(bool value)
    {
        SetOption(IntroOptionsFlags.PlayTutorial, value);
    }

    /// <summary>
    /// Получить параметр необходимости проигрывания начальной катсцены
    /// </summary>
    /// <returns></returns>
    public bool GetPlayIntroOption()
    {
        return GetOption(IntroOptionsFlags.PlayIntro);
    }

    /// <summary>
    /// Установить параметр необходимости проигрывания начальной катсцены
    /// </summary>
    /// <param name="value"></param>
    public void SetPlayIntroOption(bool value)
    {
        SetOption(IntroOptionsFlags.PlayIntro, value);        
    }

    /// <summary>
    /// Сохранить параметры в хранилище
    /// </summary>
    private void SaveToStorage()
    {
        PlayerPrefs.SetInt(IntroOptionsStoreName, (int)options);
    }

    /// <summary>
    /// Получить параметр
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    private bool GetOption(IntroOptionsFlags option)
    {
        return (options & option) == option;
    }

    /// <summary>
    /// Установить параметр
    /// </summary>
    /// <param name="option"></param>
    /// <param name="value"></param>
    private void SetOption(IntroOptionsFlags option, bool value)
    {
        if (value)
        {
            options |= option;
        }
        else
        {
            options ^= option;
        }

        SaveToStorage();
    }

}