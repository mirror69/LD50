using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public GameSettings GameSettings;
    [SerializeField]
    public TimeController TimeController;
    [SerializeField]
    public GameScreenController GameScreenController;
    [SerializeField]
    private QuestStarter QuestStarter;
    [SerializeField]
    public PlayerInput Player;

    [Space]
    [Header("Debug")]
    [SerializeField]
    private DebugView _debugView;

    private GameData _gameData;

    private NavMeshAgent _agent;
    private PlayerAnimations _animations;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _agent = Player.gameObject.GetComponent<NavMeshAgent>();
        _animations = Player.gameObject.GetComponent<PlayerAnimations>();

        _gameData = new GameData();
        _gameData.Init(GameSettings);

        Player.DestinationPointReached += OnPlayerReachedDestinationPoint;
        GameScreenController.MainGameScreen.DestinationPointClicked += OnDestinationPointClicked;

        TimeController.DeathTimeOver += OnDeathTimeOver;
        TimeController.WinTimeReached += OnWinTimeReached;

        TimeController.Init(GameSettings, _gameData, _debugView);
        TimeController.StartTime();

        RefreshDebugView();
    }

    private bool IsWinReached()
    {
        return _gameData.GoodItemCount >= GameSettings.TimeSettings.GoodItemCountToWin
            && TimeController.IsWinTimeReached();
    }

    private void StopCurrentInteraction()
    {
        if (_gameData.CurrentInteractingItem == null)
        {
            return;
        }

        ItemTimerType timerType = _gameData.CurrentInteractingItem.TimerType;
        switch (timerType)
        {
            case ItemTimerType.BadItem:
                TimeController.StopBadInteraction();
                _gameData.AddBadItemInteraction();
                break;
            case ItemTimerType.GoodItem:
                TimeController.StopGoodInteraction();
                _gameData.AddGoodItemInteraction();

                if (IsWinReached())
                {
                    ProcessWinAfterDelay(GameSettings.TimeSettings.WinDelayAfterLastItemUse);
                }

                break;
            default:
                break;
        }

        _gameData.ResetCurrentInteraction();
        QuestStarter.Disable();

        RefreshDebugView();
    }

    private void ProcessWinAfterDelay(int delay)
    {
        TimeController.StopTime();
        Invoke(nameof(ProcessWinActions), delay);
    }

    private void ProcessWinActions()
    {
        TimeController.StopTime();
        _debugView.ShowWinScreen();
    }

    private void ProcessLoseActions()
    {
        TimeController.StopTime();
        _debugView.ShowLoseScreen();
        _agent.enabled = false;
        _animations.DedDead();
    }

    private void ProcessItemInteraction(InteractableItem item)
    {
        _gameData.SetCurrentInteraction(item);

        if (item.TimerType == ItemTimerType.BadItem)
        {
            TimeController.StartBadInteraction();
        }
        else
        {
            TimeController.StartGoodInteraction();
            GameScreenController.BlackScreen.Activate(() => StartMiniGame(item));
        }
    }

    private void StartMiniGame(InteractableItem item)
    {
        GameScreenController.ShowItemScreen(item.Type);
        GameScreenController.CurrentScreen.CloseRequested += OnGameScreenCloseRequested;
    }

    private void OnWinTimeReached()
    {
        if (IsWinReached())
        {
            ProcessWinActions();
        }
    }

    private void OnDeathTimeOver()
    {
        ProcessLoseActions();
    }

    private void OnDestinationPointClicked(DestinationPoint destinationPoint)
    {
        StopCurrentInteraction();
        Player.SetNewTargetPosition(destinationPoint);

        if (destinationPoint.item != null)
        {
            TimeController.PauseTime();
            if (destinationPoint.item.TimerType == ItemTimerType.BadItem)
            {
                QuestStarter.Enable(destinationPoint);
            }
        }
    }

    private void OnPlayerReachedDestinationPoint(DestinationPoint destinationPoint)
    {
        if (destinationPoint.item != null)
        {
            TimeController.ResumeTime();
            destinationPoint.item.ResetDraw();
            ProcessItemInteraction(destinationPoint.item);
        }
    }

    private void OnGameScreenCloseRequested()
    {
        GameScreenController.CurrentScreen.CloseRequested -= OnGameScreenCloseRequested;
        GameScreenController.CloseCurrentScreen();
        StopCurrentInteraction();
    }

    private void RefreshDebugView()
    {
        _debugView.SetInteractionsCount(_gameData);
    }
}
