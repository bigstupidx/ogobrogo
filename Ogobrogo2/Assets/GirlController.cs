using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class GirlController : MonoBehaviour 
{

	public PlatformerCharacter2D Dude;
	public float SpeedPenalty = 4.5f;


	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Girl"))
		{
			Dude.SpeedPenalty = SpeedPenalty;
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Girl"))
		{
			Dude.SpeedPenalty = 1;
		}

	}
}
