using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etkileşim : MonoBehaviour
{
    [SerializeField] public GameObject tus;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tus.gameObject.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tus.gameObject.SetActive(false);

        }
    }
}
