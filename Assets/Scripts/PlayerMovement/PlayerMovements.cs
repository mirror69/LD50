using UnityEngine;
using UnityEngine.AI;

public class PlayerMovements : MonoBehaviour
{

    private NavMeshAgent agent;

    public delegate void PlayerPositionEvents();
    public static event PlayerPositionEvents ReachedDestination;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
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
