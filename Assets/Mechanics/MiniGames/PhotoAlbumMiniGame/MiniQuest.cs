using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MiniQuest : MonoBehaviour
{
    //public Image previewImageInAlbum;
    public SpriteRenderer previewImageInAlbum;
    public Sprite originalImage;

    public static event Action<MiniQuest> OnMiniQuestEnded;

    public bool questIsDone;

    public void SetMiniQuestToPreviewImage()
    {
        Debug.Log($"{this.GetType()} is setted");
        previewImageInAlbum.gameObject.GetComponent<InteractablePhotoDrawer>().myMiniQuest = this;
    }

    public virtual void MiniQuestStart()
    {
        PhotoAlbumQuest.Instance.HideAllPreviews();
    }

    public virtual void MiniQuestEnded()
    {
        questIsDone = true;
        OnMiniQuestEnded?.Invoke(this);
        //gameObject.SetActive(false);
    }
}
