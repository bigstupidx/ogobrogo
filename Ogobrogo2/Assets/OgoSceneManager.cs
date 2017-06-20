using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OgoSceneManager : MonoBehaviour 
{
	public int IdleTimeout = 2;

	// Idle timer
	private float currentTime = 0;
	private bool idleTimerRunning = false;

	private string currentSceneName;

	void Awake()
	{
		GameObject.DontDestroyOnLoad(this);
	}

	void Start()
	{
		LoadScene("Start Screen");
		Fabric.EventManager.Instance.PostEvent("MUS/StartScreen");
		StartIdleTimer();
	}

	void Update()
	{
		if(idleTimerRunning)
		{
			currentTime -= Time.deltaTime;
			int tempTime = Mathf.CeilToInt(currentTime);

			if(tempTime <= 0)
			{		
				ResetIdleTimer();

				if(currentSceneName == "Start Screen")
				{
					LoadScene("Leaderboard");
				}
				else
				{
					LoadScene("Start Screen");
				}
			}

			if(Input.anyKey)
			{
				StopIdleTimer();
				Fabric.EventManager.Instance.PostEvent("MUS/StartScreen", Fabric.EventAction.StopAll, null, gameObject);
				LoadScene("GameCharacterAndLayout");

			}
		}
	}

	public void ResetIdleTimer()
	{
		currentTime = IdleTimeout; 
	}

	public void StartIdleTimer()
	{
		ResetIdleTimer();
		idleTimerRunning = true;
	}

	public void StopIdleTimer()
	{
		idleTimerRunning = false;
	}

	public void LoadScene(string sceneName, bool loadAdative = false)
	{
		LoadSceneMode sceneMode = loadAdative ? LoadSceneMode.Additive : LoadSceneMode.Single;
		SceneManager.LoadScene(sceneName, sceneMode); 
		currentSceneName = sceneName;
	}

}
