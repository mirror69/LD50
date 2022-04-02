using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFinder : MonoBehaviour
{
    [SerializeField] private GameObject selectionEffect;
    [SerializeField] private const string PlayerTag = "Player";

    private bool characterInZone = false;

    private void Start()
    {
        selectionEffect.SetActive(false);    
    }

    private void Update()
    {
        if (characterInZone && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"Активировано событие предмета {name}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            characterInZone = true;
            selectionEffect.SetActive(true);
            //Debug.Log($"Вы подошли к предмету {gameObject.name}" +
            //    $"\nНажмите E чтобы взаимодействовать");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            characterInZone = false;
            selectionEffect.SetActive(false);
        }
    }
}
