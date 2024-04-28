using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BossButtons : MonoBehaviour
{
    public GameObject altin;
    public Canvas dialogCan;
    [SerializeField] public BossEnemy bossEnemy;
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
	public void start()
	{
        SceneManager.LoadScene(1);
	}
    public void dovusYukle()
    {
        SceneManager.LoadScene(6);
    }
	public void banditGel()
	{
		SceneManager.LoadScene(3);
	}

    public void ExitGame()
    {
        #if UNITY_EDITOR
            // Unity Editörde ise oyunu durdur
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                // Unity Editörde deðilse oyunu kapat
                Application.Quit();
        #endif
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
