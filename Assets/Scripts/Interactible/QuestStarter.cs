using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStarter : MonoBehaviour
{
    [SerializeField] float distanceToStartQuest;

    private BlackScreen blackScreen;
    private bool CharacterIsNear;
    private bool questIsActivated;

    private void Awake()
    {
        blackScreen = GetComponent<BlackScreen>();
    }

    private DestinationPoint _currentDestinationPoint;

    private void Update()
    {
        if (Mathf.Abs(_currentDestinationPoint.point.x - CameraController.Instance.GetPlayerPosition().x) < distanceToStartQuest)
        {
            CharacterIsNear = true;
            if (!questIsActivated)
                StartQuest();
        }
    }

    public void Enable(DestinationPoint destinationPoint)
    {
        _currentDestinationPoint = destinationPoint;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        EndQuest();
        gameObject.SetActive(false);
    }

    private void StartQuest ()
    {
        blackScreen.Activate();
        questIsActivated = true;
        Debug.Log("Quest is STARTED");
        CameraController.Instance.ZoomCamera();
    }

    private void EndQuest ()
    {
        Debug.Log("Quest is ENDED");
        questIsActivated = false;
        CameraController.Instance.ResetCamera();
    }
}
