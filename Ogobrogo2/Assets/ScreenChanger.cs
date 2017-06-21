using UnityEngine;
using System.Collections;

public class ScreenChanger : MonoBehaviour 
{

	public enum Screen
	{
		Start,
		Leaderboard,
		SuccessLeaderboard,
		SuccessNoLeaderboard,
		TimeDone,
		None
	}

	public GameObject StartScreen;
	public GameObject Leaderboard;
	public GameObject SuccessLeaderboard;
	public GameObject SuccessNoLeaderboard;
	public GameObject TimeDone;

	public int IdleTimeout = 10;

	private Screen currentScreen;

	// Idle timer
	private float currentTime = 0;
	private bool idleTimerRunning = false;

	private OgoSceneManager sceneManager;


	void Start()
	{
		sceneManager = FindObjectOfType<OgoSceneManager>();
		if(sceneManager.NextScreen != Screen.None)
		{
			SetScreen(sceneManager.NextScreen);
		}
		else
		{
			SetScreen(Screen.Start);
		}

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

				if(currentScreen == Screen.Start)
				{
					SetScreen(Screen.Leaderboard);
				}
				else
				{
					SetScreen(Screen.Start);
				}
			}

			if(Input.anyKey)
			{

				switch(currentScreen)
				{
				case Screen.SuccessLeaderboard:
					// Do nothing
					break;

				case Screen.SuccessNoLeaderboard:
					ResetIdleTimer();
					SetScreen(Screen.Leaderboard);
					break;
				
				case Screen.TimeDone:
					ResetIdleTimer();
					SetScreen(Screen.Leaderboard);
					break;

				default:
					StopIdleTimer();
					Fabric.EventManager.Instance.PostEvent("MUS/StartScreen", Fabric.EventAction.StopAll, null, gameObject);
					sceneManager.LoadScene("GameCharacterAndLayout", Screen.None);
					break;
				}
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

	public void SetScreen(Screen screen)
	{
		disableAllScreens();

		currentScreen = screen;

		switch(screen)
		{

		case Screen.Start:
			StartScreen.SetActive(true);
			break;

		case Screen.Leaderboard:
			Leaderboard.SetActive(true);
			break;

		case Screen.SuccessLeaderboard:
			currentTime = 30f;
			SuccessLeaderboard.SetActive(true);
			break;

		case Screen.SuccessNoLeaderboard:
			SuccessNoLeaderboard.SetActive(true);
			break;

		case Screen.TimeDone:
			TimeDone.SetActive(true);
			break;

		default:
			StartScreen.SetActive(true);
			break;
		}
	}

	private void disableAllScreens()
	{
		foreach(Transform trans in GetComponentInChildren<Transform>())
		{
			if(trans.gameObject != gameObject)
			{
				trans.gameObject.SetActive(false);
			}
		}
	}

}
