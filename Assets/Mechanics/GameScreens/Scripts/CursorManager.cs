using UnityEngine;

public class CursorManager
{
    public static CursorManager Instance;
    private Texture2D _cursorCommon;
    private Texture2D _cursorHighlight;

    public void Init(Texture2D cursorCommon, Texture2D cursorHighlight)
    {
        _cursorHighlight = cursorHighlight;
        _cursorCommon = cursorCommon;
        Instance = this;
        SetCursorHighlight(false);
    }

    public void SetCursorHighlight(bool highlight)
    {
        if(highlight)
            Cursor.SetCursor(_cursorHighlight, new Vector2(_cursorCommon.width/2f, _cursorCommon.height/2f), CursorMode.Auto);
        else
            Cursor.SetCursor(_cursorCommon, new Vector2(_cursorCommon.width/2f, _cursorCommon.height/2f), CursorMode.Auto);
    }
}