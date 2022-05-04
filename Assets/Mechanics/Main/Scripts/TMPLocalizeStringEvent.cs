using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

[RequireComponent(typeof(TMP_Text))]
public class TMPLocalizeStringEvent: LocalizeStringEvent
{
    private TMP_Text _tmpText;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    protected override void UpdateString(string value)
    {
        base.UpdateString(value);
        if (Application.isPlaying)
        {
            _tmpText.text = value;
        }
        
    }
}
