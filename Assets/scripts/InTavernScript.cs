using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTavernScript : MonoBehaviour
{
    public GameObject canvas;
	[SerializeField] public GameObject E;

	void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            canvas.SetActive(true);
            E.SetActive(false);
        }
    }
}
