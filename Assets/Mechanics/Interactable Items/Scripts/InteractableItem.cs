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
    PhotoAlbum = 11
}

[Serializable]
public struct AnimatorIntParam
{
    public string Name;
    public int Value;
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
    public Transform LeftInteractionPoint { get; private set; }

    [field: SerializeField]
    public Transform RightInteractionPoint { get; private set; }

    [field: SerializeField]
    public InteractableItemDrawer Drawer { get; private set; }

    [field: SerializeField]
    public GameScreen GameScreen { get; private set; }

    [field: SerializeField]
    public PlayableDirector InTimeline { get; private set; }

    [field: SerializeField]
    public PlayableDirector OutTimeline { get; private set; }

    [field: SerializeField]
    public AnimatorIntParam[] OutAnimatorIntParams { get; private set; }

    public Action<InteractableItem> Clicked;

    private void OnMouseEnter()
    {
        Drawer.Show();
        CursorManager.Instance.SetCursorHighlight(true);
    }

    private void OnMouseExit()
    {
        Drawer.Hide();
        CursorManager.Instance.SetCursorHighlight(false);
    }

    public void ResetDraw()
    {
        Drawer.Hide();
    }
}
