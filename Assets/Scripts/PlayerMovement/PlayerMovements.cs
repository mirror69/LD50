using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour
{
    [Header("Movement vars")]
    [SerializeField] private float _speed;

    public delegate void PlayerPositionEvents();
    public static event PlayerPositionEvents ReachedDestination;

    public void Move(float direction, float newXPos)
    {
        if (Mathf.Abs(direction) > 0.01f)
        {
            HorizontalMovement(newXPos);
        }
    }

    private void HorizontalMovement(float newXPos)
    {
        Vector2 newDestination = new Vector2(newXPos, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, newDestination, _speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, newDestination) < 0.1f)
        {
            ReachedDestination();
        }
    }
}
