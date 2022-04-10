using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Min and Max X axies values")]
    [SerializeField] float leftBorder;
    [SerializeField] float rightBorder;

    [Header("Zoom settings")]
    [SerializeField] private float maxCameraSizeInZoom = 4;

    [Header("Main character")]
    [SerializeField] Transform playerTransform;

    private Camera mainCamera;
    private float initialCameraSize;
    private float maxSizeDifference;

    private bool isWinMode;

    private void Awake()
    {
        mainCamera = Camera.main;
        initialCameraSize = mainCamera.orthographicSize;

        maxSizeDifference = Mathf.Abs(initialCameraSize - maxCameraSizeInZoom);
    }

    private void LateUpdate()
    {
        SetNewCameraPosition();
    }

    public void SetWinModeOn()
    {
        isWinMode = true;
    }

    public Vector2 GetPlayerPosition()
    {
        return playerTransform.position;
    }

    public void ZoomCamera(float time)
    {
        StopAllCoroutines();
        StartCoroutine(ProcessZoom(maxCameraSizeInZoom, time));
    }

    public void ResetCamera(float time)
    {
        StopAllCoroutines();
        StartCoroutine(ProcessZoom(initialCameraSize, time));
    }

    private void SetNewCameraPosition()
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

        //if (isWinMode)
        //{
        //    newCameraPosition.x = playerTransform.position.x;
        //    newCameraPosition.x = Mathf.Lerp(transform.position.x, newCameraPosition.x, Time.deltaTime / 10);
        //}

        transform.position = newCameraPosition;
    }

    private IEnumerator ProcessZoom(float targetSize, float time)
    {
        if (mainCamera.orthographicSize == targetSize)
        {
            yield break;
        }

        float difference = targetSize - mainCamera.orthographicSize;
        time *= Mathf.Abs(difference / maxSizeDifference);
        float currentTime = 0;
        float startSize = mainCamera.orthographicSize;
        while (currentTime < time)
        {
            mainCamera.orthographicSize = startSize + difference * EaseInOutSine(currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.orthographicSize = targetSize;
    }

    private float EaseOutSine(float x)
    {
        return Mathf.Sin(x * Mathf.PI / 2);
    }

    private float EaseInOutSine(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }
}
