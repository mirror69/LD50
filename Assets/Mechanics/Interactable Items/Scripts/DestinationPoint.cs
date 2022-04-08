using UnityEngine;

public class DestinationPoint
{
    public Vector2 point;
    public InteractableItem item;

    public DestinationPoint(Vector2 point, InteractableItem item)
    {
        this.point = point;
        this.item = item;
    }

    public bool IsEqual(DestinationPoint destinationPoint)
    {
        return destinationPoint != null && destinationPoint.item == item && destinationPoint.point == point;
    }
}
