using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableItemDrawer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private GameObject Content;
    [SerializeField] private Color mouseOnItemColor; 

    private Color startColor;

    private void Awake()
    {
        Content.SetActive(false);
        startColor = Renderer.color;
    }

    public void Show()
    {
        Content.SetActive(true);
        Renderer.color = mouseOnItemColor;
    }

    public void Hide()
    {
        Content.SetActive(false);
        Renderer.color = startColor;
    }
}
