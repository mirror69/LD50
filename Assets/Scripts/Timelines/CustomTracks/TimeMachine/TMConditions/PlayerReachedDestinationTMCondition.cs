using UnityEngine;

public class PlayerReachedDestinationTMCondition : TMCondPolled
{
	[SerializeField]
	private PlayerInput PlayerInput;

	[SerializeField]
	private Transform Destination;

	protected override bool EvaluateCondition()
	{
		return Vector2.SqrMagnitude(Destination.position - PlayerInput.transform.position) < 0.2f;
	}
}
