using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public static CameraController Instance=null;

    [Header("Min and Max X axies values")]
    [SerializeField] float leftBorder;
    [SerializeField] float rightBorder;

    [Header("Zoom settings")]
    [SerializeField] private float timeToZoom;
    [SerializeField] private float maxCameraSizeInZoom;

    [Header("Main character")]
    [SerializeField] Transform playerTransform;

    private Camera mainCamera;
    private float currentTime;
    private float targetCameraSize;
    private float startCameraSize;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(this);

        mainCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        currentTime = timeToZoom;
        startCameraSize = mainCamera.orthographicSize;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            ZoomCamera();
        if (Input.GetKeyDown(KeyCode.X))
            ResetCamera();

        if (currentTime<timeToZoom)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetCameraSize, currentTime * Time.deltaTime);
            currentTime += Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        SetNewCameraPosition();
    }

    private void SetNewCameraPosition ()
    {
        Vector3 newCameraPosition = transform.position;

        if (playerTransform.position.x < leftBorder)
        {
            newCameraPosition.x = leftBorder;
        }
        else if (playerTransform.position.x > rightBorder)
        {
            newCameraPosition.x = rightBorder;
        }
        else
        {
            newCameraPosition.x = playerTransform.position.x;
        }

        transform.position = newCameraPosition;
    }

    public Vector3 GetPlayerPosition ()
    {
        return playerTransform.position;
    }

    private void ChangeCameraZoom (float zoomValue)
    {
        currentTime = 0;
        targetCameraSize = zoomValue;
    }

    public void ZoomCamera ()
    {
        ChangeCameraZoom(maxCameraSizeInZoom);
    }

    public void ResetCamera ()
    {
        ChangeCameraZoom(startCameraSize);
    }
}
