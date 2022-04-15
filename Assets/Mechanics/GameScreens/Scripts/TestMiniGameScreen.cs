using UnityEngine;

public class TestMiniGameScreen : GameScreen
{
    [SerializeField]
    private PhotoAlbumQuest TestMiniGamePrefab;

    private PhotoAlbumQuest _testMiniGameObject;

    public override void Show()
    {
        base.Show();
        if (_testMiniGameObject == null)
        {
            _testMiniGameObject = Instantiate(TestMiniGamePrefab, transform);
            _testMiniGameObject.OnPhotoAlbumQuestDone += TestMiniGameScreen_OnPhotoAlbumQuestDone;
            //_testMiniGameObject.GetComponent<ChessController>().OnFinish += TestMiniGameScreen_OnFinish;

        }
    }

    private void TestMiniGameScreen_OnPhotoAlbumQuestDone(PhotoAlbumQuest obj)
    {
        RequestClose(GameScreenResult.WinGame);
    }

    public override void Close()
    {
        base.Close();
    }
}
