using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Параметры громкости звука.
/// Отвечает за хранение параметров звуков, а также чтение и запись в
/// долговременное хранилище.
/// Имена параметров совпадают с именами параметров в аудиомикшере.
/// </summary>
public class SoundOptions
{
    /// <summary>
    /// Имя параметра общей громкости
    /// </summary> 
    public const string MasterVolumeParamName = "MasterVolume";

    /// <summary>
    /// Имя параметра громкости звуковых эффектов
    /// </summary>     
    public const string SfxVolumeParamName = "SfxVolume";

    /// <summary>
    /// Имя параметра громкости музыки
    /// </summary>       
    public const string MusicVolumeParamName = "MusicVolume";

    /// <summary>
    /// Имя параметра громкости голоса
    /// </summary>          
    //public const string VoiceVolumeParamName = "VoiceVolume";

    /// <summary>
    /// Имя параметра громкости звукового сопровождения в катсценах
    /// </summary>          
    //public const string CutsceneVolumeParamName = "CutsceneVolume";

    /// <summary>
    /// Словарь, задающий соответствие между именами параметров и объектами, хранящими
    /// значения параметров
    /// </summary>
    private Dictionary<string, SoundVolumeData> soundOptionsDataDictionary 
        = new Dictionary<string, SoundVolumeData>();

    public SoundOptions()
    {
        AddVolumeData(MasterVolumeParamName);
        AddVolumeData(SfxVolumeParamName);
        AddVolumeData(MusicVolumeParamName);
        //AddVolumeData(VoiceVolumeParamName);
        //AddVolumeData(CutsceneVolumeParamName);
    }

    /// <summary>
    /// Загрузить параметры из долговременного хранилища
    /// </summary>
    /// <returns></returns>
    public bool LoadFromStorage()
    {
        bool loaded = false;
        foreach (var item in soundOptionsDataDictionary.Keys)
        {
            if (PlayerPrefs.HasKey(item))
            {
                soundOptionsDataDictionary[item].SetVolume(PlayerPrefs.GetFloat(item));
                loaded = true;
            }
        }
        return loaded;
    }

    /// <summary>
    /// Сохранить параметры в долговременное хранилище
    /// </summary>
    /// <returns></returns>
    public void SaveToStorage()
    {
        foreach (var item in soundOptionsDataDictionary.Keys)
        {
            PlayerPrefs.SetFloat(item, soundOptionsDataDictionary[item].Volume);
        }
    }

    /// <summary>
    /// Получить данные о громкости звука
    /// </summary>
    /// <param name="paramName"></param>
    /// <returns></returns>
    public SoundVolumeData GetVolumeData(string paramName)
    {
        if (soundOptionsDataDictionary.ContainsKey(paramName))
        {
            return soundOptionsDataDictionary[paramName];
        }
        return null;
    }

    /// <summary>
    /// Применить значение параметра к аудиомикшеру
    /// </summary>
    /// <param name="audioMixer"></param>
    /// <param name="paramName"></param>
    public void ApplyToAudioMixer(AudioMixer audioMixer, string paramName)
    {
        SoundVolumeData soundVolumeData = GetVolumeData(paramName);
        if (soundVolumeData == null)
        {
            return;
        }
        audioMixer.SetFloat(paramName, soundVolumeData.Volume);
    }

    /// <summary>
    /// Применить все параметры к аудиомикшеру
    /// </summary>
    /// <param name="audioMixer"></param>
    public void ApplyAllToAudioMixer(AudioMixer audioMixer)
    {
        foreach (var item in soundOptionsDataDictionary.Keys)
        {
            ApplyToAudioMixer(audioMixer, item);
        }
    }

    /// <summary>
    /// Добавить параметр громкости звука
    /// </summary>
    /// <param name="name"></param>
    private void AddVolumeData(string name)
    {
        soundOptionsDataDictionary[name] = new SoundVolumeData();
    }
}
