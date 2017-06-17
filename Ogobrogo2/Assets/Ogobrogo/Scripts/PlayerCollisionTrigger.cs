using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCollisionTrigger : MonoBehaviour 
{
	public GameController gameController;
	public Text AppleCount;
	private int appleCount = 0;

	void Start()
	{
 		AppleCount.text = appleCount.ToString();
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.CompareTag("Collectable"))
		{
			Destroy(other.gameObject);
		}

		if(other.gameObject.CompareTag("EndOgo"))
		{
			gameController.OnGameComplete();
		}

	}

}
