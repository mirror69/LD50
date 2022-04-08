using UnityEngine;
using UnityEngine.AI;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer SpriteRenderer;
    [SerializeField]
    private float ChangeSortingOrderYValue;
    [SerializeField]
    private int MinSortingOrder;
    [SerializeField]
    private int MaxSortingOrder;

    private NavMeshAgent agent;
    private bool _isMoving;
    public delegate void PlayerPositionEvents();
    public static event PlayerPositionEvents ReachedDestination;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (transform.position.y < ChangeSortingOrderYValue)
        {
            if (SpriteRenderer.sortingOrder != MaxSortingOrder)
            {
                SpriteRenderer.sortingOrder = MaxSortingOrder;
            }
        }
        else
        {
            if (SpriteRenderer.sortingOrder != MinSortingOrder)
            {
                SpriteRenderer.sortingOrder = MinSortingOrder;
            }
        }

        if (_isMoving)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                _isMoving = false;
                ReachedDestination();
            }
        }
    }

    public bool IsStayingOnPoint(Vector2 point)
    {
        return Vector2.SqrMagnitude(new Vector2(transform.position.x, transform.position.y) - point) < 0.01f;
    }

    public Vector2 GetVelocity()
    {
        return agent.velocity;
    }

    public void Move(int direction, Vector2 newPos)
    {
        if (Mathf.Abs(direction) > 0)
        {
            MoveAgent(newPos);
        }
    }

    public void Stop()
    {
        agent.ResetPath();
    }

    public void StartAgent()
    {
        agent.isStopped = false;
    }

    public void StopAgent()
    {
        agent.isStopped = true;
    }

    private void MoveAgent(Vector2 newPos)
    {
        if (agent.enabled == true)
        {
            _isMoving = true;
            agent.SetDestination(newPos);
        }
    }
}
