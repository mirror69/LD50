using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalTextTiping : MonoBehaviour
{
    [SerializeField] private TMP_Text textObj;
    [SerializeField] private float typeDelay;
    //private TMP textTMP;
    private string lastWords;

    private void Awake()
    {
        //textTMP = textObj.GetComponent<TMP_Text>();
        lastWords = textObj.text;
        textObj.text = "";
    }

    public void StartTyting()
    {
        //StartCoroutine(TypingCourutine());
    }

    private IEnumerator TypingCourutine()
    {
        foreach (char abc in lastWords)
        {
            textObj.text += abc;
            yield return new WaitForSeconds(typeDelay);
        }
    }
}
