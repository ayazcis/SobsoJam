using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int water = 100 , food =  100 , gold = 30;

    //bu elemaný yok etme pls
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

}
