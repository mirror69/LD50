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
        }
    }

    public override void Close()
    {
        base.Close();
        //Destroy(_photoDustMiniGame);
        //_photoDustMiniGame = null;
    }
}
