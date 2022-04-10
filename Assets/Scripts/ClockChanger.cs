using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockChanger : MonoBehaviour
{
    [SerializeField]
    private Sprite[] clockSprites;

    [SerializeField]
    private GameScreen[] miniGameScreens;

    private SpriteRenderer spriteRenderer;
    private static int spriteIndex = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = clockSprites[spriteIndex];
    }

    private void Start()
    {
        foreach (var screen in miniGameScreens)
        {
            screen.CloseRequested += ChangeSprite;
        }
    }

    private void ChangeSprite (GameScreenResult gameScreenResult)
    {
        if (gameScreenResult == GameScreenResult.WinGame)
        {
            spriteIndex++;
            try
            {
                spriteRenderer.sprite = clockSprites[spriteIndex];
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}
