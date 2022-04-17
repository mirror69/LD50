using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PazzlePartMovement : MonoBehaviour
{ 
    private PazzleDestroyer pazzleDestroyer;
    private Vector3 startPosition;
    private Vector2 mouseOffset=Vector2.zero;

    private SpriteRenderer spriteRenderer;
    private Vector2 mousePosition;

    public int SpriteSortingLayer => spriteRenderer.sortingOrder;

    public void Init(PazzleDestroyer pazzleDestroyer)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        this.pazzleDestroyer = pazzleDestroyer;
    }

    public void SetRandomPosition()
    {
        float xPos = Random.Range(-9.5f, 9.5f);
        float yPos = Random.Range(-4.5f, 4.5f);

        Vector3 newPosition = new Vector3(xPos, yPos, transform.localPosition.z);
        transform.localPosition = newPosition;
    }

    private void OnMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        if (hit.collider.gameObject == this.gameObject)
        {
            mouseOffset = new Vector2(transform.position.x - hit.point.x, transform.position.y - hit.point.y);
        }
        //Debug.Log(mouseOffset);
    }

    private void OnMouseUp()
    {
        if (Vector2.Distance(transform.position, startPosition) < 0.3f)
        {
            transform.position = startPosition;

            spriteRenderer.sortingOrder = pazzleDestroyer.MinSortingLayer - 1;
            pazzleDestroyer.AddReadyPartToCount();

            this.enabled = false;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPosition = mousePosition + mouseOffset;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
