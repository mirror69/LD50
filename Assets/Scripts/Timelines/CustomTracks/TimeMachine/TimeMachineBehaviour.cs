using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TimeMachineBehaviour : PlayableBehaviour
{
	public TimeMachineAction action;
	public Condition condition = Condition.Satisfied;
	public string markerToJumpTo, markerLabel;
	public float timeToJumpTo;
    public TimeMachineCondition tmCondition;

	[HideInInspector]
	public bool clipExecuted = false; //the user shouldn't author this, the Mixer does

    public bool NeedCheckCondition()
    {
        return tmCondition != null && condition == Condition.Satisfied;
    }

    public bool IsConditionEnabled()
    {
        if (tmCondition != null)
        {
            return tmCondition.IsEnabled;
        }
        return false;
    }

    public void SetEnabledCondition(bool enabled)
    {
        if (tmCondition != null)
        {
            tmCondition.SetEnabled(enabled);
        }       
    }

    public bool ConditionMet()
    {
        switch (condition)
        {
            case Condition.Always:
                return true;

            case Condition.Satisfied:
                return tmCondition.IsSatisfied();

            case Condition.Never:
            default:
                return false;
        }
    }

    public enum TimeMachineAction
	{
		Marker,
		JumpToTime,
		JumpToMarker,
		Pause,
	}

	public enum Condition
	{
		Always,
		Never,
		Satisfied,
	}
}
