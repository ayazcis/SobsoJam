using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryClose : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Etkile�im etkile�im;
    void Update()
    {
        if (etkile�im.isTriggered == 1 && Input.GetKeyDown("e"))
        {
            canvas.SetActive(false);
        }
    }
}
