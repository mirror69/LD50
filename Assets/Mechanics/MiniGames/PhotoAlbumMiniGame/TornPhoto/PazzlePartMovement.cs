using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PazzlePartMovement : MonoBehaviour
{ 
    private PazzleDestroyer pazzleDestroyer;
    private Vector3 startPosition;
    private Vector2 mouseOffset;

    private bool isClicked;

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
        float xPos = Random.Range(-10.5f, 10.5f);
        float yPos = Random.Range(-5f, 5f);

        Vector3 newPosition = new Vector3(xPos, yPos, 0);
        Debug.Log($"part {name} goes to {newPosition}");
        transform.localPosition = newPosition;
    }

    private void OnMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        if (hit.collider.gameObject == this.gameObject)
        {
            isClicked = true;
            mouseOffset = new Vector2(transform.position.x - hit.point.x, transform.position.y - hit.point.y);
        }
    }

    private void OnMouseUp()
    {
        isClicked = false;

        if (Vector3.Distance(transform.position, startPosition) < 0.3f)
        {
            transform.position = startPosition;

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
            Vector2 mousePosition = new Vector2(hit.point.x, hit.point.y);
            transform.position = mousePosition + mouseOffset;
        }
    }
}
