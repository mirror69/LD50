using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class BlackScreen : MonoBehaviour
{
    [SerializeField]
    private Image BlackScreenImage;

    [SerializeField]
    private float startTimeToFadeImage;
    [SerializeField]
    private float startTimeToFadeOutImage;
    [SerializeField]
    private float timeIntervalInFade;
    [SerializeField]
    private float timeAddAfterUseItem;

    private void Start()
    {
        SetImageActive(false);
    }

    public void Activate(TweenCallback callback)
    { 
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => SetImageActive(true))
            .Append(BlackScreenImage.DOFade(1, startTimeToFadeImage))
            .AppendCallback(callback)
            .AppendInterval(timeIntervalInFade)
            .Append(BlackScreenImage.DOFade(0, startTimeToFadeOutImage))
            .AppendCallback(() => SetImageActive(false));

        startTimeToFadeImage += timeAddAfterUseItem;
        startTimeToFadeOutImage += timeAddAfterUseItem;
    }

    private void SetImageActive (bool active)
    {
        BlackScreenImage.gameObject.SetActive(active);
    }
}