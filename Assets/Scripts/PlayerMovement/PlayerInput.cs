using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovements))]
[RequireComponent(typeof(PlayerAnimations))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float gruntTimer;

    private PlayerMovements _playerMovements;
    private PlayerAnimations _playerAnimations;
    private PlayerSounds playerSounds;
    private float _newTargetPosX;
    private float _horizontalDirection;
    private float _currTime;
    private bool _isGruntTimerOn = false;
    private bool _isWalkSoundPlaying = false;

    private DestinationPoint _currentDestinationPoint;

    public event Action<DestinationPoint> DestinationPointReached;

    private void OnEnable()
    {
        PlayerMovements.ReachedDestination += OnDestinationPointReached;
    }

    private void OnDisable()
    {
        PlayerMovements.ReachedDestination -= OnDestinationPointReached;
    }

    private void Awake()
    {
        playerSounds = GetComponent<PlayerSounds>();
        _playerMovements = GetComponent<PlayerMovements>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _newTargetPosX = 0;
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
                playerSounds.PlayWalkSound();
                Debug.Log(" GDE ZVUK");
                _isWalkSoundPlaying = true;
                _currTime = Time.time;
            }
        }
        else
        {
            playerSounds.StopWalkSound();
            _isWalkSoundPlaying = false;
            _isGruntTimerOn = false;
        }

        if (_isGruntTimerOn)
        {
            GruntTimer();
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
        _playerAnimations.SetDead();
    }

    public void SetNewTargetPosition(DestinationPoint destinationPoint)
    {
        _currentDestinationPoint = destinationPoint;
        _newTargetPosX = destinationPoint.point.x;
        _isGruntTimerOn = true;
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

    private void GruntTimer()
    {
        if (Time.time - _currTime > 2f)
        {
            playerSounds.PlayGrountSound();
            _currTime = Time.time;
        }
    }
}
