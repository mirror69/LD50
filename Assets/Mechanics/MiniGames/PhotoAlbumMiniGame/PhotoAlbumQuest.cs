using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoAlbumQuest : MonoBehaviour
{
    public static PhotoAlbumQuest Instance = null;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private List<MiniQuest> photoQuests;

    private int _currentQuestIndex=0;

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

        backButton.gameObject.SetActive(false);
        backButton.onClick.AddListener(ShowAllPreviews);
    }

    public void HideAllPreviews ()
    {
        foreach (var photo in photoQuests)
        {
            photo.previewImageInAlbum.enabled = false;
        }
        backButton.gameObject.SetActive(true);
    }

    public void ShowAllPreviews ()
    {
        foreach (var photo in photoQuests)
        {
            photo.previewImageInAlbum.enabled = true;
            if (photo.gameObject.activeSelf)
                photo.gameObject.SetActive(false);
        }
        backButton.gameObject.SetActive(false);
    }

    private void MiniQuest_OnMiniQuestEnded(MiniQuest obj)
    {
        Debug.Log($"Закончился миниквест {obj.name}");

        obj.previewImageInAlbum.sprite = obj.originalImage;
        obj.previewImageInAlbum.color = Color.white;
        obj.previewImageInAlbum.gameObject.GetComponent<InteractablePhotoDrawer>().enabled = false;

        obj.gameObject.SetActive(false);
        ShowAllPreviews();

        _currentQuestIndex++;

        if (_currentQuestIndex == photoQuests.Count)
        {
            Debug.Log("??????? СОБЫТИЕ О ЗАВЕРШЕНИИ КВЕСТА С АЛЬБОМОМ ???????");
            //событие о завершении квеста с фотоальбомом
            return;
        }
    }
}
