using UnityEngine;
using System.Collections;

public class SpeechGirl1Control : MonoBehaviour 
{

	public GameObject WaterSpeech;
	public GameObject RoidSpeech;

	public GameController GameController;

	void Start()
	{
		Reset();
	}

	public void Reset()
	{
		WaterSpeech.SetActive(false);
		RoidSpeech.SetActive(false);
	}

	public void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.CompareTag("Player"))
		{
			if(GameController.PowerUpControl.IsOnPowerUp)
			{
				RoidSpeech.SetActive(true);
			}
			else if(GameController.IsDead)
			{
				WaterSpeech.SetActive(true);
			}

		}
	}
}
