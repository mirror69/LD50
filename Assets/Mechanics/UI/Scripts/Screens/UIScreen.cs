using System.Collections;
using System.Collections.Generic;
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

    public virtual void Init(UIEventMediator uiEventMediator)
    {
        _uiEventMediator = uiEventMediator;

        if (childScreenButtons.Length > 0)
        {
            foreach (var item in childScreenButtons)
            {
                item.Init(_uiEventMediator);
            }
        }

        backScreenButton?.Init(_uiEventMediator);
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
}
