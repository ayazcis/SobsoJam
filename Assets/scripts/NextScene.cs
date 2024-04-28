using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    int current = SceneManager.GetActiveScene().buildIndex;
    void Start()
    {
        SceneManager.LoadScene(current + 1);
    }
}
