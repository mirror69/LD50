using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite lampOn;
    [SerializeField] private Sprite lampOff;
    [SerializeField] private GameObject lightObject;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Sprite currentSprite;

    private void Start()
    {
        currentSprite = lampOff;
        lightObject.SetActive(false);
        spriteRenderer.sprite = currentSprite;
    }

    public void ChangeSprite ()
    {
        if (currentSprite == lampOn)
            currentSprite = lampOff;
        else
            currentSprite = lampOn;

        spriteRenderer.sprite = currentSprite;
        lightObject.SetActive(currentSprite == lampOn);
    }
}
