using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScoreInput : MonoBehaviour 
{
	public ScreenChanger.Screen NextScreen;
	public InputField ScoreInputField;

	private LeaderboardData data;
	private OgoSceneManager sceneManager;

	void Start () 
	{
		data = FindObjectOfType<LeaderboardData>();
		sceneManager = FindObjectOfType<OgoSceneManager>();

		ScoreInputField.ActivateInputField();
	}

	public void Update()
	{
		if(Input.GetKey(KeyCode.Return) || Input.GetButtonDown("Fire1"))
		{
			OnDoneClicked();
		}
	}

	public void OnDoneClicked()
	{
		if(ScoreInputField.text.Length == 2)
		{
			data.SetHighScoreName(ScoreInputField.text);
			sceneManager.LoadScene("All Screens", NextScreen);
		}
	}
}
