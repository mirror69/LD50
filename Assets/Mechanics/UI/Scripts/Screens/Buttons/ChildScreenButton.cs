using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChildScreenButton: MonoBehaviour
{
    [SerializeField]
    private UIScreen childScreen;

    private Button button;

    public void Init(UIEventMediator uiEventMediator)
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => uiEventMediator.RequestShowChildScreen(childScreen));
    }
}
