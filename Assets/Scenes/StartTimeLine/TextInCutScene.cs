using System.Collections;
using UnityEngine;
using TMPro;
using System;
using SimpleMan.CoroutineExtensions;
using UnityEngine.Localization;

[System.Serializable]
public class Dialogs
{
    public LocalizedString person;
    public LocalizedString phrase;
}

public class TextInCutScene : MonoBehaviour
{
    public event Action OnStartCutSceneEnded;

    [SerializeField]
    private TMP_Text textInCanvas;

    [SerializeField]
    private Dialogs[] dialogs;

    [SerializeField]
    private float timeBetweenLetters;
    [SerializeField]
    private float timeBetweenPhrases;

    private string currentText;
    private int currentPhraseIndex = 0;
    private int currentLetterIndex = 0;

    private bool sceneIsEnded;

    private void Start()
    {
    }

    private void OnEnable()
    {
        currentText = dialogs[currentPhraseIndex].phrase.GetLocalizedString();
        ShowCurrentPhrase("");
        StartCoroutine(LetterShower());

    }

    private void ShowPartOfText(string text)
    {
        textInCanvas.text = text;
    }

    private void ShowCurrentPhrase(string text)
    {
        textInCanvas.text = $"{dialogs[currentPhraseIndex].person.GetLocalizedString()}: {text}";
    }

    private void Update()
    {
        if (sceneIsEnded)
            return;

        if (Input.GetMouseButtonDown(0) || currentLetterIndex >= currentText.Length)
        {
            StopAllCoroutines();

            StartCoroutine(WaitCoroutine());
            ShowCurrentPhrase(currentText);
            currentPhraseIndex++;
            currentLetterIndex = 0;
            if (currentPhraseIndex >= dialogs.Length)
            {
                currentText = "";
                sceneIsEnded = true;
                StopAllCoroutines();
                this.Delay(2f, () => OnStartCutSceneEnded?.Invoke());
                this.Delay(3f, () => this.gameObject.SetActive(false));
                return;
            }
            currentText = dialogs[currentPhraseIndex].phrase.GetLocalizedString();
        }
    }

    private IEnumerator LetterShower()
    {
        yield return new WaitForSeconds(timeBetweenLetters);
        if (currentLetterIndex >= currentText.Length)
        {
            yield break;
        }
        string resText = textInCanvas.text + currentText[currentLetterIndex];
        //Debug.Log(resText);

        ShowPartOfText(resText);
        currentLetterIndex++;

        if (currentLetterIndex >= currentText.Length)
        {
            StartCoroutine(WaitCoroutine());
            yield return null;
        }

        StartCoroutine(LetterShower());
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(timeBetweenPhrases);

        ShowCurrentPhrase("");
        StartCoroutine(LetterShower());
    }
}
