using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractablePhotoDrawer : MonoBehaviour
{
    [SerializeField] private Color mouseOnItemColor;

    [HideInInspector]
    public MiniQuest myMiniQuest;
    private SpriteRenderer image;
    private RectTransform rectTransform;
    private bool questIsPreparing;

    private Color startColor;

    private void OnMouseUp()
    {
        myMiniQuest.gameObject.SetActive(true);
        myMiniQuest.MiniQuestStart();
    }

    private void OnMouseEnter()
    {
        image.color = mouseOnItemColor;
    }

    private void OnMouseExit()
    {
        image.color = startColor;
    }

    private void Awake()
    {
        image = GetComponent<SpriteRenderer>();
        rectTransform = GetComponent<RectTransform>();
        startColor = image.color;
    }
}
