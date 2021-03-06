using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour
{
    public event Action<DestinationPoint> DestinationPointClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            if (hit.collider != null)
            {
                var item = hit.collider.GetComponent<InteractableItem>();
                Vector2 point;
                if (item != null)
                {
                    point = item.StayPoint.position;
                    if (!item.IsAvailableToInteract)
                    {
                        item = null;
                    }
                }
                else
                {
                    point = hit.point;
                }

                DestinationPointClicked?.Invoke(
                    new DestinationPoint(point, item));
            }
        }
    }
}
