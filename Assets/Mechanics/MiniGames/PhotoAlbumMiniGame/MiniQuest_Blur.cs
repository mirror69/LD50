using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MiniQuest_Blur : MiniQuest
{
    [SerializeField]
    private Volume universalGlobalVolume;
    [SerializeField]
    private GameObject localCanvasWithSlider;
    [SerializeField]
    private Slider leftSlider;

    [SerializeField]
    private float defaultFocalLength;

    [SerializeField]
    private float focalAccuracy;

    private DepthOfField depthOfField;
    private bool sliderIsNear;

    private void Awake()
    {
        universalGlobalVolume.profile.TryGet<DepthOfField>(out depthOfField);
    }

    private void Start()
    {
        depthOfField.mode.value = DepthOfFieldMode.Bokeh;
        depthOfField.mode.overrideState = true;
    }

    private void OnEnable()
    {
        leftSlider.onValueChanged.AddListener(ChangeFocalLength);
    }


    public void ChangeFocalLength (float value)
    {
        if (value < defaultFocalLength)
        {
            depthOfField.focalLength.value = defaultFocalLength + (defaultFocalLength - value);
        }
        else
        {
            depthOfField.focalLength.value = value;
        }
    }

    private void Update()
    {
        float x2 = Mathf.Abs(depthOfField.focalLength.value - defaultFocalLength);

        if (x2 < focalAccuracy && !questIsDone)
        {
            sliderIsNear = true;
            StartCoroutine(CheckSliderIsNear());
        }
        else
        {
            StopAllCoroutines();
            sliderIsNear = false;
        }
    }

    private IEnumerator CheckSliderIsNear ()
    {
        yield return new WaitForSeconds(0.5f);
        if (sliderIsNear)
        {
            depthOfField.mode.overrideState = false;
            localCanvasWithSlider.SetActive(false);
            MiniQuestEnded();
        }
    }

    public override void MiniQuestStart()
    {
        base.MiniQuestStart();
    }
}
