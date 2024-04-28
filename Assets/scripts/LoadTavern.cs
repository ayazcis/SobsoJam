using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTavern : MonoBehaviour
{
    [SerializeField] GameObject tus;
    int isTriggered = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isTriggered = 1;
            tus.gameObject.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tus.gameObject.SetActive(false);
            isTriggered = 0;

        }
    }

    public void loadTavern ()
    {
        if(isTriggered == 1 && Input.GetKeyDown("e"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void Update()
    {
        loadTavern();
    }
}
