﻿using UnityEngine;
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
	private LeaderboardData data;
	private Platformer2DUserControl userControl;

	private Vector2 spawnPoint;

	public void AddApple()
	{
		appleCount++;
	}

	void Start()
	{
		data = FindObjectOfType<LeaderboardData>();
		sceneManager = FindObjectOfType<OgoSceneManager>();
		userControl = Player.GetComponent<Platformer2DUserControl>();

		spawnPoint = Player.transform.position;

		userControl.enabled = false;
		isTimerRunning = false;

		TimerText.GetComponentInChildren<Text>().text = "01:00";

		showFinalScore(false);
		StartCoroutine(OnStart());
	}

	public IEnumerator OnStart()
	{
		yield return new WaitForSeconds(5);

		userControl.enabled = true;
		StartText.SetActive(false);
		isTimerRunning = true;

		ResetGame();
	}


	public void OnGameComplete()
	{
		// stop player interaction
		isTimerRunning = false; 

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
			sceneManager.LoadScene("Success Leaderboard Screen");
		}
		else
		{
			sceneManager.LoadScene("Success No Leaderboard Screen");
			sceneManager.StartIdleTimer();
		}
	}

	public void OnTimerComplete()
	{
		sceneManager.LoadScene("Time Done Screen");
		sceneManager.StartIdleTimer();
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

	public void SoftReset()
	{
		Player.transform.position = spawnPoint;
	}

	private void showFinalScore(bool show)
	{
		FinalScoreText1.SetActive(show);
		FinalScoreText2.SetActive(show);
		FinalScoreText3.SetActive(show);

		FinalScoreText1.GetComponentInChildren<Text>().text = "SCORE : "+Score.ToString();
		FinalScoreText2.GetComponentInChildren<Text>().text = "BRONUS : "+BonusScore.ToString();
		FinalScoreText3.GetComponentInChildren<Text>().text = "TOTAL : "+TotalScore.ToString();
	}
}
