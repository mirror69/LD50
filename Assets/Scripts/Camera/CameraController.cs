using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMoveMode
{
    None = 0,
    FollowPlayer = 1,
    FollowRightEdge = 2
}

public class CameraController : MonoBehaviour
{
    [SerializeField] SpriteRenderer borderBackground;

    [Header("Zoom settings")]
    [SerializeField] private float maxCameraSizeInZoom = 4;

    [Header("Main character")]
    [SerializeField] Transform playerTransform;

    private Camera mainCamera;
    private float leftBorder;
    private float rightBorder;

    private float initialCameraSize;
    private float maxSizeDifference;

    private float moveSpeed;
    private CameraMoveMode moveMode;

    private void Awake()
    {
        mainCamera = Camera.main;
        initialCameraSize = mainCamera.orthographicSize;

        maxSizeDifference = Mathf.Abs(initialCameraSize - maxCameraSizeInZoom);

        leftBorder = borderBackground.transform.position.x + 
            borderBackground.sprite.bounds.min.x * borderBackground.transform.lossyScale.x;
        rightBorder = borderBackground.transform.position.x + 
            borderBackground.sprite.bounds.max.x * borderBackground.transform.lossyScale.x;

        moveMode = CameraMoveMode.FollowPlayer;
    }

    private void LateUpdate()
    {
        SetNewCameraPosition();
    }

    public void FollowRightEdge(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        moveMode = CameraMoveMode.FollowRightEdge;
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
        Vector3 leftBottomCameraPoint = mainCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 rightTopCameraPoint = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        float halfCameraWidth = 0.5f * (rightTopCameraPoint.x - leftBottomCameraPoint.x);

        Vector3 newCameraPosition = transform.position;

        if (moveMode == CameraMoveMode.FollowPlayer)
        {
            newCameraPosition.x = playerTransform.position.x;
        }
        else if (moveMode == CameraMoveMode.FollowRightEdge)
        {
            newCameraPosition.x = playerTransform.position.x;
            newCameraPosition.x = transform.position.x + moveSpeed * Time.deltaTime;
        }

        if (newCameraPosition.x + halfCameraWidth > rightBorder)
        {
            newCameraPosition.x = rightBorder - halfCameraWidth;
        }
        else if (newCameraPosition.x - halfCameraWidth < leftBorder)
        {
            newCameraPosition.x = leftBorder + halfCameraWidth;
        }

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
