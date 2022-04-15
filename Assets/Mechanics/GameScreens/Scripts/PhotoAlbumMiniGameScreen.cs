using UnityEngine;

public class PhotoAlbumMiniGameScreen : GameScreen
{
    [SerializeField]
    private PhotoAlbumQuest PhotoAlbumMiniGamePrefab;

    private PhotoAlbumQuest _photoAlbumMiniGameObject;

    public override void Show()
    {
        base.Show();
        if (_photoAlbumMiniGameObject == null)
        {
            _photoAlbumMiniGameObject = Instantiate(PhotoAlbumMiniGamePrefab, transform);
            _photoAlbumMiniGameObject.OnPhotoAlbumQuestDone += PhotoAlbumMiniGameScreen_OnPhotoAlbumQuestDone;
            _photoAlbumMiniGameObject.SetEnabledMinigameMusicRequested += RequestSetEnabledMinigameMusic;
        }
    }

    private void PhotoAlbumMiniGameScreen_OnPhotoAlbumQuestDone(PhotoAlbumQuest obj)
    {
        wasAlreadyChoosen = true;
        RequestClose(GameScreenResult.WinGame);
    }

    public override void Close()
    {
        base.Close();
    }
}
