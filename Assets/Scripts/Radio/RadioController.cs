using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class RadioController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _noizeSource;
    [SerializeField] private AudioSource _musicSource;
    [Header("Radio Values Settings")]
    [Tooltip("Set between 0 and 1")]
    [SerializeField] private float _musicPositionOnSlider;
    [Tooltip("Set between 0 and 1")]
    [SerializeField] private float _areaValue;

    private void Start()
    {
        StartMusic();
        if (_musicPositionOnSlider != Mathf.Clamp(_musicPositionOnSlider,0f,1f))
        {
            _musicPositionOnSlider = 0.7f;
        }
    }

    public void StartMusic()
    {
        _musicSource.Play();
        _noizeSource.Play();
        _audioMixer.SetFloat("MusicVolume", -50f);
        _audioMixer.SetFloat("NoizeVolume", 0f);
    }

    private void Update()
    {
        if (_slider.value > _musicPositionOnSlider + _areaValue || _slider.value < _musicPositionOnSlider - _areaValue)
        {
            _audioMixer.SetFloat("MusicVolume", -50f);
            _audioMixer.SetFloat("NoizeVolume", 0f);
        }
        else if (_slider.value > _musicPositionOnSlider + (_areaValue/2 )|| _slider.value < _musicPositionOnSlider - (_areaValue / 2))
        {
            _audioMixer.SetFloat("MusicVolume", -40f);
            _audioMixer.SetFloat("NoizeVolume", -10f);
        }
        else if (_slider.value > _musicPositionOnSlider + (_areaValue / 3) || _slider.value < _musicPositionOnSlider - (_areaValue / 3))
        {
            _audioMixer.SetFloat("MusicVolume", -30f);
            _audioMixer.SetFloat("NoizeVolume", -10f);
        }
        else if (_slider.value > _musicPositionOnSlider + (_areaValue / 4) || _slider.value < _musicPositionOnSlider - (_areaValue / 4))
        {
            _audioMixer.SetFloat("MusicVolume", -20f);
            _audioMixer.SetFloat("NoizeVolume", -15f);
        }
        else if (_slider.value > _musicPositionOnSlider + (_areaValue / 5) || _slider.value < _musicPositionOnSlider - (_areaValue / 5))
        {
            _audioMixer.SetFloat("MusicVolume", -10f);
            _audioMixer.SetFloat("NoizeVolume", -20f);
        }
        else if (_slider.value < _musicPositionOnSlider + (_areaValue / 5) && _slider.value > _musicPositionOnSlider - (_areaValue / 5))
        {
            _audioMixer.SetFloat("MusicVolume", 0f);
            _audioMixer.SetFloat("NoizeVolume", -40f);
        }
    }


}
