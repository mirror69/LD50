using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// Контроллер таймлинии обучения
/// </summary>
public class TutorialController : MonoBehaviour
{
    [SerializeField, Tooltip("Таймлиния обучения")]
    private PlayableDirector tutorialTimeLine;

    /// <summary>
    /// Включен ли контроллер обучения
    /// </summary>
    public bool IsEnabled => gameObject.activeSelf;

    public PlayableDirector TimeLine => tutorialTimeLine;

    /// <summary>
    /// Включить/выключить контроллер обучения
    /// </summary>
    /// <param name="enabled"></param>
    public void SetEnabled(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    /// <summary>
    /// Воспроизвести таймлинию обучения
    /// </summary>
    public void Play()
    {
        tutorialTimeLine.Play();
        tutorialTimeLine.stopped += (t) => OnTutorialFinished();
    }

    /// <summary>
    /// Обработать событие окончания обучения
    /// </summary>
    private void OnTutorialFinished()
    {
        SetEnabled(false);
    }
}
