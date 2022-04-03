using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovements))]
[RequireComponent(typeof(PlayerAnimations))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovements _playerMovements;
    private PlayerAnimations _playerAnimations;
    private float _lastDir;
    private float _newTargetPosX;
    private float _horizontalDirection;

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
        _playerMovements = GetComponent<PlayerMovements>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _lastDir = 1f;
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

        bool isMoving = (_horizontalDirection != 0);


        _playerAnimations.AnimatorStateChanger(isMoving);
    }

    private void FixedUpdate()
    {
        CharacterRotation();
        if (_newTargetPosX != 0)
        {
            _playerMovements.Move(_horizontalDirection, _newTargetPosX);
        }
    }

    public void SetNewTargetPosition(DestinationPoint destinationPoint)
    {
        _currentDestinationPoint = destinationPoint;
        _newTargetPosX = destinationPoint.point.x;
        Debug.Log(_newTargetPosX);
        Debug.Log(transform.position);
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

    private void CharacterRotation()
    {
        if (_horizontalDirection < 0) gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        if (_horizontalDirection > 0) gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
