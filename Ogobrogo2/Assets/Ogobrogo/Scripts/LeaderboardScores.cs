using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeaderboardScores : MonoBehaviour 
{
	public Text leaderBoardText;
	private LeaderboardData data;

	void Start()
	{
		data = FindObjectOfType<LeaderboardData>();

		setLeaderboardText();
	}

	private void setLeaderboardText()
	{
		LeaderboardData.ScoreData[] scores = data.GetHighScores();

		string scoreText = string.Format("1ST {0} {1}\n2ND {2} {3}\n3RD {4} {5}" 
			, scores[0].Initials, string.Format("{0:0,0}", scores[0].Score)
			, scores[1].Initials, string.Format("{0:0,0}", scores[1].Score)
			, scores[2].Initials, string.Format("{0:0,0}", scores[2].Score)
		);
		leaderBoardText.text = scoreText;
	}


}
