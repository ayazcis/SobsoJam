using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class videoEndedStart : MonoBehaviour
{
	public VideoPlayer videoPlayer;


	void Start()
	{
		// VideoPlayer bile�eninde "loopPointReached" event'ini dinleyerek video biti�ini alg�la
		videoPlayer.loopPointReached += OnVideoFinished;
	}

	void OnVideoFinished(VideoPlayer vp)
	{
		// Video bitti�inde sahne y�kleme i�lemini ger�ekle�tir
		SceneManager.LoadScene(2);
	}
}

