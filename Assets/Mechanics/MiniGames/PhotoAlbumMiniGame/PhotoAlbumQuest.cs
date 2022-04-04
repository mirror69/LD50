using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoAlbumQuest : MonoBehaviour
{
    public static PhotoAlbumQuest Instance = null;

    //[SerializeField]
    //private Button backButton;

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

        //backButton.gameObject.SetActive(false);
        //backButton.onClick.AddListener(ShowAllPreviews);

        foreach (var elem in photoQuests)
        {
            elem.SetMiniQuestToPreviewImage();
        }
    }

    private void Start()
    {
        HideAllPreviews();
        _currentQuestIndex = 0;
        ShowNextPreview();
    }

    public void HideAllPreviews ()
    {
        foreach (var photo in photoQuests)
        {
            photo.previewImageInAlbum.gameObject.SetActive(false);
        }
        //backButton.gameObject.SetActive(true);
    }

    public void ShowNextPreview ()
    {
        photoQuests[_currentQuestIndex].previewImageInAlbum.gameObject.SetActive(true);
    }

    private void MiniQuest_OnMiniQuestEnded(MiniQuest obj)
    {
        Debug.Log($"Çàêîí÷èëñÿ ìèíèêâåñò {obj.name}");

        StartCoroutine(TurnOffQuest(obj));
    }

    private IEnumerator TurnOffQuest (MiniQuest obj)
    {
        obj.previewImageInAlbum.gameObject.GetComponent<InteractablePhotoDrawer>().enabled = false;
        obj.previewImageInAlbum.gameObject.GetComponent<Collider2D>().enabled = false;

        obj.gameObject.GetComponent<Animator>().SetTrigger("SetPhotoInAlbum");

        yield return new WaitForSeconds(3f);


        _currentQuestIndex++;

        if (_currentQuestIndex < photoQuests.Count)
        {
            ShowNextPreview();
        }
        else
            Debug.Log("??????? ÑÎÁÛÒÈÅ Î ÇÀÂÅÐØÅÍÈÈ ÊÂÅÑÒÀ Ñ ÀËÜÁÎÌÎÌ ???????");
    }
}
