using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoAlbumQuest : MonoBehaviour
{
    [SerializeField]
    private MiniQuest[] quests;

    private int _currentQuestIndex=0;

    private void OnEnable()
    {
        MiniQuest.OnMiniQuestEnded += MiniQuest_OnMiniQuestEnded;
    }

    private void OnDisable()
    {
        MiniQuest.OnMiniQuestEnded -= MiniQuest_OnMiniQuestEnded;
    }

    private void Start()
    {
        quests[_currentQuestIndex].MiniQuestStart();
    }

    private void MiniQuest_OnMiniQuestEnded(MiniQuest obj)
    {
        Debug.Log($"���������� ��������� {obj.name}");
        _currentQuestIndex++;

        if (_currentQuestIndex==quests.Length)
        {
            Debug.Log("??????? ������� � ���������� ������ � �������� ???????");
            //������� � ���������� ������ � ������������
            return;
        }

        WaitForNextQuest();
    }

    private void WaitForNextQuest ()
    {
        //�������� �������������� ��� �������� ������� �� ������
        Invoke(nameof(StartNextQuest), 2f);
    }

    private void StartNextQuest ()
    {
        quests[_currentQuestIndex].MiniQuestStart();
    }
}
