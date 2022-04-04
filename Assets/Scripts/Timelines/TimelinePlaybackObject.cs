using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Объект катсцены, проигрывающий таймлинию.
/// </summary>
public class TimelinePlaybackObject : MonoBehaviour
{
    [SerializeField, Tooltip("Объект таймлинии")]
    private PlayableDirector timeline;

    //[SerializeField, Tooltip("Отображается ли курсор во время проигрывания")]
    //private bool cursorEnabled = false;

    //[SerializeField, Tooltip("Включено ли управление во время проигрывания")]
    //private bool playerInputEnabled = false;

    //[SerializeField, Tooltip("Включить управление после проигрывания")]
    //private bool enablePlayerInputAfterPlayback = true;

    [SerializeField, Tooltip("Может ли катсцена быть прервана")]
    private bool canBeInterrupted = true;

    [SerializeField, Tooltip("Если проигрывание было прервано, будет осуществлен переход в конец таймлинии," +
        "за вычетом данного значения в секундах")]
    private float timeShiftFromTheEndWhenInterrupted = 1;

    /// <summary>
    /// Объект корутины воспроизведения
    /// </summary>
    private Coroutine playbackCoroutine = null;

    /// <summary>
    /// Объект корутины остановки воспроизведения
    /// </summary>
    private Coroutine stoppingCoroutine = null;

    public void RegisterTimeLineStoppedListener(Action<PlayableDirector> listener)
    {
        timeline.stopped += listener;
    }
    public void UnregisterTimeLineStoppedListener(Action<PlayableDirector> listener)
    {
        timeline.stopped -= listener;
    }

    public void Play()
    {
        if (playbackCoroutine != null)
        {
            return;
        }
        
        playbackCoroutine = StartCoroutine(PerformPlaying());

        //GameManager.Instance.Controller.SetCursorEnabled(cursorEnabled);
        //GameManager.Instance.Controller.SetGamePlayEnabled(playerInputEnabled);     
    }

    /// <summary>
    /// Запросить остановку воспроизведения
    /// </summary>
    public void RequestInterruption()
    {
        if (!canBeInterrupted || stoppingCoroutine != null || timeline.state != PlayState.Playing)
        {
            return;
        }
        double newTime = timeline.duration - timeShiftFromTheEndWhenInterrupted;
        if (newTime <= timeline.time)
        {
            return;
        }
        timeline.time = newTime;
        stoppingCoroutine = StartCoroutine(PerformStopping());
    }

    /// <summary>
    /// Остановить воспроизведение
    /// </summary>
    public void Stop()
    {
        if (playbackCoroutine != null)
        {
            StopCoroutine(playbackCoroutine);
            playbackCoroutine = null;
        }
        timeline.Stop();

        //GameManager.Instance.Controller.SetCursorEnabled(true);
        //GameManager.Instance.Controller.SetGamePlayEnabled(enablePlayerInputAfterPlayback);
    }

    /// <summary>
    /// Корутина, выполняющая воспроизведение
    /// </summary>
    /// <returns></returns>
    private IEnumerator PerformPlaying()
    {
        timeline.time = timeline.initialTime;
        timeline.Play();
        yield return new WaitForSeconds((float)timeline.duration);
        Stop();
        playbackCoroutine = null;
    }

    /// <summary>
    /// Корутина, выполняющая прерывание воспроизведения
    /// </summary>
    /// <returns></returns>
    private IEnumerator PerformStopping()
    {
        yield return new WaitForSeconds(timeShiftFromTheEndWhenInterrupted);
        Stop();
        stoppingCoroutine = null;
    }
}
