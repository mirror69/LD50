using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSounds))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private PlayerSounds playerSounds;

    private void Start()
    {
        playerSounds = GetComponent<PlayerSounds>();
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
        playerSounds.PlayDeadSound();
        playerSounds.StopWalkSound();
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
