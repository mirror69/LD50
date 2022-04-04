using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections;

public class FigureChessKing : MonoBehaviour
{
    public static FigureChessKing Instance;
    public float TimeToShake => _timeToShake;




    //---------FIELDS
    [SerializeField]
    private Transform _parentTransform;

    [SerializeField, Min(0.1f)]
    private float _timeToShake = 3f;

    private SpriteRenderer _spriteRenderer;
    private bool _isChoose;
    private float _time;




    [SerializeField]
    private UnityEvent _onFigureChoose = new UnityEvent();
    public event UnityAction OnFigureChoose
    {
        add => _onFigureChoose.AddListener(value);
        remove => _onFigureChoose.RemoveListener(value);
    }

    [SerializeField]
    private UnityEvent _onMoveToPosition = new UnityEvent();
    public event UnityAction OnMoveToPosition
    {
        add => _onMoveToPosition.AddListener(value);
        remove => _onMoveToPosition.RemoveListener(value);
    }

    [SerializeField]
    private UnityEvent _onShake = new UnityEvent();
    public event UnityAction OnShake
    {
        add => _onShake.AddListener(value);
        remove => _onShake.RemoveListener(value);
    }




    //-------METHODS
    private void Awake()
    {
        if (Instance && Instance != this)
            Destroy(gameObject);

        Instance = this;
    }
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_isChoose)
            return;

        if(_time >= _timeToShake)
        {
            Shake();
            _isChoose = true;
            StartCoroutine(DelayForShake());
        }
    }

    private void OnMouseDown()
    {
        StartCoroutine(DelayForShake());
        transform.localScale = Vector3.one;
        _spriteRenderer.color = Color.yellow;
        transform.DOScale(1.3f, 1f).From();
        _onFigureChoose.Invoke();
        _isChoose = true;
    }

    public void MoveToPosition(Transform MoveToTrandsform)
    {
        StartCoroutine(DelayForShake());
        _onMoveToPosition.Invoke();
        _parentTransform.DOMove(MoveToTrandsform.position, 1f);
        _spriteRenderer.color = Color.white;
        _isChoose = true;
    }

    private void Shake()
    {
        transform.DOShakeScale(2, 0.1f, 90);
        _onShake.Invoke();
    }

    private IEnumerator DelayForShake()
    {
        yield return new WaitForSeconds(1);
        _isChoose = false;
        _time = 0;
    }

    public void StopNafigShake()
    {
        _isChoose = true;
    }
}
