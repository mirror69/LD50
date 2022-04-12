using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableItemDrawer : MonoBehaviour
{
    [SerializeField] private GameObject Content;

    private void Awake()
    {
        Content.SetActive(false);
    }

    public void Show()
    {
        Content.SetActive(true);
    }

    public void Hide()
    {
        Content.SetActive(false);
    }
}
