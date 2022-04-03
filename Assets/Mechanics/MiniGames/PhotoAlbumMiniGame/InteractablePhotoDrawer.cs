using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractablePhotoDrawer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Color mouseOnItemColor;

    private Image image;
    private RectTransform rectTransform;
    private bool questIsPreparing;

    private Color startColor;

    public void OnPointerDown(PointerEventData eventData)
    {
        Transform go = transform.GetChild(0);
        go.gameObject.SetActive(true);

        MiniQuest miniQuest = go.gameObject.GetComponent<MiniQuest>();
        miniQuest.MiniQuestStart();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = mouseOnItemColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = startColor;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        startColor = image.color;
    }
}
