using UnityEngine;

/// <summary>
/// Условие TimeMachine трека
/// </summary>
public abstract class TimeMachineCondition : MonoBehaviour
{
    public bool IsEnabled => gameObject.activeSelf;


    public abstract bool IsSatisfied();
    
    public virtual void SetEnabled(bool enabled)
    {
        gameObject.SetActive(enabled); 
    }
}
