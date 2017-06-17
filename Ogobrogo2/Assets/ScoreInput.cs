using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreInput : MonoBehaviour 
{
	public string NextScene;
	public InputField ScoreInputField;

	private LeaderboardData data;
	private OgoSceneManager sceneManager;

	void Start () 
	{
		data = FindObjectOfType<LeaderboardData>();
		sceneManager = FindObjectOfType<OgoSceneManager>();
	}

	public void OnDoneClicked()
	{
		data.SetHighScoreName(ScoreInputField.text);
		sceneManager.LoadScene(NextScene);
	}
}
