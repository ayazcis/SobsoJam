using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryClose : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Etkileþim etkileþim;
    void Update()
    {
        if (etkileþim.isTriggered == 1 && Input.GetKeyDown("e"))
        {
            canvas.SetActive(false);
        }
    }
}
