using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof(SpriteRenderer))]
public class InteractableItemDrawer : MonoBehaviour
{
    [SerializeField] private Color mouseOnItemColor; 

    private SpriteRenderer _renderer;
    private Color startColor;
    private Transform hiddenObjects;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        startColor = _renderer.color;
        hiddenObjects = transform.GetChild(0);
    }

    public void Show()
    {
        hiddenObjects.gameObject.SetActive(true);
        _renderer.color = mouseOnItemColor;
    }

    public void Hide()
    {
        hiddenObjects.gameObject.SetActive(false);
        _renderer.color = startColor;
    }

    //private void OnMouseEnter()
    //{
    //    hiddenObjects.gameObject.SetActive(true);
    //    _renderer.color = mouseOnItemColor;
    //}

    //private void OnMouseExit()
    //{
    //    hiddenObjects.gameObject.SetActive(false);
    //    _renderer.color = startColor;
    //}
}
