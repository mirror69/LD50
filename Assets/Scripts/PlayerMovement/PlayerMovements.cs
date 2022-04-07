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
    }

    public Vector2 GetVelocity()
    {
        return agent.velocity;
    }

    public void Move(float direction, Vector2 newPos)
    {
        if (Mathf.Abs(direction) > 0.01f)
        {
            HorizontalMovement(newPos);
        }
    }

    public void StartAgent()
    {
        agent.isStopped = false;
    }

    public void StopAgent()
    {
        agent.isStopped = true;
    }

    private void HorizontalMovement(Vector2 newPos)
    {
        if (agent.enabled == true)
        {

            agent.SetDestination(newPos);
        }

        if (!agent.pathPending && agent.remainingDistance < 0.2f)
        {
            ReachedDestination();
        }
    }
}
