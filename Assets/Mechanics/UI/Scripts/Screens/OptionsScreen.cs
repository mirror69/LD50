using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Экран настроек игры
/// </summary>
public class OptionsScreen : UIScreen
{
    [SerializeField]
    private SoundOptionsPanel soundOptionsPanel;
    [SerializeField]
    private IntroOptionsPanel introOptionsPanel = null;

    public override void Init(UIEventMediator uiEventMediator)
    {
        base.Init(uiEventMediator);
        soundOptionsPanel.Init(uiEventMediator);
        introOptionsPanel.Init();
    }
}
