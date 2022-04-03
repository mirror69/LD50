using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class DeathTextsController : MonoBehaviour
{
    [SerializeField] private AnimationClip firstTextAnimation;

    [SerializeField] private GameObject blackScreen;

    //[SerializeField]
    //private float startTimeToFadeImage;
    //[SerializeField]
    //private float startTimeToFadeOutImage;
    //[SerializeField]
    //private float timeIntervalInFade;
    //[SerializeField]
    //private float timeAddAfterUseItem;

    [Header("Texts Arrays")]
    [SerializeField] private GameObject[] firstTextsArray;
    [SerializeField] private GameObject[] secondTextsArray;
    [SerializeField] private GameObject[] thirdTextsArray;
    [SerializeField] private GameObject[] fourthTextsArray;
    [SerializeField] private GameObject[] fivthTextsArray;

    [Header("Text POS")]
    [SerializeField] private Transform[] singteTextPosition;
    [SerializeField] private Transform[] twoTextPosition;
    [SerializeField] private Transform[] threeTextPosition;
    [SerializeField] private Transform[] fourTextPosition;

    [Header("Texts Settings")]
    [SerializeField] private float animatorSpeed;
    //[SerializeField] private float appeareAnimLenght;
    //[SerializeField] private float stayAnimLenght;
    //[SerializeField] private float disappeareAnimLenght;


    private List<GameObject[]> arrayList = new List<GameObject[]>();
    private List<Transform[]> posArrayList = new List<Transform[]>();

    private void Start()
    {
        StartMethod();
        blackScreen.GetComponent<Animator>().enabled = true;
    }

    private void StartMethod()
    {
        if (firstTextsArray != null)
        {
            arrayList.Add(firstTextsArray);
            if (secondTextsArray != null)
            {
                arrayList.Add(secondTextsArray);
                if (thirdTextsArray != null)
                {
                    arrayList.Add(thirdTextsArray);
                    if (fourthTextsArray != null)
                    {
                        arrayList.Add(fourthTextsArray);
                        if (fivthTextsArray != null)
                        {
                            arrayList.Add(fivthTextsArray);
                        }
                    }
                }
            }
        }

        if (singteTextPosition != null)
        {
            posArrayList.Add(singteTextPosition);
            if (twoTextPosition != null)
            {
                posArrayList.Add(twoTextPosition);
                if (threeTextPosition != null)
                {
                    posArrayList.Add(threeTextPosition);
                    if (fourTextPosition != null)
                    {
                        posArrayList.Add(fourTextPosition);
                    }
                }
            }
        }

        for (int i = 0; i < arrayList.Count; i++)
        {
            for (int j = 0; j < arrayList[i].Length; j++)
            {
                arrayList[i][j].GetComponent<Animator>().enabled = false;
            }
        }
        StartCoroutine(NextArrayCorutine());

    }

    //private void ActivateBlackScreen() 
    //{
    //    Sequence sequence = DOTween.Sequence();

    //    sequence.AppendCallback(() => SetImageActive(true))
    //        .Append(BlackScreenImage.DOFade(1, startTimeToFadeImage))
    //        .AppendCallback(callback)
    //        .AppendInterval(timeIntervalInFade)
    //        .Append(BlackScreenImage.DOFade(0, startTimeToFadeOutImage))
    //        .AppendCallback(() => SetImageActive(false));

    //    startTimeToFadeImage += timeAddAfterUseItem;
    //    startTimeToFadeOutImage += timeAddAfterUseItem;
    //}

    //private void SetImageActive(bool active)
    //{
    //    BlackScreenImage.gameObject.SetActive(active);
    //}


    private IEnumerator NextArrayCorutine()
    {
        for (int i = 0; i < arrayList.Count; i++)
        {
            yield return StartCoroutine(TextAnimationCorutine(arrayList[i]));
        }
    }


    private IEnumerator TextAnimationCorutine(GameObject[] gameObjects)
    {

        Transform[] posArray = posArrayList[gameObjects.Length -1];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].transform.position = posArray[i].position;
        }

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<Animator>().enabled = true;
            gameObjects[i].GetComponent<Animator>().speed = animatorSpeed;

            yield return new WaitForSeconds(firstTextAnimation.length);
        }
        yield return new WaitForSeconds(5f);
    }
}
