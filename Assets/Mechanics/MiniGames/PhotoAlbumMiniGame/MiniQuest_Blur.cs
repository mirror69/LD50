using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MiniQuest_Blur : MiniQuest
{
    [SerializeField]
    private GameObject localCanvasWithSlider;
    [SerializeField]
    private Slider leftSlider;
    [SerializeField]
    private SpriteRenderer photoWithBlur;
    [SerializeField]
    private float alphaEnoughtAccuracy = 0.005f;

    [SerializeField]
    private float blurValueIfMiss;

    private bool sliderIsNear;

    private void OnEnable()
    {
        leftSlider.onValueChanged.AddListener(ChangeFocus);
    }

    public void ChangeFocus(float sliderValue)
    {
        Color color = photoWithBlur.color;
        color.a = Mathf.Abs(sliderValue);
        if (color.a > alphaEnoughtAccuracy)
        {
            color.a = blurValueIfMiss;
        }

        photoWithBlur.color = color;
    }

    private void Update()
    {
        if (photoWithBlur.color.a < alphaEnoughtAccuracy && !questIsDone)
        {
            Debug.Log("IsNear");
            sliderIsNear = true;
            StartCoroutine(CheckSliderIsNear());
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("UnInteractable");

                leftSlider.interactable = false;
            }
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
            localCanvasWithSlider.SetActive(false);
            MiniQuestEnded();
        }
    }

    public override void MiniQuestStart()
    {
        base.MiniQuestStart();
    }
}
