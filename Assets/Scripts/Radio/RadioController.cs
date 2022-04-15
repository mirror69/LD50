using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class RadioController : MonoBehaviour
{
    public const string MusicVolumeParamName = "RadioMusicVolume";
    public const string NoiseVolumeParamName = "RadioNoiseVolume";

    public event Action RadioIsFounded;
    public event Action<bool> SetEnabledMinigameMusicRequested;

    [SerializeField] private float TimeToWinWhenListenWalz;

    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _noizeSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private GameObject _niddleObj;
    [SerializeField] private ColliderBasedButton buttonSwitcher;
    [SerializeField] private Sprite onButtonSprite;
    [SerializeField] private Sprite offButtonSprite;
    [SerializeField] private Animator plateAnimator;

    [Header("Radio Values Settings")]
    [SerializeField] private float _plateSpeed;
    [Tooltip("Set between 0 and 1, default 0.7")]
    [SerializeField] private float _musicPositionOnSlider;
    [Tooltip("Set between 0 and 1, default 0.05")]
    [SerializeField] private float _valueSpace;
    [Space]
    [Tooltip("Set between 0 and 1")]
    [SerializeField] private float _firstAreaValue;
    [Tooltip("Set between 0 and 1")]
    [SerializeField] private float _secondAreaValue;
    [Tooltip("Set between 0 and 1")]
    [SerializeField] private float _thirdAreaValue;
    [Tooltip("Set between 0 and 1")]
    [SerializeField] private float _fourthAreaValue;


    private bool isOn = false;
    private bool needleIsOnPlace;
    private float currentTime = 0;
    private bool questIsReady;

    float musicVolume;
    float noiseVolume;

    private SpriteRenderer switcherSpriteRenderer;

    private void Start()
    {
        if (_musicPositionOnSlider != Mathf.Clamp(_musicPositionOnSlider, 0f, 1f))
        {
            _musicPositionOnSlider = 0.7f;
        }

        if (_valueSpace != Mathf.Clamp(_valueSpace, 0.01f, 0.5f))
        {
            _valueSpace = 0.05f;
        }
        plateAnimator.speed = 0;
        _niddleObj.transform.rotation = Quaternion.Euler(0, 0, -30);

        switcherSpriteRenderer = buttonSwitcher.GetComponent<SpriteRenderer>();
        switcherSpriteRenderer.sprite = onButtonSprite;
    }

    private void OnEnable()
    {
        buttonSwitcher.Pressed += OnSwitcherClick;
    }

    private void OnDisable()
    {
        buttonSwitcher.Pressed -= OnSwitcherClick;
    }

    public void OnSwitcherClick()
    {
        if (isOn && !needleIsOnPlace)
        {
            SetEnabledMinigameMusicRequested?.Invoke(true);
            isOn = false;
            switcherSpriteRenderer.sprite = onButtonSprite;
            plateAnimator.speed = 0;
        }
        else if (!isOn && !needleIsOnPlace)
        {
            SetEnabledMinigameMusicRequested?.Invoke(false);
            isOn = true;
            switcherSpriteRenderer.sprite = offButtonSprite;
            plateAnimator.speed = _plateSpeed;
            _musicSource.Play();
            _noizeSource.Play();
        }

    }


    private void Update()
    {
        if (questIsReady)
        {
            musicVolume -= 2*Time.deltaTime;
            noiseVolume -= 2*Time.deltaTime;

            _audioMixer.SetFloat(MusicVolumeParamName, musicVolume);
            _audioMixer.SetFloat(NoiseVolumeParamName, noiseVolume);

            return;
        }
            
         
        float currentNiddleZRotation = (_slider.value * 25f);

        _niddleObj.transform.rotation = Quaternion.Euler(0, 0, -50 + currentNiddleZRotation);

        needleIsOnPlace = false;

        if (isOn)
        {
            if (_slider.value == Math.Clamp(_slider.value, _firstAreaValue - _valueSpace, _firstAreaValue + _valueSpace))
            {
                needleIsOnPlace = false;
                _audioMixer.SetFloat(MusicVolumeParamName, -15);
                _audioMixer.SetFloat(NoiseVolumeParamName, 20f);
            }
            else if (_slider.value == Math.Clamp(_slider.value, _secondAreaValue - _valueSpace, _secondAreaValue + _valueSpace))
            {
                needleIsOnPlace = false;
                _audioMixer.SetFloat(MusicVolumeParamName, -15);
                _audioMixer.SetFloat(NoiseVolumeParamName, 20f);
            }
            else if (_slider.value == Math.Clamp(_slider.value, _thirdAreaValue - _valueSpace, _thirdAreaValue + _valueSpace))
            {
                needleIsOnPlace = false;
                _audioMixer.SetFloat(MusicVolumeParamName, -15);
                _audioMixer.SetFloat(NoiseVolumeParamName, 20f);
            }
            else if (_slider.value == Math.Clamp(_slider.value, _fourthAreaValue - _valueSpace, _fourthAreaValue + _valueSpace))
            {
                needleIsOnPlace = false;
                _audioMixer.SetFloat(MusicVolumeParamName, -15);
                _audioMixer.SetFloat(NoiseVolumeParamName, 20f);
            }
            else if (_slider.value == Math.Clamp(_slider.value, _musicPositionOnSlider - _valueSpace, _musicPositionOnSlider + _valueSpace))
            {
                needleIsOnPlace = true;
                
                _audioMixer.SetFloat(MusicVolumeParamName, -10f);
                _audioMixer.SetFloat(NoiseVolumeParamName, -40f);

                if (Input.GetMouseButtonUp(0))
                {
                    _slider.interactable = false;
                }
            }
            else
            {
                _audioMixer.SetFloat(MusicVolumeParamName, -50f);
                _audioMixer.SetFloat(NoiseVolumeParamName, 20f);
            }
        }
        else
        {
            _audioMixer.SetFloat(MusicVolumeParamName, -50f);
            _audioMixer.SetFloat(NoiseVolumeParamName, -50f);
        }

        if (!needleIsOnPlace)
            currentTime = 0;

        currentTime += Time.deltaTime;

        if (currentTime >= TimeToWinWhenListenWalz)
        {
            questIsReady = true;
            _audioMixer.GetFloat(MusicVolumeParamName, out musicVolume);
            _audioMixer.GetFloat(NoiseVolumeParamName, out noiseVolume);
            RadioIsFounded?.Invoke();
        }
    }

}
