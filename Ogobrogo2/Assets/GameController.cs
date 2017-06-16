using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public GameObject Player;
	public int ExtraTimeMultiplier = 2;
	public int AppleMultipler = 2;

	public int Score;
	public int BonusScore;
	public int TotalScore;

	private int appleCount = 0;
	private float remainingTime = 0;
	private float randomMultiplier = 1;

	public void AddApple()
	{
		appleCount++;
	}

	public void AddRemainingTime(float remainingTime)
	{
		this.remainingTime = remainingTime;
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
	}


	public void OnTimerComplete()
	{
		// stop player interaction
		// Show the time done screen
	}

	public void OnPlayerReset()
	{
		// show the hurry up text
	}


	private void ResetGame()
	{
		Score = 0;
		BonusScore = 0;
		TotalScore = 0;
		appleCount = 0;
		remainingTime = 0;
		randomMultiplier = UnityEngine.Random.Range(1f,3f);
	} 
}
