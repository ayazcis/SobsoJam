using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etkileşim : MonoBehaviour
{
    [SerializeField] public GameObject tus;
    [SerializeField] public GameObject diyalog;
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

    void CanvasOpen()
    {
        if(isTriggered == 1 && Input.GetKeyDown("e"))
        {
			tus.gameObject.SetActive(false);
			diyalog.SetActive(true);
            Time.timeScale = 0;
        }

    }

    private void Update()
    {
        CanvasOpen();
    }

    public void TimeScaleToNormal()
    {
        Time.timeScale = 1;
    }
}
