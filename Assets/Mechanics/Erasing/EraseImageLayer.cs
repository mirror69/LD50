using DG.Tweening;
using System;
using UnityEngine;

public class EraseImageLayer : MonoBehaviour
{
    public static event Action<EraseImageLayer> OnEraseImageEnded;
    public static float PercentToWin;

    private Texture2D m_Texture;
    private Color[] m_Colors;
    private BoxCollider2D imageCollider;
    SpriteRenderer spriteRend;
    Color zeroAlpha = Color.clear;
    public int erSize;
    public Vector2Int lastPos;
    public bool Drawing = false;
    public bool isReady = false;

    private int colorPixelsCount=0;
    private int allPixelsCount;

    private Camera mainCamera;

    void Start()
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        var tex = spriteRend.sprite.texture;
        allPixelsCount = tex.width * tex.height;

        m_Texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        m_Texture.filterMode = FilterMode.Bilinear;
        m_Texture.wrapMode = TextureWrapMode.Clamp;
        m_Colors = tex.GetPixels();
        m_Texture.SetPixels(m_Colors);
        m_Texture.Apply();
        spriteRend.sprite = Sprite.Create(m_Texture, spriteRend.sprite.rect, new Vector2(0.5f, 0.5f));

        imageCollider = GetComponent<BoxCollider2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isReady)
            return;

        float dif = (float)colorPixelsCount / (float)allPixelsCount * 100f;
        if (dif>PercentToWin)
        {
            isReady = true;
            imageCollider.enabled = false;
            spriteRend.DOFade(0, 1);
            OnEraseImageEnded?.Invoke(this);
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (imageCollider.OverlapPoint(mousePoint))
            {
                UpdateTexture(mousePoint);
                Drawing = true;
            }
        }
        else
        {
            Drawing = false;
        }
    }

    public void UpdateTexture(Vector2 point)
    {
        int w = m_Texture.width;
        int h = m_Texture.height;
        var mousePos = point - (Vector2)imageCollider.bounds.min;
        mousePos.x *= w / imageCollider.bounds.size.x;
        mousePos.y *= h / imageCollider.bounds.size.y;
        Vector2Int p = new Vector2Int((int)mousePos.x, (int)mousePos.y);
        Vector2Int start = new Vector2Int();
        Vector2Int end = new Vector2Int();
        if (!Drawing)
            lastPos = p;
        start.x = Mathf.Clamp(Mathf.Min(p.x, lastPos.x) - erSize, 0, w);
        start.y = Mathf.Clamp(Mathf.Min(p.y, lastPos.y) - erSize, 0, h);
        end.x = Mathf.Clamp(Mathf.Max(p.x, lastPos.x) + erSize, 0, w);
        end.y = Mathf.Clamp(Mathf.Max(p.y, lastPos.y) + erSize, 0, h);
        Vector2 dir = p - lastPos;
        for (int x = start.x; x < end.x; x++)
        {
            for (int y = start.y; y < end.y; y++)
            {
                Vector2 pixel = new Vector2(x, y);
                Vector2 linePos = p;
                if (Drawing)
                {
                    float d = Vector2.Dot(pixel - lastPos, dir) / dir.sqrMagnitude;
                    d = Mathf.Clamp01(d);
                    linePos = Vector2.Lerp(lastPos, p, d);
                }
                if ((pixel - linePos).sqrMagnitude <= erSize * erSize)
                {
                    if (m_Colors[x + y * w] != zeroAlpha)
                    {
                        colorPixelsCount++;
                        m_Colors[x + y * w] = zeroAlpha;
                    }
                }
            }
        }
        lastPos = p;
        m_Texture.SetPixels(m_Colors);
        m_Texture.Apply();
        spriteRend.sprite = Sprite.Create(m_Texture, spriteRend.sprite.rect, new Vector2(0.5f, 0.5f));
    }
}