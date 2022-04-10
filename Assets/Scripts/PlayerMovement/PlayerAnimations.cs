using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AnimatorParam<T>
{
    public string Name;
    public T Value;
}

[Serializable]
public class AnimatorParamSet
{
    public AnimatorParam<int>[] IntParams;
    public AnimatorParam<bool>[] BoolParams;
}

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public event Action RotationStarted;
    public event Action RotationEnded;
    public event Action StepHappened;
    public event Action StickHitHappened;

    public bool IsSitting()
    {
        return _animator.GetBool("IsSitting");
    }

    public void SetSitting(bool isSitting)
    {
        _animator.SetBool("IsSitting", isSitting);
    }

    public void SetParam(AnimatorParam<int> intParam)
    {
        _animator.SetInteger(intParam.Name, intParam.Value);
    }

    public void SetParam(AnimatorParam<bool> param)
    {
        _animator.SetBool(param.Name, param.Value);
    }

    public void SetMoving(bool isMoving)
    {
        _animator.SetBool("IsMoving", isMoving);
    }

    public void SetDirection(Vector2Int direction)
    {
        _animator.SetInteger("XDirection", direction.x);
        _animator.SetInteger("YDirection", direction.y);
    }

    public void SetDead()
    {
        Invoke(nameof(DeadTrigger), 0.5f);
    }

    /// <summary>
    /// Notify about rotation animation start (calling by animation signal)
    /// </summary>
    public void NotifyStartRotation()
    {
        RotationStarted?.Invoke();
    }

    /// <summary>
    /// Notify about rotation animation end (calling by animation signal)
    /// </summary>
    public void NotifyEndRotation()
    {
        RotationEnded?.Invoke();
    }

    /// <summary>
    /// Notify about step (calling by animation signal)
    /// </summary>
    public void NotifyStepHappened()
    {
        StepHappened?.Invoke();
    }

    /// <summary>
    /// Notify about stick hited floor (calling by animation signal)
    /// </summary>
    public void NotifyStickHitHappened()
    {
        StickHitHappened?.Invoke();
    }

    private void DeadTrigger()
    {
        _animator.SetTrigger("IsDead");
    }
}
