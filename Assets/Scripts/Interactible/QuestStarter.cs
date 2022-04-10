using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStarter : MonoBehaviour
{
    [SerializeField] float distanceToStartQuest;

    private CameraController _cameraController;
    private GameSettings _gameSettings;

    private bool questIsActivated;


    private Vector2 _currentDestinationPoint;

    private void Update()
    {
        if (Mathf.Abs(_currentDestinationPoint.x - _cameraController.GetPlayerPosition().x) < distanceToStartQuest)
        {
            if (!questIsActivated)
                StartQuest();
        }
    }

    public void Init(CameraController cameraController, GameSettings gameSettings)
    {
        _cameraController = cameraController;
        _gameSettings = gameSettings;
    }

    public void Enable(Vector2 destinationPoint)
    {
        _currentDestinationPoint = destinationPoint;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        EndQuest();
        gameObject.SetActive(false);
    }

    private void StartQuest()
    {
        questIsActivated = true;
        //Debug.Log("Quest is STARTED");
        _cameraController.ZoomCamera(_gameSettings.CameraSettings.BadItemZoomInTime);
    }

    private void EndQuest()
    {
        //Debug.Log("Quest is ENDED");
        questIsActivated = false;
        _cameraController.ResetCamera(_gameSettings.CameraSettings.BadItemZoomOutTime);
    }
}
