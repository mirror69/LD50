using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MiniQuest : MonoBehaviour
{
    public Image previewImageInAlbum;
    public Sprite originalImage;

    public static event Action<MiniQuest> OnMiniQuestEnded;

    public bool questIsDone;

    public virtual void MiniQuestStart()
    {
        PhotoAlbumQuest.Instance.HideAllPreviews();
    }

    public virtual void MiniQuestEnded()
    {
        questIsDone = true;
        gameObject.SetActive(false);
        OnMiniQuestEnded?.Invoke(this);
    }
}
