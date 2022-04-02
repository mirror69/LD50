using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PErsonMove : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        float input = Input.GetAxis("Horizontal");

        if (Mathf.Abs(input)>0.1f)
        {
            Vector2 newPos = new Vector2(transform.position.x + input * speed * Time.deltaTime, transform.position.y);
            transform.position = newPos;
        }
    }
}
