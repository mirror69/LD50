using UnityEngine;

public class TableMiniGameScreen : GameScreen
{
    [SerializeField]
    private GameObject TableMiniGamePrefab;

    private GameObject _tableMiniGameObject;

    public override void Show()
    {
        base.Show();
        if (_tableMiniGameObject == null)
        {
            _tableMiniGameObject = Instantiate(TableMiniGamePrefab, transform);
        }
    }

    public override void Close()
    {
        base.Close();
        //Destroy(_photoDustMiniGame);
        //_photoDustMiniGame = null;
    }
}
