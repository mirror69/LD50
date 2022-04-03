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
    private float timeToFadeImage;
    [SerializeField]
    private float timeToFadeOutImage;
    [SerializeField]
    private float timeIntervalInFade;

    private void Start()
    {
        SetImageActive(false);
    }

    public void Activate ()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => SetImageActive(true))
            .Append(BlackScreenImage.DOFade(1, timeToFadeImage))
            .AppendCallback(() => Debug.Log("Какое то действие в темноте"))
            .AppendInterval(timeIntervalInFade)
            .Append(BlackScreenImage.DOFade(0, timeToFadeOutImage))
            .AppendCallback(() => SetImageActive(false));   
    }

    private void SetImageActive (bool active)
    {
        BlackScreenImage.gameObject.SetActive(active);
    }
}