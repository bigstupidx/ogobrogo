using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour 
{

	public void LoadNextScene(int index)
	{
		SceneManager.LoadScene(index);
	}
}
