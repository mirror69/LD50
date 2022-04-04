using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;


public class DeathTextsController : MonoBehaviour
{
    [SerializeField] private AnimationClip firstTextAnimation;

    [SerializeField] private GameObject blackScreen;

    [SerializeField] private GameObject activateForText;
    [SerializeField]
    private GameObject ButtonObject;

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

    [Header("Settings")]
    [SerializeField] private float animatorSpeed;
    [SerializeField] private float blackScreenSpeed;
    [SerializeField] private float nextTextDelay;
    [SerializeField] private float nextPageDelay;



    private List<GameObject[]> arrayList = new List<GameObject[]>();
    private List<Transform[]> posArrayList = new List<Transform[]>();

    private void Start()
    {
        activateForText.SetActive(false);
        ButtonObject.SetActive(false);
    }

    public void StartDeathTextMethod()
    {
        activateForText.SetActive(true);
        blackScreen.GetComponent<Animator>().enabled = true;
        blackScreen.GetComponent<Animator>().speed = blackScreenSpeed;

        if (firstTextsArray.Length != 0)
        {
            arrayList.Add(firstTextsArray);
            if (secondTextsArray.Length != 0)
            {
                arrayList.Add(secondTextsArray);
                if (thirdTextsArray.Length != 0)
                {
                    arrayList.Add(thirdTextsArray);
                    if (fourthTextsArray.Length != 0)
                    {
                        arrayList.Add(fourthTextsArray);
                        if (fivthTextsArray.Length != 0)
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
            if (twoTextPosition.Length != 0)
            {
                posArrayList.Add(twoTextPosition);
                if (threeTextPosition.Length != 0)
                {
                    posArrayList.Add(threeTextPosition);
                    if (fourTextPosition.Length != 0)
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

    public void OnClickAgain()
    {

    }

    private IEnumerator NextArrayCorutine()
    {
        for (int i = 0; i < arrayList.Count; i++)
        {
            yield return StartCoroutine(TextAnimationCorutine(arrayList[i]));
        }
    }


    private IEnumerator TextAnimationCorutine(GameObject[] gameObjects)
    {
        if (gameObjects.Length != 0)
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

                yield return new WaitForSeconds(nextTextDelay);

            }
        }

        yield return new WaitForSeconds(nextPageDelay);

    }
}
