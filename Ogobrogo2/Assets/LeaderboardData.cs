using UnityEngine;
using System.Collections;

public class LeaderboardData : MonoBehaviour 
{
	public struct ScoreData
	{
		public string Initials;
		public int Score;

		public ScoreData(string initials, int score)
		{
			Initials = initials;
			Score = score;
		}
	}

	private ScoreData[] highScores;

	void Awake()
	{
		DontDestroyOnLoad(this);

		highScores = new ScoreData[3];

		// seed the scores
		highScores[0] = new ScoreData("KP", 1000);
		highScores[1] = new ScoreData("DM", 900);
		highScores[2] = new ScoreData("DL", 800);
	}

	// returns highscore index -1 is not found
	public int GetHighScoreindex(int score)
	{
		int i;
		for(i=highScores.Length-1; i >= -1; i--)
		{
			if(score < highScores[i].Score)
			{
				break;
			}
		}

		return i;
	}

	public void SetHighScore(int index, string initials, int score)
	{
		highScores[index].Initials = initials;
		highScores[index].Score = score;
	}

	public ScoreData[] GetHighScores()
	{
		return highScores;
	}
}
