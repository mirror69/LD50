using System.Collections.Generic;
using UnityEngine;

public class GameScreenController : MonoBehaviour
{
    [field: SerializeField]
    public MainGameScreen MainGameScreen { get; private set; }

    [field: SerializeField]
    public TestMiniGameScreen TestMiniGameScreen { get; private set; }

    public GameScreen CurrentScreen { get; private set; }

    private List<GameScreen> _screens;

    private void Awake()
    {
        _screens = new List<GameScreen>();
        _screens.Add(MainGameScreen);
        _screens.Add(TestMiniGameScreen);
        CurrentScreen = MainGameScreen;
    }

    public void ShowItemScreen(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Bulb:
                ShowScreen(TestMiniGameScreen);
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