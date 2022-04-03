using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameScreen : MonoBehaviour
{
    [SerializeField]
    private Button CloseButton;

    public bool IsActive => gameObject.activeSelf;

    public Action CloseRequested;

    private void OnEnable()
    {
        CloseButton?.onClick.AddListener(() => CloseRequested?.Invoke());
    }

    private void OnDisable()
    {
        CloseButton?.onClick.RemoveAllListeners();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
