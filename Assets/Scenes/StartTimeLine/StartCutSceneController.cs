using SimpleMan.CoroutineExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartCutSceneController : MonoBehaviour
{
    [SerializeField]
    private TextInCutScene textInCutScene;

    [SerializeField]
    private Image fadeScreenImage;

    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private float timeToFadeOut;

    private bool isFadingOut;

    private void OnEnable()
    {
        text = textInCutScene.gameObject.GetComponent<TMP_Text>();
        textInCutScene.OnStartCutSceneEnded += TextInCutScene_OnStartCutSceneEnded;
    }

    private void OnDisable()
    {
        textInCutScene.OnStartCutSceneEnded -= TextInCutScene_OnStartCutSceneEnded;
    }

    private void Update()
    {
        if (isFadingOut)
        {
            Color color = fadeScreenImage.color;
            color.a = Mathf.Lerp(color.a, 0, timeToFadeOut*Time.deltaTime);
            fadeScreenImage.color = color;
        }
    }

    private void TextInCutScene_OnStartCutSceneEnded()
    {
        isFadingOut = true;
        this.Delay(timeToFadeOut, () => this.gameObject.SetActive(false));
    }
}
