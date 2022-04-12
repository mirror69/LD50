using System;
using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private PlayerMovements _playerMovements;
    [SerializeField]
    private PlayerAnimations _playerAnimations;
    [SerializeField]
    private PlayerSounds _playerSounds;

    private DestinationPoint _currentDestinationPoint;

    /// <summary>
    /// Current direction the character is facing.
    /// x = -1 means character turned to the left, 1 - to the right
    /// y = -1 means character standing facing us, 1 - back to us
    /// </summary>
    private Vector2Int _currentPointMoveDirection = new Vector2Int(-1, -1);

    public event Action<DestinationPoint> DestinationPointReached;

    private void OnEnable()
    {
        PlayerMovements.ReachedDestination += OnDestinationPointReached;
        _playerAnimations.RotationStarted += OnRotationStarted;
        _playerAnimations.RotationEnded += OnRotationEnded;
        _playerAnimations.StepHappened += OnStepHappened;
        _playerAnimations.StickHitHappened += OnStickHitHappened;
    }

    private void OnStickHitHappened()
    {
        _playerSounds.PlayStickSound();
    }

    private void OnStepHappened()
    {
        _playerSounds.PlayWalkSound();
    }

    private void OnDisable()
    {
        PlayerMovements.ReachedDestination -= OnDestinationPointReached;
        _playerAnimations.RotationStarted -= OnRotationStarted;
        _playerAnimations.RotationEnded -= OnRotationEnded;
    }

    private void OnRotationEnded()
    {
        _playerMovements.StartAgent();
    }

    private void OnRotationStarted()
    {
        _playerMovements.StopAgent();
    }

    public void SetNewTargetPosition(DestinationPoint destinationPoint)
    {
        StopAllCoroutines();
        _currentDestinationPoint = destinationPoint;

        _currentPointMoveDirection.y = -1;
        if (_playerMovements.IsStayingOnPoint(_currentDestinationPoint.point))
        {
            _playerAnimations.SetDirection(_currentPointMoveDirection);
            OnDestinationPointReached();
            return;
        }
        
        _currentPointMoveDirection.x = destinationPoint.point.x > transform.position.x ? 1 : -1;

        _playerMovements.Move(_currentPointMoveDirection.x, _currentDestinationPoint.point);
        _playerAnimations.SetDirection(_currentPointMoveDirection);
        _playerAnimations.SetMoving(true);
    }

    public void TryApplyAnimParams(AnimatorParamSet animParamSet)
    {
        if (animParamSet == null)
        {
            return;
        }

        if (animParamSet.IntParams != null)
        {
            foreach (var item in animParamSet.IntParams)
            {
                _playerAnimations.SetParam(item);
            }
        }

        if (animParamSet.BoolParams != null)
        {
            foreach (var item in animParamSet.BoolParams)
            {
                _playerAnimations.SetParam(item);
            }
        }
    }

    public void ProcessDeath()
    {
        Invoke(nameof(SetAnimatorDead), 0.5f);

        if (_playerAnimations.IsSitting())
        {
            _playerSounds.PlayDeadSittingSound();
        }
        else
        {
            _playerSounds.PlayDeadSound();
        }
    }

    public void ProcessWin()
    {
        if (_playerAnimations.IsSitting())
        {
            _playerAnimations.SetSitting(false);
        }
        _playerMovements.Stop();
        _playerMovements.StopAgent();
        _currentPointMoveDirection = new Vector2Int(1, -1);
        _playerAnimations.SetDirection(_currentPointMoveDirection);
    }

    private void SetAnimatorDead()
    {
        _playerAnimations.SetDead();
        Invoke(nameof(StopAgent), 0.5f);
    }

    private void StopAgent()
    {
        _playerMovements.StopAgent();
    }

    private void OnDestinationPointReached()
    {
        StartCoroutine(StopOnDestinationPoint());
    }

    private IEnumerator StopOnDestinationPoint()
    {
        _playerMovements.Stop();
        _playerAnimations.SetMoving(false);

        Vector2Int direction = new Vector2Int(0, 0);
        if (_currentDestinationPoint.item != null)
        {
            direction.x = _currentDestinationPoint.item.StayPoint.right.x > 0 ? 1 : -1;
            direction.y = _currentDestinationPoint.item.StayPoint.right.y > 0 ? 1 : -1;
            _playerAnimations.SetDirection(direction);

            // waiting for rotation end
            if (_currentPointMoveDirection.x != direction.x)
            {
                yield return new WaitForSeconds(0.5f);
            }
        }

        DestinationPointReached?.Invoke(_currentDestinationPoint);
    }
}
