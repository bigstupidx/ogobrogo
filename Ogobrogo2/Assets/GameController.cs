using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject Player;
	public GameObject StartText;
	public GameObject ScoreText;
	public GameObject TimerText;

	public int MaxSeconds = 60;
	public int ExtraTimeMultiplier = 2;
	public int AppleMultipler = 2;
	public int Score;
	public int BonusScore;
	public int TotalScore;

	private int appleCount = 0;
	private float remainingTime = 0;
	private float randomMultiplier = 1;

	private float currentTime = 0;
	private bool isTimerRunning = false;

	private int lastTime = 0;

	private OgoSceneManager sceneManager;

	public void AddApple()
	{
		appleCount++;
	}

	public void AddRemainingTime(float remainingTime)
	{
		this.remainingTime = remainingTime;
	}


	void Start()
	{
		sceneManager = FindObjectOfType<OgoSceneManager>();
		OnStart();
		isTimerRunning = true;
	}

	public void OnStart()
	{
		// show the starting text
		// start player interaction after 1-2 seconds
		// maybe need a "Go Bro" message? 
		ResetGame();
	}


	public void OnGameComplete()
	{
		// stop player interaction

		// apply ExtraTimeMultiplier * remaining time * random variance to score
		Score = appleCount * AppleMultipler;
		BonusScore = Mathf.CeilToInt(remainingTime * ExtraTimeMultiplier * randomMultiplier);
		TotalScore = Score + BonusScore;

		// show score in middle of screen for n seconds (
		// determine whether on leaderboard and show
		// if not on leader board show other end screen

		StartCoroutine(showScore());
	}

	private IEnumerator showScore()
	{
		yield return new WaitForSeconds(5);

		sceneManager.LoadScene("Success No Leaderboard Screen");
	}


	public void OnTimerComplete()
	{
		// stop player interaction
		// Show the time done screen

		Debug.Log("Timer Complete");

		sceneManager.LoadScene("Time Done Screen");
	}

	public void OnPlayerReset()
	{
		// show the hurry up text
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
}
