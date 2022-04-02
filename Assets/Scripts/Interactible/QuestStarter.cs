using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStarter : MonoBehaviour
{
    [SerializeField] float distanceToStartQuest;

    private bool CharacterIsNear;
    private bool questIsActivated;

    private void Update()
    {
        if (Vector3.Distance(transform.position, CameraController.Instance.GetPlayerPosition()) < distanceToStartQuest)
        {
            CharacterIsNear = true;
            if (!questIsActivated)
                StartQuest();
        }

        else
        {
            CharacterIsNear = false;
            if (questIsActivated)
                QuestIsEnd();
        }
    }

    public void StartQuest ()
    {
        questIsActivated = true;
        Debug.Log("Quest is STARTED");
        CameraController.Instance.ZoomCamera();
    }

    public void QuestIsEnd ()
    {
        Debug.Log("Quest is ENDED");
        questIsActivated = false;
        CameraController.Instance.ResetCamera();
    }
}
