using System.Collections;
using UnityEngine;

/// <summary>
/// Чёрный экран, плавно появляющийся и исчезающий
/// </summary>
public class BlackoutScreen : UIScreen
{
    [SerializeField, Tooltip("CanvasGroup, у которого будет меняться прозрачность")]
    private CanvasGroup canvasGroup = null;

    [SerializeField, Tooltip("Время плавного появления/исчезновения")]
    private float fadeInOutTime = 1;

    /// <summary>
    /// Корутина плавного появления/исчезновения
    /// </summary>
    private Coroutine fadeInOutCoroutine = null;

    /// <summary>
    /// Показать/скрыть экран
    /// </summary>
    /// <param name="active"></param>
    public override void SetActive(bool active)
    {
        if (active == IsActive)
        {
            return;
        }

        if (fadeInOutTime <= 0)
        {
            gameObject.SetActive(active);
            return;           
        }

        if (!IsActive)
        {
            canvasGroup.alpha = 0;
            gameObject.SetActive(true);
        }
        
        if (fadeInOutCoroutine != null)
        {
            StopCoroutine(fadeInOutCoroutine);
        }
        fadeInOutCoroutine = StartCoroutine(FadeInOut(active));
    }

    /// <summary>
    /// Корутина плавного появления/исчезновения
    /// </summary>
    /// <param name="active"></param>
    /// <returns></returns>
    private IEnumerator FadeInOut(bool active)
    {
        int targetAlpha;
        int direction;
        if (active)
        {
            targetAlpha = 1;
            direction = 1;
        }
        else
        {
            targetAlpha = 0;
            direction = -1;
        }
        float endTime = Time.time + fadeInOutTime * direction * (targetAlpha - canvasGroup.alpha);

        while (Time.time < endTime)
        {
            canvasGroup.alpha += direction * Time.deltaTime / fadeInOutTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        if (!active)
        {
            fadeInOutCoroutine = null;
            gameObject.SetActive(false);
        }
    }
}
