using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderBasedButton : MonoBehaviour
{
    public event Action Pressed;

    private void OnMouseUp()
    {
        Pressed?.Invoke();
    }
}
