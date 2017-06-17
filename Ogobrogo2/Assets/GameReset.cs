using UnityEngine;
using System.Collections;

public class GameReset : MonoBehaviour 
{
	private GameController gameController;

	void Start()
	{
		gameController = FindObjectOfType<GameController>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			gameController.SoftReset();
		}
	}
}
