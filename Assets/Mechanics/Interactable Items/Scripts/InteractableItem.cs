using System;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTimerType
{
    SpeedUpDeath,
    SlowDownDeath
}

public enum ItemType
{
    Table,
    TV
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
        Debug.Log("SHOW");

        Drawer.Show();
    }

    private void OnMouseExit()
    {
        Debug.Log("HIDE");

        Drawer.Hide();
    }

    public void ResetDraw()
    {
        Drawer.Hide();
    }
}
