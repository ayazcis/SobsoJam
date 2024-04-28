using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tavern : MonoBehaviour
{
	[SerializeField] public GameObject E;
	[SerializeField] public GameObject diyalog;
	int isTriggered = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			isTriggered = 1;
			E.gameObject.SetActive(true);

		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			E.gameObject.SetActive(false);
			isTriggered = 0;

		}
	}

	void InTavern()
	{
		if (isTriggered == 1 && Input.GetKeyDown("e"))
		{
			SceneManager.LoadScene(3);
		}

	}

	private void Update()
	{
		InTavern();
	}

	public void TimeScaleToNormal()
	{
		Time.timeScale = 1;
	}
}
