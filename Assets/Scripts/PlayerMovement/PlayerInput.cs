using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private PlayerMovements _playerMovements;
    [SerializeField]
    private PlayerAnimations _playerAnimations;
    [SerializeField]
    private PlayerSounds _playerSounds;

    private float _newTargetPosX = 0;
    private float _horizontalDirection;
    private float _currTime;
    private bool _isGruntTimerOn = false;
    private bool _isWalkSoundPlaying = false;

    private DestinationPoint _currentDestinationPoint;

    public event Action<DestinationPoint> DestinationPointReached;

    private void OnEnable()
    {
        PlayerMovements.ReachedDestination += OnDestinationPointReached;
        _playerAnimations.RotationStarted += OnRotationStarted;
        _playerAnimations.RotationEnded += OnRotationEnded;

    }

    private void OnRotationEnded()
    {
        _playerMovements.StartAgent();
    }

    private void OnRotationStarted()
    {
        _playerMovements.StopAgent();
    }

    private void OnDisable()
    {
        PlayerMovements.ReachedDestination -= OnDestinationPointReached;
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        if (_newTargetPosX != 0 && _newTargetPosX > transform.position.x)
        {
            _horizontalDirection = 1;
        }
        else if (_newTargetPosX != 0 && _newTargetPosX < transform.position.x)
        {
            _horizontalDirection = -1;
        }
        else if (_newTargetPosX == 0)
        {
            _horizontalDirection = 0;
        }

        bool isMoving = (_newTargetPosX != 0);

        if (isMoving)
        {
            if (!_isWalkSoundPlaying)
            {
                _playerSounds.PlayWalkSound();
                _isWalkSoundPlaying = true;
                _currTime = Time.time;
            }
        }
        else
        {
            _playerSounds.StopWalkSound();
            _isWalkSoundPlaying = false;
            _isGruntTimerOn = false;
        }

    }

    private void FixedUpdate()
    {
        if (_newTargetPosX != 0)
        {
            _playerMovements.Move(_horizontalDirection, _currentDestinationPoint.point);
        }

        _playerAnimations.SetByVelocity(_playerMovements.GetVelocity());
    }

    public void SetNewTargetPosition(DestinationPoint destinationPoint)
    {
        _currentDestinationPoint = destinationPoint;
        _newTargetPosX = destinationPoint.point.x;
        _isGruntTimerOn = true;
    }

    public void TryApplyAnimParams(AnimatorIntParam[] animIntParams)
    {
        if (animIntParams.Length > 0)
        {
            foreach (var item in animIntParams)
            {
                _playerAnimations.SetByIntParam(item);
            }
        }
    }

    public void SetAnimatorDead()
    {
        Invoke(nameof(SetDead), 0.5f);
        _playerSounds.PlayDeadSound();
    }

    private void SetDead()
    {
        _playerAnimations.SetDead();
        Invoke(nameof(StopAgent), 0.5f);
 
    }

    private void StartAgent()
    {
        _playerMovements.StartAgent();
    }

    private void StopAgent()
    {
        _playerSounds.StopWalkSound();
        _playerMovements.StopAgent();
    }

    private void OnDestinationPointReached()
    {
        DestinationPointReached?.Invoke(_currentDestinationPoint);
        SetDestinationToZero();
    }

    private void SetDestinationToZero()
    {
        _newTargetPosX = 0;
    }
}
