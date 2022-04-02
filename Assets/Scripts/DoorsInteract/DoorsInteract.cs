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

    public delegate void DoorsEvents();
    public static event DoorsEvents Opened;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector2 coursorPos = Input.mousePosition;
        float screenMiddle = _camera.pixelWidth / 2;

        var leftDoorRot = _leftDoor.transform.rotation;
        var rightDoorRot = _rightDoor.transform.rotation;
        Vector3 vect = _rightDoor.transform.right;
        vect.y = 0;
        float angle = Vector3.SignedAngle(vect, Vector3.forward, Vector3.up);

        //Debug.Log(_rightDoor.transform.rotation.y);
        Debug.Log(angle);

        float dir = -Input.GetAxis("Mouse X");

        if (Input.GetMouseButton(0) && coursorPos.x > screenMiddle)
        {

            if (dir  < 0 && angle <= 120f || dir > 0 && angle >= 0)
            {

                _rightDoor.transform.Rotate(0, dir  * _rotateMultiplier, 0);

            }


            //Debug.Log(Vector3.Angle(Vector3.up, transform.rotation.));
            //if (_rightDoor.transform.rotation.y == Mathf.Clamp(_rightDoor.transform.rotation.y, _rightDoorMinValue, _rightDoorMaxValue))
            //{
            //    _rightDoor.transform.Rotate(0, -Input.GetAxis("Mouse X") * _rotateMultiplier, 0);
            //}

            //if (_rightDoor.transform.rotation.y == Mathf.Clamp(_rightDoor.transform.rotation.y, _rightDoorMinValue - 10f, _rightDoorMinValue + 10f) && Input.GetAxis("Mouse X") < 0)
            //{
            //    _rightDoor.transform.Rotate(0, -Input.GetAxis("Mouse X") * _rotateMultiplier, 0);
            //}

            //if (_rightDoor.transform.rotation.y == Mathf.Clamp(_rightDoor.transform.rotation.y, _rightDoorMaxValue - 10f, _rightDoorMaxValue + 10f) && Input.GetAxis("Mouse X") > 0)
            //{
            //    _rightDoor.transform.Rotate(0, -Input.GetAxis("Mouse X") * _rotateMultiplier, 0);
            //}

        }
        else if (Input.GetMouseButton(0) && coursorPos.x < screenMiddle)
        {
            _leftDoor.transform.Rotate(0, -Input.GetAxis("Mouse X") * _rotateMultiplier, 0);
        }

    }
}
