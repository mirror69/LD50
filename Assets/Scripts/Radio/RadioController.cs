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
    [SerializeField] private GameObject _niddleObj;
    [SerializeField] private GameObject buttonSwitcher;
    [SerializeField] private Sprite onButtonSprite;
    [SerializeField] private Sprite offButtonSprite;
    [SerializeField] private Animator plateAnimator;
    [Header("Radio Values Settings")]
    [SerializeField] private float _plateSpeed;
    [Tooltip("Set between 0 and 1")]
    [SerializeField] private float _musicPositionOnSlider;
    [Tooltip("Set between 0 and 1")]
    [SerializeField] private float _areaValue;
    private bool isOn = false;

    private void Start()
    {
        if (_musicPositionOnSlider != Mathf.Clamp(_musicPositionOnSlider, 0f, 1f))
        {
            _musicPositionOnSlider = 0.7f;
        }
        plateAnimator.speed = 0;
        _niddleObj.transform.rotation = Quaternion.Euler(0, 0, -30);
        buttonSwitcher.GetComponent<Image>().sprite = onButtonSprite;
        _musicSource.Play();
        _noizeSource.Play();
    }

    public void OnSwitcherClick()
    {
        if (isOn)
        {
            isOn = false;
            buttonSwitcher.GetComponent<Image>().sprite = onButtonSprite;
            plateAnimator.speed = 0;
        }
        else
        {
            isOn = true;
            buttonSwitcher.GetComponent<Image>().sprite = offButtonSprite;
            plateAnimator.speed = _plateSpeed;
        }

    }


    private void Update()
    {
        float currentNiddleZRotation = (_slider.value * 25f);

        _niddleObj.transform.rotation = Quaternion.Euler(0, 0, -50 + currentNiddleZRotation);

        if (isOn)
        {
            if (_slider.value > _musicPositionOnSlider + _areaValue || _slider.value < _musicPositionOnSlider - _areaValue)
            {
                _audioMixer.SetFloat("MusicVolume", -50f);
                _audioMixer.SetFloat("NoizeVolume", 20f);
            }
            else if (_slider.value > _musicPositionOnSlider + (_areaValue / 2) || _slider.value < _musicPositionOnSlider - (_areaValue / 2))
            {
                _audioMixer.SetFloat("MusicVolume", -40f);
                _audioMixer.SetFloat("NoizeVolume", 15f);
            }
            else if (_slider.value > _musicPositionOnSlider + (_areaValue / 3) || _slider.value < _musicPositionOnSlider - (_areaValue / 3))
            {
                _audioMixer.SetFloat("MusicVolume", -30f);
                _audioMixer.SetFloat("NoizeVolume", -15f);
            }
            else if (_slider.value > _musicPositionOnSlider + (_areaValue / 4) || _slider.value < _musicPositionOnSlider - (_areaValue / 4))
            {
                _audioMixer.SetFloat("MusicVolume", -25);
                _audioMixer.SetFloat("NoizeVolume", -20f);
            }
            else if (_slider.value > _musicPositionOnSlider + (_areaValue / 5) || _slider.value < _musicPositionOnSlider - (_areaValue / 5))
            {
                _audioMixer.SetFloat("MusicVolume", -15);
                _audioMixer.SetFloat("NoizeVolume", -30f);
            }
            else if (_slider.value < _musicPositionOnSlider + (_areaValue / 5) && _slider.value > _musicPositionOnSlider - (_areaValue / 5))
            {
                _audioMixer.SetFloat("MusicVolume", -10f);
                _audioMixer.SetFloat("NoizeVolume", -40f);
            }
        }
        else
        {
            _audioMixer.SetFloat("MusicVolume", -50f);
            _audioMixer.SetFloat("NoizeVolume", -50f);
        }



    }


}
