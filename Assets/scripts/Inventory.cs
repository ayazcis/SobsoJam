using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public int water = 100 , food =  100 , gold = 30;

    //bu elemaný yok etme pls
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
	private void Update()
	{
		if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            water = 100;
            food = 100;
            gold = 30;
        }
	}
}
