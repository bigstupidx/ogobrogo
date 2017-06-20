using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour 
{
	public string NextScene;
	private OgoSceneManager sceneManager;

	void Start()
	{
		sceneManager = FindObjectOfType<OgoSceneManager>();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			sceneManager.LoadScene(NextScene);
			sceneManager.StartIdleTimer();
		}
	}
}
