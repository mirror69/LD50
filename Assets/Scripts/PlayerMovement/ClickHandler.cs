using System;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public event Action<DestinationPoint> DestinationPointClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            if (hit.collider != null)
            {
                DestinationPointClicked?.Invoke(
                    new DestinationPoint(hit.point, hit.collider.GetComponent<InteractableItem>()));
            }
        }
    }
}
