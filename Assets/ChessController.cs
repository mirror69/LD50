using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using SimpleMan.CoroutineExtensions;

public class ChessController : MonoBehaviour
{
    [SerializeField]
    private AudioClip chessMemory;

    //---------FIELDS
    [SerializeField, Min(0.1f)]
    private float _enemyFirstMoveDelay = 1.5f;

    [SerializeField, Min(0.1f)]
    private float _enemySecondMoveDelay = 0.7f;

    [SerializeField, Min(0.1f)]
    private float _figureMoveTime = 1f;

    [SerializeField, Min(0.1f)]
    private float _afterLastTurn;
    [SerializeField, Min(0.1f)]
    private float _afterVoiceDelay;


    [SerializeField]
    private Transform _enemyRook;

    [SerializeField]
    private Transform[] _allyKingPositions;

    [SerializeField]
    private GameObject[] _allyKingPositionsAllarm;

    [SerializeField]
    private Transform[] _enemyRookPositions;

    private bool _isFirstMove = true;
    private AudioSource audio;




    [SerializeField]
    private UnityEvent _onFinish = new UnityEvent();
    public event UnityAction OnFinish
    {
        add => _onFinish.AddListener(value);
        remove => _onFinish.RemoveListener(value);
    }

    //-------METHODS
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        FigureChessKing.Instance.OnFigureChoose += OnFigureChoose;
        FigureChessKing.Instance.OnMoveToPosition += OnMoveToPosition;
    }

    private void OnMoveToPosition()
    {
        if (_isFirstMove)
        {
            this.Delay(_enemyFirstMoveDelay, () => _enemyRook.transform.DOMove(_enemyRookPositions[0].position, _figureMoveTime));
            _allyKingPositions[0].gameObject.SetActive(false);
            _allyKingPositionsAllarm[0].SetActive(false);
            _allyKingPositionsAllarm[1].SetActive(true);
            StartCoroutine(DelayOpenNextTurnForKing());
            _isFirstMove = false;
        }
        else
        {
            this.Delay(_enemySecondMoveDelay, () => _enemyRook.transform.DOMove(_enemyRookPositions[1].position, _figureMoveTime));
            _allyKingPositions[1].gameObject.SetActive(false);
            this.Delay(FigureChessKing.Instance.TimeToShake * 0.7f, ()=> FigureChessKing.Instance.StopNafigShake());

            this.Delay(_afterLastTurn, () => audio.PlayOneShot(chessMemory));
            this.Delay(_afterVoiceDelay, () => _onFinish.Invoke());
        }
    }

    private void OnFigureChoose()
    {
        if(_isFirstMove)
            _allyKingPositions[0].gameObject.SetActive(true);
    }

    private IEnumerator DelayOpenNextTurnForKing()
    {
        yield return new WaitForSeconds(1);
        _allyKingPositions[1].gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        FigureChessKing.Instance.OnFigureChoose -= OnFigureChoose;
        FigureChessKing.Instance.OnMoveToPosition -= OnMoveToPosition;
    }
}
