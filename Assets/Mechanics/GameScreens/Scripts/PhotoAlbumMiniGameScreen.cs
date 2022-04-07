using UnityEngine;

public class PhotoAlbumMiniGameScreen : GameScreen
{
    [SerializeField]
    private GameObject PhotoAlbumMiniGamePrefab;

    private GameObject _testMiniGameObject;

    public override void Show()
    {
        base.Show();
        if (_testMiniGameObject == null)
        {
            _testMiniGameObject = Instantiate(PhotoAlbumMiniGamePrefab, transform);
            _testMiniGameObject.GetComponent<PhotoAlbumQuest>().OnPhotoAlbumQuestDone += PhotoAlbumMiniGameScreen_OnPhotoAlbumQuestDone;
        }
    }

    private void PhotoAlbumMiniGameScreen_OnPhotoAlbumQuestDone(PhotoAlbumQuest obj)
    {
        wasAlreadyChoosen = true;
        CloseRequested?.Invoke(GameScreenResult.WinGame);
    }

    public override void Close()
    {
        base.Close();
    }
}
