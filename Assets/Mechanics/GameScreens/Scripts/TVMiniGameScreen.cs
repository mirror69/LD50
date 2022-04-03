using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVMiniGameScreen : GameScreen
{
    [SerializeField]
    private GameObject TVMiniGamePrefab;

    private GameObject _tvMiniGameObject;

    public override void Show()
    {
        base.Show();
        if (_tvMiniGameObject == null)
        {
            _tvMiniGameObject = Instantiate(TVMiniGamePrefab, transform);
        }
    }

    public override void Close()
    {
        base.Close();
        //Destroy(_photoDustMiniGame);
        //_photoDustMiniGame = null;
    }
}
