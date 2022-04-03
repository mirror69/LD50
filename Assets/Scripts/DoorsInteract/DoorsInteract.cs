using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsInteract : MonoBehaviour
{
    [SerializeField] private GameObject _leftDoor;
    [SerializeField] private GameObject _rightDoor;
    [SerializeField] private float _rotateMultiplier;
    [Header("Doors Settings")]
    [SerializeField] private float _rightDoorMinValue;
    [SerializeField] private float _rightDoorMaxValue;
    [SerializeField] private float _leftDoorMinValue;
    [SerializeField] private float _leftDoorMaxValue;
    private Camera _camera;
    private bool _rightDoorOpened;
    private bool _leftDoorOpened;
    private bool _isFinished;

    public delegate void DoorsEvents();
    public static event DoorsEvents Opened;

    private void Start()
    {
        _camera = Camera.main;
        _rightDoorOpened = false;
        _leftDoorOpened = false;
        _isFinished = false;
    }

    private void Update()
    {
        Vector2 coursorPos = Input.mousePosition;
        float screenMiddle = _camera.pixelWidth / 2;

        var leftDoorRot = _leftDoor.transform.rotation;
        var rightDoorRot = _rightDoor.transform.rotation;


        float dir = -Input.GetAxis("Mouse X");

        if (Input.GetMouseButton(0) && coursorPos.x > screenMiddle)
        {
            Vector3 vect = _rightDoor.transform.right;
            vect.y = 0;
            float angle = Vector3.SignedAngle(vect, Vector3.forward, Vector3.up);

            if (dir  < 0 && angle <= _rightDoorMaxValue || dir > 0 && angle >= _rightDoorMinValue)
            {
                _rightDoor.transform.Rotate(0, dir  * _rotateMultiplier, 0);
            }

            if (angle >= _rightDoorMaxValue)
            {
                _rightDoorOpened = true;
            }
            else
            {
                _rightDoorOpened = false;
            }
        }
        else if (Input.GetMouseButton(0) && coursorPos.x < screenMiddle)
        {
            Vector3 vect = _leftDoor.transform.right;
            vect.y = 0;
            float angle = Vector3.SignedAngle(vect, Vector3.forward, Vector3.up);

            if (dir < 0 && angle <= _leftDoorMaxValue || dir > 0 && angle >= _leftDoorMinValue)
            {
                _leftDoor.transform.Rotate(0, dir * _rotateMultiplier, 0);
            }

            if (angle <= _leftDoorMinValue)
            {
                _leftDoorOpened = true;
            }
            else
            {
                _leftDoorOpened = false;
            }
        }

        if (_rightDoorOpened && _leftDoorOpened && !_isFinished)
        {
            _isFinished = true;
            Debug.Log("Doors are opened!");
            //Opened();
        }

    }
}
