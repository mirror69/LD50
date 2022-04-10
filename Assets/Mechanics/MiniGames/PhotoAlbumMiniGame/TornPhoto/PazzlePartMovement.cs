using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PazzlePartMovement : MonoBehaviour
{ 
    private PazzleDestroyer pazzleDestroyer;
    private Vector3 startPosition;
    private Vector2 mouseOffset=Vector2.zero;

    private bool isClicked;
    private SpriteRenderer spriteRenderer;
    private Vector2 mousePosition;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
        float xPos = Random.Range(-9.5f, 9.5f);
        float yPos = Random.Range(-4.5f, 4.5f);

        Vector3 newPosition = new Vector3(xPos, yPos, 0);
        transform.localPosition = newPosition;
    }

    private void OnMouseDown()
    {
        if (isClicked)
            return;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        if (hit.collider.gameObject == this.gameObject)
        {
            isClicked = true;
            mouseOffset = new Vector2(transform.position.x - hit.point.x, transform.position.y - hit.point.y);
        }
        Debug.Log(mouseOffset);
    }

    private void OnMouseUp()
    {
        isClicked = false;

        if (Vector3.Distance(transform.position, startPosition) < 0.3f)
        {
            transform.position = startPosition;

            spriteRenderer.sortingOrder--;
            pazzleDestroyer.AddReadyPartToCount();

            this.enabled = false;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void Update()
    {
        if (isClicked)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

            if (hit.point.x != 0 && hit.point.y != 0)
                 mousePosition = new Vector2(hit.point.x, hit.point.y);
            //Debug.Log($"transform={transform.position}, mouse={mousePosition}");
            transform.position = mousePosition + mouseOffset;
        }
    }
}
