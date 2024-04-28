using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etkileşim : MonoBehaviour
{
    [SerializeField] public GameObject tus;
    [SerializeField] public GameObject diyalog;
    [SerializeField] public GameObject[] diyaloglar;
    int currentDialog = 0;
    public int isTriggered = 0;
    int isStarted = 0;

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
            isStarted = 1;
        }

    }

    private void Update()
    {
        CanvasOpen();
        TalkWithNpc();
    }

    public void TimeScaleToNormal()
    {
        Time.timeScale = 1;
    }

    public void TalkWithNpc()
    {

        if(isStarted == 1 && Input.GetMouseButtonDown(0))
        {
            if(currentDialog + 1 < diyaloglar.Length)
            {
                diyaloglar[currentDialog].SetActive(false);
                currentDialog++;
                diyaloglar[currentDialog].SetActive(true);
            }
            else
            {

            }
        }
    }
}
