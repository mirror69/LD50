using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoAlbumQuest : MonoBehaviour
{
    public static PhotoAlbumQuest Instance = null;
    public event Action<PhotoAlbumQuest> OnPhotoAlbumQuestDone;

    [SerializeField]
    private List<MiniQuest> photoQuests;

    private int _currentQuestIndex;

    private void OnEnable()
    {
        MiniQuest.OnMiniQuestEnded += MiniQuest_OnMiniQuestEnded;
    }

    private void OnDisable()
    {
        MiniQuest.OnMiniQuestEnded -= MiniQuest_OnMiniQuestEnded;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);

        foreach (var elem in photoQuests)
        {
            elem.SetMiniQuestToPreviewImage();
        }
    }

    private void Start()
    {
        for (int i = 0; i < photoQuests.Count; i++)
        {
            if (!photoQuests[i].questIsDone)
            {
                _currentQuestIndex = i-1;
                break;
            }
        }
        HideAllPreviews();
        ShowNextPreview();
    }

    public void HideAllPreviews ()
    {
        foreach (var photo in photoQuests)
        {
            photo.previewImageInAlbum.gameObject.SetActive(false);
        }
    }

    public void ShowNextPreview ()
    {
        _currentQuestIndex++;

        if (_currentQuestIndex >= photoQuests.Count)
        {
            OnPhotoAlbumQuestDone?.Invoke(this);
            Debug.Log("??????? СОБЫТИЕ О ЗАВЕРШЕНИИ КВЕСТА С АЛЬБОМОМ ???????");
            return;
        }

        photoQuests[_currentQuestIndex].previewImageInAlbum.gameObject.SetActive(true);
    }

    private void MiniQuest_OnMiniQuestEnded(MiniQuest obj)
    {
        Debug.Log($"Закончился миниквест {obj.name}");

        StartCoroutine(TurnOffQuest(obj));
    }

    private IEnumerator TurnOffQuest (MiniQuest obj)
    {
        obj.previewImageInAlbum.gameObject.GetComponent<InteractablePhotoDrawer>().enabled = false;
        obj.previewImageInAlbum.gameObject.GetComponent<Collider2D>().enabled = false;

        obj.gameObject.GetComponent<Animator>().SetTrigger("SetPhotoInAlbum");

        yield return new WaitForSeconds(3f);

        ShowNextPreview();
    }
}
