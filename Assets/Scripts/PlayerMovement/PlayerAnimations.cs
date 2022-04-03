using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private GameObject _animatorObj;
    private Animator _animator;

    private void Awake()
    {
        _animator = _animatorObj.GetComponent<Animator>();
    }

    public void AnimatorStateChanger(float direction)
    {
        if (direction > 0)
        {
            _animator.SetBool("WalkRight", true);
            _animator.SetBool("WalkLeft", false);
            _animator.SetBool("Idle", false);
        }
        else if(direction < 0)
        {
            _animator.SetBool("WalkRight", false);
            _animator.SetBool("WalkLeft", true);
            _animator.SetBool("Idle", false);
        }
        else
        {
            _animator.SetBool("WalkRight", false);
            _animator.SetBool("WalkLeft", false);
            _animator.SetBool("Idle", true);
        }
    }

    public void DedDead()
    {

        if (transform.position.x < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0,180,0);
        }
        _animator.SetTrigger("Dead");
    }
}
