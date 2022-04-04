using UnityEngine;

public class ChessCell : MonoBehaviour
{
    private void OnMouseDown()
    {
        FigureChessKing.Instance.MoveToPosition(transform);
    }
}
