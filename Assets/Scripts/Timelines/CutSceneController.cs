using UnityEngine;

/// <summary>
/// Контроллер катсцен.
/// Управляет всеми катсценами в игре
/// </summary>
public class CutSceneController : MonoBehaviour
{
    [SerializeField, Tooltip("Катсцена, показывающая главное меню")]
    private TimelinePlaybackObject mainMenuCutscene = null;

    [SerializeField, Tooltip("Начальная катсцена игры")]
    private TimelinePlaybackObject startGameCutscene = null;

    [SerializeField, Tooltip("Конечная катсцена игры")]
    private TimelinePlaybackObject endGameCutscene = null;

    public bool IsEnabled => gameObject.activeSelf;

    public TimelinePlaybackObject CurrentCutscene { get; private set; }

    public void InterruptCurrentCutscene()
    {
        if (CurrentCutscene != null)
        {
            CurrentCutscene.RequestInterruption();
        }
    }

    public void PlayMainMenuCutscene()
    {
        CurrentCutscene = mainMenuCutscene;
        CurrentCutscene.Play();
    }

    public void PlayNewGameCutscene()
    {
        CurrentCutscene = startGameCutscene;
        CurrentCutscene.Play();
    }

    public void PlayEndGameCutscene()
    {
        CurrentCutscene = endGameCutscene;
        CurrentCutscene.Play();
    }
}
