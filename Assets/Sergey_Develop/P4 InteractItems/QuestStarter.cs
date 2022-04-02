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
        //Debug.Log(Vector3.Distance(transform.position, CameraController.Instance.GetPlayerPosition()));

        if (Vector3.Distance(transform.position, CameraController.Instance.GetPlayerPosition()) < distanceToStartQuest)
        {
            CharacterIsNear = true;
            if (!questIsActivated)
                StartQuest();
        }
    }

    public void StartQuest ()
    {
        questIsActivated = true;
        Debug.Log("Quest is STARTED");
        CameraController.Instance.ZoomCamera();

        //и потом что то должно произойти в плане запуска миниигры
    }
}
