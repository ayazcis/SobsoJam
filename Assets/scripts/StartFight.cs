using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFight : MonoBehaviour
{
    public void startFight()
    {
        SceneManager.LoadScene(0);
    }
}
