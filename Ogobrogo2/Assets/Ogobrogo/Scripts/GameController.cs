using UnityEngine;
using Fabric;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class GameController : MonoBehaviour 
{
	public GameObject Player;
	public GameObject StartText;
	public GameObject ScoreText;
	public GameObject TimerText;

	public GameObject FinalScoreText1;
	public GameObject FinalScoreText2;
	public GameObject FinalScoreText3;

	public GameObject LeapText;
	public Text AppleCountText;

	public int MaxSeconds = 60;
	public int ExtraTimeMultiplier = 2;
	public int AppleMultipler = 2;
	public int Score;
	public int BonusScore;
	public int TotalScore;

	public bool IsDead = false;

	private int appleCount = 0;
	private float remainingTime = 0;
	private float randomMultiplier = 1;

	private float currentTime = 0;
	private bool isTimerRunning = false;

	private int lastTime = 0;

	private OgoSceneManager sceneManager;
	private LeaderboardData data;
	private Platformer2DUserControl userControl;
	public PowerUpController PowerUpControl;

	private Vector2 spawnPoint;

	public void AddApple()
	{
		appleCount++;
		Score = appleCount * AppleMultipler;
		AppleCountText.text = Score.ToString();
		Fabric.EventManager.Instance.PostEvent("SFX/AppleCollect");
	}

	void Start()
	{
		data = FindObjectOfType<LeaderboardData>();
		sceneManager = FindObjectOfType<OgoSceneManager>();
		userControl = Player.GetComponent<Platformer2DUserControl>();
		PowerUpControl = Player.GetComponent<PowerUpController>();

		spawnPoint = Player.transform.position;

		DisableControls();
		isTimerRunning = false;

		LeapText.SetActive(false);

		TimerText.GetComponentInChildren<Text>().text = "01:00";

		Fabric.EventManager.Instance.PostEvent("MUS/StartScreen", Fabric.EventAction.StopAll, null, gameObject);
		Fabric.EventManager.Instance.PostEvent("MUS/GameWin", Fabric.EventAction.StopAll, null, gameObject);
		Fabric.EventManager.Instance.PostEvent("MUS/Gameplay");

		showFinalScore(false);
		StartCoroutine(OnStart());
	}

	public IEnumerator OnStart()
	{
		yield return new WaitForSeconds(5);

		EnableControls();
		StartText.SetActive(false);
		isTimerRunning = true;

		ResetGame();
	}


	public void OnGameComplete()
	{
		// stop player interaction
		isTimerRunning = false; 

		DisableControls();

		remainingTime = Mathf.Ceil(currentTime);

		// apply ExtraTimeMultiplier * remaining time * random variance to score
		Score = appleCount * AppleMultipler;
		BonusScore = Mathf.CeilToInt(remainingTime * ExtraTimeMultiplier * randomMultiplier);
		TotalScore = Score + BonusScore;

		StartCoroutine(showScore());
	}

	private IEnumerator showScore()
	{
		showFinalScore(true);
		int index = data.GetHighScoreindex(TotalScore);

		yield return new WaitForSeconds(4);

		showFinalScore(false);

		if(index > -1)
		{
			data.SetCurrentHighScore(TotalScore);
			sceneManager.LoadScene("All Screens", ScreenChanger.Screen.SuccessLeaderboard);
		}
		else
		{
			sceneManager.LoadScene("All Screens", ScreenChanger.Screen.SuccessNoLeaderboard);
		}
	}

	public void OnTimerComplete()
	{
		Fabric.EventManager.Instance.PostEvent("SFX/StopAll", Fabric.EventAction.StopAll, null, gameObject);
		Fabric.EventManager.Instance.PostEvent("MUS/Gameplay", Fabric.EventAction.StopAll, null, gameObject);
		Fabric.EventManager.Instance.PostEvent("SFX/GameLose", Fabric.EventAction.PlaySound, null, gameObject);
		sceneManager.LoadScene("All Screens", ScreenChanger.Screen.TimeDone);
	}

	void Update()
	{
		if(isTimerRunning)
		{
			currentTime -= Time.deltaTime;
			int tempTime = Mathf.CeilToInt(currentTime);

			if(tempTime == MaxSeconds)
			{
				TimerText.GetComponentInChildren<Text>().text = "01:00";
			}

			if(tempTime < lastTime)
			{
				string leadingZeros = "00 : ";

				if(tempTime < 10)
				{
					leadingZeros = "00 : 0";
				}
				TimerText.GetComponentInChildren<Text>().text = leadingZeros+tempTime.ToString();
				lastTime = tempTime;

				if (tempTime <= 5) 
				{
					Fabric.EventManager.Instance.PostEvent ("SFX/Timer");
				}

			}

			if(currentTime <= 0)
			{
				currentTime = 0;
				isTimerRunning = false;
				OnTimerComplete();
			}
		}
	}


	private void ResetGame()
	{
		Score = 0;
		BonusScore = 0;
		TotalScore = 0;
		appleCount = 0;
		remainingTime = 0;
		currentTime = MaxSeconds;
		lastTime = MaxSeconds;
		randomMultiplier = UnityEngine.Random.Range(1f,3f);

		ScoreText.GetComponentInChildren<Text>().text = Score.ToString();
		TimerText.GetComponentInChildren<Text>().text = "01:00";
	} 

	public void SoftReset()
	{
		if(!PowerUpControl.IsOnPowerUp)
		{
			SetSadState(false);
			Player.transform.position = spawnPoint;
			PowerUpControl.SetPowerUp(false);
			EnableControls();
			IsDead = false;
			Fabric.EventManager.Instance.PostEvent("MUS/Gameplay");
		}
	}

	private void showFinalScore(bool show)
	{
		FinalScoreText1.SetActive (show);
		FinalScoreText2.SetActive (show);
		FinalScoreText3.SetActive (show);

		FinalScoreText1.GetComponentInChildren<Text> ().text = "SCORE : " + Score.ToString ();
		FinalScoreText2.GetComponentInChildren<Text> ().text = "BRONUS : " + BonusScore.ToString ();
		FinalScoreText3.GetComponentInChildren<Text> ().text = "TOTAL : " + TotalScore.ToString ();
	
		if(show)
		{
			Fabric.EventManager.Instance.PostEvent("MUS/Gameplay", Fabric.EventAction.StopAll, null, gameObject);
			Fabric.EventManager.Instance.PostEvent("MUS/GameWin");
		}
	}

	public void ShowLeapText()
	{
		StartCoroutine(showLeapText());
	}

	private IEnumerator showLeapText()
	{
		LeapText.SetActive(true);

		yield return new WaitForSeconds(3);

		LeapText.SetActive(false);

	}

	public void DisableControls()
	{
		userControl.enabled = false;
	}

	public void EnableControls()
	{
		userControl.enabled = true;
	}

	public void PlayerDead()
	{
		SetSadState(true);
		IsDead = true;

	}

	public void SetSadState(bool value)
	{
		Player.GetComponent<Animator>().SetBool("Sad", value);
	}
}
