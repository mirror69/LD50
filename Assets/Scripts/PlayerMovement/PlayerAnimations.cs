using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public event Action RotationStarted;
    public event Action RotationEnded;

    public void SetByIntParam(AnimatorIntParam intParam)
    {
        _animator.SetInteger(intParam.Name, intParam.Value);
    }

    public void SetByVelocity(Vector2 velocity)
    {
        int direction;

        if (velocity.x < 0)
        {
            direction = -1;

        }
        else if (velocity.x > 0)
        {
            direction = 1;

        }
        else
        {
            direction = 0;

        }

        _animator.SetInteger("XSpeed", direction);
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

    private void DeadTrigger()
    {
        _animator.SetTrigger("IsDead");
    }
}
