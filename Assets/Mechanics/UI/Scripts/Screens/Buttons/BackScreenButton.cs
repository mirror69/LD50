using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackScreenButton : MonoBehaviour
{
    private Button button;

    public void Init(UIEventMediator uiEventMediator)
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(uiEventMediator.RequestReturnToPreviousScreen);
    }
}
