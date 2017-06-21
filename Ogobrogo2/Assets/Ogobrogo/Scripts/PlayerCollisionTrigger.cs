using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCollisionTrigger : MonoBehaviour 
{
	public GameController gameController;

	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.CompareTag("Collectable"))
		{
			Destroy(other.gameObject);
			gameController.AddApple();
		}

		if(other.gameObject.CompareTag("EndOgo"))
		{
			GetComponent<Animator>().SetFloat("Speed", 0f);
			gameController.OnGameComplete();
		}

		if(other.gameObject.CompareTag("LeapTrigger"))
		{
			gameController.ShowLeapText();
		}

	}

}
