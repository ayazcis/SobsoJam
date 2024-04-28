using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossButtons : MonoBehaviour
{
    public GameObject altin;
    public Canvas dialogCan;
    public BossEnemy bossEnemy;
    public void AltinYok()
    {
        Debug.Log("altii");
        altin.SetActive(true);
    }
    public void dovus()
    {
        bossEnemy.talked = true;
        dialogCan.enabled = false;
    }
}
