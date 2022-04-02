using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class InteractibleItem : MonoBehaviour
{
    [SerializeField] private Color mouseOnItemColor; 

    private SpriteRenderer _renderer;
    private Color startColor;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        startColor = _renderer.color;
    }

    private void OnMouseEnter()
    {
        _renderer.color = mouseOnItemColor;
        //Debug.Log($"Мышь на объекте {name}");
    }

    private void OnMouseExit()
    {
        _renderer.color = startColor;
        //Debug.Log($"Мышь ушла с объекта {name}");
    }
}
