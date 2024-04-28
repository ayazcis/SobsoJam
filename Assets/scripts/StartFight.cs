using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFight : MonoBehaviour
{
    public void startFight()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
