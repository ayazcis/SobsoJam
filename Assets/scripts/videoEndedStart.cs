using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class videoEndedStart : MonoBehaviour
{
	public VideoPlayer videoPlayer;


	void Start()
	{
		// VideoPlayer bileþeninde "loopPointReached" event'ini dinleyerek video bitiþini algýla
		videoPlayer.loopPointReached += OnVideoFinished;
	}

	void OnVideoFinished(VideoPlayer vp)
	{
		// Video bittiðinde sahne yükleme iþlemini gerçekleþtir
		SceneManager.LoadScene(2);
	}
}

