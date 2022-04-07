/// <summary>
/// Условие TimeMachine трека, проверяющая выполнение условия каждый кадр
/// </summary>
public abstract class TMCondPolled : TimeMachineCondition
{
	
	private bool cachedCond = false;

	protected abstract bool EvaluateCondition();
	
	public override bool IsSatisfied()
	{
		return cachedCond;
	}

	public override void SetEnabled(bool enabled)
	{
		base.SetEnabled(enabled);
		cachedCond = false;
	}

	private void Update()
	{
		if (EvaluateCondition())
		{
			cachedCond = true;
		}
	}
}
