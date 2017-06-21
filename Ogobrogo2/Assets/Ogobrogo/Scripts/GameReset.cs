using UnityEngine;
using Fabric;
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
			Fabric.EventManager.Instance.PostEvent("SFX/StopAll", Fabric.EventAction.StopAll, null, gameObject);
			Fabric.EventManager.Instance.PostEvent("SFX/Die", Fabric.EventAction.PlaySound, null, gameObject);
			Fabric.EventManager.Instance.PostEvent("MUS/Gameplay", Fabric.EventAction.StopAll, null, gameObject);
			Fabric.EventManager.Instance.PostEvent("SFX/GameLoseDelayed", Fabric.EventAction.PlaySound, null, gameObject);

			yield return new WaitForSeconds(2.5f);

			gameController.SoftReset();
		}

		yield break;
	}
}
