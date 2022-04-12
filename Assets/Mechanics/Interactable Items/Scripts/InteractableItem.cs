using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum ItemTimerType
{
    BadItem = 0,
    GoodItem = 1,
    NeutralItem = 2
}

public enum ItemType
{
    Table = 0,
    Bulb = 1,
    Jacket = 2,
    Mirror = 3,
    Whiskey = 4,
    Curtain = 5,
    Chair = 6,
    TV = 7,
    Turntable = 8,
    Records = 9,
    Chess = 10,
    PhotoAlbum = 11,
    Lamp = 12
}

public class InteractableItem : MonoBehaviour
{
    [field: SerializeField]
    public ItemType Type { get; private set; }

    [field: SerializeField]
    public ItemTimerType TimerType { get; private set; }

    [field: SerializeField]
    public Transform StayPoint { get; private set; }

    [field: SerializeField]
    public InteractableItemDrawer Drawer { get; private set; }

    [field: SerializeField]
    public PlayableDirector InTimeline { get; private set; }

    [field: SerializeField]
    public PlayableDirector OutTimeline { get; private set; }

    [field: SerializeField]
    public AnimatorParamSet InAnimatorParamSet { get; private set; }

    [field: SerializeField]
    public AnimatorParamSet OutAnimatorParamSet { get; private set; }

    public Action<InteractableItem> MouseEnter;
    public Action<InteractableItem> MouseExit;

    private void OnMouseEnter()
    {
        MouseEnter?.Invoke(this);
    }

    private void OnMouseExit()
    {
        MouseExit?.Invoke(this);
    }

    public void DrawTooltip()
    {
        Drawer.Show();
    }

    public void ResetDraw()
    {
        Drawer.Hide();
    }
}
