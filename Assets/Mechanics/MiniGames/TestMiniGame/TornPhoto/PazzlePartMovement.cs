using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PazzlePartMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private PazzleDestroyer pazzleDestroyer;
    private Vector3 startPosition;
    private Vector2 mouseOffset;

    public void SaveStartPosition ()
    {
        startPosition = transform.position;
    }

    public void SetPazzleDestroyer (PazzleDestroyer pazzleDestroyer)
    {
        this.pazzleDestroyer = pazzleDestroyer;
    }

    public void SetRandomPosition()
    {
        float xPos = Random.Range(50, Camera.main.pixelWidth-50);
        float yPos = Random.Range(50, Camera.main.pixelHeight-50);

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

            pazzleDestroyer.AddReadyPartToCount();

            this.enabled = false;
        }
    }
}
