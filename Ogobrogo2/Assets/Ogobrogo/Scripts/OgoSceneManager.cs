using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OgoSceneManager : MonoBehaviour 
{

	public ScreenChanger.Screen NextScreen;

	void Awake()
	{
		GameObject.DontDestroyOnLoad(this);
	}

	void Start()
	{
		LoadScene("All Screens", ScreenChanger.Screen.Start);
		//Fabric.EventManager.Instance.PostEvent("MUS/StartScreen");
	}



	public void LoadScene(string sceneName, ScreenChanger.Screen screen)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single); 
		NextScreen = screen;
	}

}
