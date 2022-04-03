using System;
using System.Collections;
using UnityEngine;

public class TimeService : MonoBehaviour
{
    private WaitForSeconds _waitForSecondLeft;

    public event Action SecondPassed;

    private void Awake()
    {
        _waitForSecondLeft = new WaitForSeconds(1);
        StartCoroutine(TimeUpdating());
    }

    private IEnumerator TimeUpdating()
    {
        while (true)
        {
            yield return _waitForSecondLeft;
            SecondPassed?.Invoke();
        }
    }
}
