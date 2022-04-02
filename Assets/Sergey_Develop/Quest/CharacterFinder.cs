using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFinder : MonoBehaviour
{
    [SerializeField] private GameObject selectionEffect;
    [SerializeField] private const string PlayerTag = "Player";


    private void Start()
    {
        selectionEffect.SetActive(false);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            selectionEffect.SetActive(true);
            Debug.Log($"�� ������� � �������� {gameObject.name}" +
                $"\n������� E ����� �����������������");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            selectionEffect.SetActive(false);
        }
    }
}
