using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Панель настроек начала игры
/// </summary>
public class IntroOptionsPanel : MonoBehaviour
{
    [SerializeField, Tooltip("Нужно ли проигрывать заставку")]
    private Toggle playIntroToggle = null;

    [SerializeField, Tooltip("Нужно ли проигрывать обучение")]
    private Toggle playTutorialToggle = null;

    public void Init()
    {
        playIntroToggle.isOn = StoredGameDataManager.IntroOptions.GetPlayIntroOption();
        playTutorialToggle.isOn = StoredGameDataManager.IntroOptions.GetPlayTutorialOption();

        playIntroToggle.onValueChanged.AddListener(
            (enabled) => StoredGameDataManager.IntroOptions.SetPlayIntroOption(enabled));
        playTutorialToggle.onValueChanged.AddListener(
            (enabled) => StoredGameDataManager.IntroOptions.SetPlayTutorialOption(enabled));
    }
}
