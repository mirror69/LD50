using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PazzlePartMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Vector2 mouseOffset;

    public void SaveStartPosition ()
    {
        startPosition = transform.position;
    }

    public void SetRandomPosition()
    {
        float xPos = transform.position.x + Random.Range(-100, 100);
        float yPos = transform.position.y + Random.Range(-100, 100);

        Vector3 newPosition = new Vector3(xPos, yPos, 0);
        transform.position = newPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseOffset = new Vector2(transform.position.x - eventData.position.x, transform.position.y - eventData.position.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = eventData.position;
        
        transform.position = mousePosition + mouseOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Vector3.Distance(transform.position, startPosition) < 10f)
        {
            transform.position = startPosition;
            PazzleDestroyer.readyParts++;
            this.enabled = false;
        }
    }
}
