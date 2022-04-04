using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameScreenController : MonoBehaviour
{
    [field: SerializeField]
    public BlackScreen BlackScreen { get; private set; }

    [field: SerializeField]
    public MainGameScreen MainGameScreen { get; private set; }
    [field: SerializeField]
    public TestMiniGameScreen TestMiniGameScreen { get; private set; }
    [field: SerializeField]
    public GameScreen ChessMiniGameScreen { get; private set; }
    [field: SerializeField]
    public GameScreen PhotoAlbumMiniGameScreen { get; private set; }
    [field: SerializeField]
    public GameScreen TurntableMiniGameScreen { get; private set; }


    [SerializeField]
    private Texture2D _cursorTexture;
    [SerializeField]
    private Texture2D _cursorTextureYellow;

    public GameScreen CurrentScreen { get; private set; }

    private List<GameScreen> _screens;

    private void Awake()
    {
        _screens = new List<GameScreen>();
        _screens.Add(MainGameScreen);

        _screens.Add(TestMiniGameScreen);
        _screens.Add(ChessMiniGameScreen);
        _screens.Add(PhotoAlbumMiniGameScreen);
        _screens.Add(TurntableMiniGameScreen);

        CursorManager cursorManager = new CursorManager();
        cursorManager.Init(_cursorTexture, _cursorTextureYellow);
        CurrentScreen = MainGameScreen;
    }

    public void ShowItemScreen(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Bulb:
                ShowScreen(TestMiniGameScreen); //пока только для тестов
                break;
            case ItemType.Chair:
                break;
            case ItemType.Chess:
                ShowScreen(ChessMiniGameScreen);
                break;
            case ItemType.Curtain:
                break;
            case ItemType.Jacket:
                break;
            case ItemType.Mirror:
                break;
            case ItemType.PhotoAlbum:
                ShowScreen(PhotoAlbumMiniGameScreen);
                break;
            case ItemType.Records:
                ShowScreen(TurntableMiniGameScreen);
                break;
            case ItemType.Table:
                break;
            case ItemType.Turntable:
                break;
            case ItemType.TV:
                break;
            case ItemType.Whiskey:
                break;

            default:
                break;
        }
    }

    public void CloseCurrentScreen()
    {
        CurrentScreen.Close();
        CurrentScreen = MainGameScreen;
        CurrentScreen.Show();
    }

    private void ShowScreen(GameScreen screen)
    {
        foreach (var item in _screens)
        {
            if (item.IsActive && item != screen)
            {
                item.Close();
            }
            else if (!item.IsActive && item == screen)
            {
                item.Show();
            }
        }

        CurrentScreen = screen;
    }
}