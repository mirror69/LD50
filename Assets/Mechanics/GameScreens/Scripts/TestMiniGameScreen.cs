using UnityEngine;

public class TestMiniGameScreen : GameScreen
{
    [SerializeField]
    private GameObject TestMiniGamePrefab;

    private GameObject _testMiniGameObject;

    public override void Show()
    {
        base.Show();
        if (_testMiniGameObject == null)
        {
            _testMiniGameObject = Instantiate(TestMiniGamePrefab, transform);
            _testMiniGameObject.GetComponent<PhotoAlbumQuest>().OnPhotoAlbumQuestDone += TestMiniGameScreen_OnPhotoAlbumQuestDone;
        }
    }

    private void TestMiniGameScreen_OnPhotoAlbumQuestDone(PhotoAlbumQuest obj)
    {
        CloseRequested?.Invoke(GameScreenResult.WinGame);
    }

    public override void Close()
    {
        base.Close();
    }
}
