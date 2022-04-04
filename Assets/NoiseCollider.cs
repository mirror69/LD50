using UnityEngine;
using UnityEngine.Events;

public class NoiseCollider : MonoBehaviour
{

    [SerializeField]
    private UnityEvent _onAlarm = new UnityEvent();
    public event UnityAction OnAlarm
    {
        add => _onAlarm.AddListener(value);
        remove => _onAlarm.RemoveListener(value);
    }




    private void OnMouseDown()
    {
        _onAlarm.Invoke();
    }
}
