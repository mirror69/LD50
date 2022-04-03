using TMPro;
using UnityEngine;

public class DebugView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text TimePassedText;
    [SerializeField]
    private TMP_Text TimeToDeathText;
    [SerializeField]
    private TMP_Text GoodInteractionCountText;
    [SerializeField]
    private TMP_Text BadInteractionCountText;

    [SerializeField]
    private GameObject WinScreen;
    [SerializeField]
    private GameObject LoseScreen;

    public void SetTime(TimeState timeState, int timeToDeath)
    {
        TimePassedText.text = timeState.SecondsPassed.ToString();
        TimeToDeathText.text = timeToDeath.ToString();
    }

    public void SetInteractionsCount(GameData gameData)
    {
        GoodInteractionCountText.text = gameData.GoodItemCount.ToString();
        BadInteractionCountText.text = gameData.BadItemCount.ToString();
    }

    public void ShowWinScreen()
    {
        WinScreen.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        LoseScreen.SetActive(true);
    }
}
