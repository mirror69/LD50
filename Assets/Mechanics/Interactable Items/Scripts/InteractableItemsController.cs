using UnityEngine;

public class InteractableItemsController : MonoBehaviour
{
    [field: SerializeField]
    public InteractableItem[] Items { get; private set; }

    public bool IsSelectionEnabled { get; private set; } = true;

    private void OnEnable()
    {
        foreach (var item in Items)
        {
            item.MouseEnter += ItemOnMouseEnter;
            item.MouseExit += ItemOnMouseExit;
        }
    }

    private void OnDisable()
    {
        foreach (var item in Items)
        {
            item.MouseEnter -= ItemOnMouseEnter;
            item.MouseExit -= ItemOnMouseExit;
        }
    }

    public void SetEnabledSelection(bool enabled)
    {
        IsSelectionEnabled = enabled;
    }

    public void ClearSelection()
    {
        foreach (var item in Items)
        {
            item.ResetDraw();
        }
    }

    public void ItemOnMouseEnter(InteractableItem item)
    {
        if (IsSelectionEnabled)
        {
            item.DrawTooltip();
        }
    }

    private void ItemOnMouseExit(InteractableItem item)
    {
        item.ResetDraw();
    }

}
