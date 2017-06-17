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
			StartCoroutine(doReset());
		}
	}

	private IEnumerator doReset()
	{
		if(!gameController.PowerUpControl.IsOnPowerUp)
		{
			gameController.SetSadState(true);
			gameController.DisableControls();

			yield return new WaitForSeconds(2.5f);

			gameController.SoftReset();
		}

		yield break;
	}
}
