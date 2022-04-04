using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

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
        _animator.SetBool("IsAlive", false);
    }

    public void AnimatorStateChanger(bool isWalking)
    {
        if (isWalking)
        {
            _animator.SetBool("Walk", true);
            _animator.SetBool("Idle", false);
        }
        else
        {
            _animator.SetBool("Walk", false);
            _animator.SetBool("Idle", true);
        }
    }
}
