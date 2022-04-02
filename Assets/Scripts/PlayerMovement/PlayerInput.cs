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

    private void OnEnable()
    {
        PlayerMovements.ReachedDestination += ReturnDestinationToZero;
    }
    private void OnDisable()
    {
        PlayerMovements.ReachedDestination -= ReturnDestinationToZero;
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

        CharacterRotation();
        if (_newTargetPosX !=0)
        {

        _playerMovements.Move(_horizontalDirection , _newTargetPosX);
        }
    }

    private void ReturnDestinationToZero()
    {
        _newTargetPosX = 0;
    }

    public void GiveNewTargetPosition( float x)
    {
        _newTargetPosX = x;
        Debug.Log(_newTargetPosX);
        Debug.Log(transform.position);
    }

    private void CharacterRotation()
    {
        if (_horizontalDirection < 0) gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        if (_horizontalDirection > 0) gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
