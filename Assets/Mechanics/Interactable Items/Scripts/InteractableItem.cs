using System;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTimerType
{
    BadItem,
    GoodItem
}

public enum ItemType
{
    Table,
    Bulb
}

public class InteractableItem : MonoBehaviour
{
    [field: SerializeField]
    public ItemType Type { get; private set; }

    [field: SerializeField]
    public ItemTimerType TimerType { get; private set; }

    [field: SerializeField]
    public Transform LeftInteractionPoint { get; private set; }

    [field: SerializeField]
    public Transform RightInteractionPoint { get; private set; }

    [field: SerializeField]
    public InteractableItemDrawer Drawer { get; private set; }

    public Action<InteractableItem> Clicked;

    private void OnMouseEnter()
    {
        Drawer.Show();
    }

    private void OnMouseExit()
    {
        Drawer.Hide();
    }

    public void ResetDraw()
    {
        Drawer.Hide();
    }
}
