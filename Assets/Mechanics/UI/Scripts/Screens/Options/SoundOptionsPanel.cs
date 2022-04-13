using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Панель настроек звука
/// </summary>
public class SoundOptionsPanel : MonoBehaviour
{
    [SerializeField, Tooltip("Слайдер общей громкости")]
    private Slider masterVolumeSlider = null;

    [SerializeField, Tooltip("Слайдер громкости звуковых эффектов")]
    private Slider sfxVolumeSlider = null;

    [SerializeField, Tooltip("Слайдер громкости музыки")]
    private Slider musicVolumeSlider = null;

    [SerializeField, Tooltip("Слайдер громкости голоса")]
    private Slider voiceVolumeSlider = null;

    [SerializeField, Tooltip("Слайдер громкости звукового сопровождения в катсценах")]
    private Slider cutsceneVolumeSlider = null;

    /// <summary>
    /// Словарь пар "название параметра громкости" - "слайдер"
    /// </summary>
    private readonly Dictionary<string, Slider> slidersParamNamesDictionary 
        = new Dictionary<string, Slider>();

    private UIEventMediator _uiEventMediator;

    private void OnEnable()
    {
        SetSlidersByOptions();
    }

    private void OnDisable()
    {
        SaveToStorage();
    }

    public void Init(UIEventMediator uiEventMediator)
    {
        _uiEventMediator = uiEventMediator;

        //RegisterVolumeSlider(masterVolumeSlider, SoundOptions.MasterVolumeParamName);
        RegisterVolumeSlider(sfxVolumeSlider, SoundOptions.SfxVolumeParamName);
        RegisterVolumeSlider(musicVolumeSlider, SoundOptions.MusicVolumeParamName);
        //RegisterVolumeSlider(voiceVolumeSlider, SoundOptions.VoiceVolumeParamName);
        //RegisterVolumeSlider(cutsceneVolumeSlider, SoundOptions.CutsceneVolumeParamName);
    }

    private void SaveToStorage()
    {
        StoredGameDataManager.SoundOptions.SaveToStorage();
    }

    private void SetSlidersByOptions()
    {
        SoundOptions soundOptions = StoredGameDataManager.SoundOptions;
        foreach (var item in slidersParamNamesDictionary)
        {
            SoundVolumeData soundVolumeData = soundOptions.GetVolumeData(item.Key);
            if (soundVolumeData != null)
            {
                item.Value.value = soundVolumeData.NormalizedVolume;
            }          
        }
    }

    private void RegisterVolumeSlider(Slider slider, string paramName)
    {
        slidersParamNamesDictionary.Add(paramName, slider);
        slider.onValueChanged.AddListener(
            (value) => OnSliderValueChanged(paramName, value));
    }

    private void OnSliderValueChanged(string paramName, float value)
    {
        SoundVolumeData soundVolumeData = StoredGameDataManager.SoundOptions.GetVolumeData(paramName);
        soundVolumeData.SetNormalizedVolume(value);
        _uiEventMediator.RequestApplyToAudioMixer(paramName);
    }
}
