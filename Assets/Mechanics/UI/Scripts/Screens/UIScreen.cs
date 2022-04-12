using DG.Tweening;
using UnityEngine;

/// <summary>
/// ����� ������ ����. 
/// ����� ������������ ����� UI-������, �������������� ������ ���� ���� 
/// ��� ������������� ������-���� �������� �������. 
/// ��� ����� ���� ����� �������� ����, ����� � ����������� � ���������� ������, ����� Game over, � �.�.)
/// ������ ������ ������ ���� �������� �� UI-������ ������.
/// </summary>
public abstract class UIScreen : MonoBehaviour
{
    [SerializeField]
    [Tooltip("")]
    private ChildScreenButton[] childScreenButtons = null;

    [SerializeField]
    [Tooltip("")]
    private BackScreenButton backScreenButton = null;

    [SerializeField]
    [Tooltip("")]
    protected CanvasGroup buttonGroup = null;

    /// <summary>
    /// ������� �� ����� � ������ ������
    /// </summary>
    public bool IsActive => gameObject.activeSelf;

    /// <summary>
    /// ������������ �����, �� ������� ����� ��������� ������� �����
    /// �������� ������� ����
    /// </summary>
    public UIScreen ParentScreen { get; private set; } = null;

    protected UIEventMediator _uiEventMediator;
    protected GameSettings _gameSettings;

    public virtual void Init(GameSettings gameSettings, UIEventMediator uiEventMediator)
    {
        _uiEventMediator = uiEventMediator;
        _gameSettings = gameSettings;

        if (childScreenButtons.Length > 0)
        {
            foreach (var item in childScreenButtons)
            {
                item.Init(_uiEventMediator);
            }
        }

        if (backScreenButton != null)
        {
            backScreenButton.Init(_uiEventMediator);
        }     
    }

    /// <summary>
    /// ��������/������ �����
    /// </summary>
    /// <param name="active"></param>
    public virtual void SetActive(bool active)
    {
        if (active != IsActive)
        {
            gameObject.SetActive(active);
        }
        if (!active)
        {
            ParentScreen = null;
        }
    }
    
    /// <summary>
    /// ���������� ������������ �����
    /// </summary>
    /// <param name="parentScreen"></param>
    public void SetParent(UIScreen parentScreen)
    {
        ParentScreen = parentScreen;
    }

    protected void HideButtons()
    {
        buttonGroup.alpha = 0;
        buttonGroup.gameObject.SetActive(true);
    }

    protected void ShowButtonsSmooth()
    {
        buttonGroup.gameObject.SetActive(true);
        buttonGroup.DOFade(1, _gameSettings.UISettings.ButtonsFadeDuration);
    }
}
