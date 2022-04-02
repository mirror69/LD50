using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PErsonMove : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 target;

    private void Start()
    {
        target = transform.position;
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetNewPosition();
        }

        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
    }

    private void SetNewPosition ()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePos);
        target.x = mousePos.x;
    }

    #region ArrowsWalk
    //private void Update()
    //{
    //    float input = Input.GetAxis("Horizontal");

    //    if (Mathf.Abs(input)>0.1f)
    //    {
    //        Vector2 newPos = new Vector2(transform.position.x + input * speed * Time.deltaTime, transform.position.y);
    //        transform.position = newPos;
    //    }
    //}
    #endregion
}
