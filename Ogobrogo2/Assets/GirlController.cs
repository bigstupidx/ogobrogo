using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class GirlController : MonoBehaviour 
{

	public PlatformerCharacter2D Dude;


	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Girl"))
		{
			Dude.SpeedPenalty = 4;
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
