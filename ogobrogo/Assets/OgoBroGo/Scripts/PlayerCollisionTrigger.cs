using UnityEngine;
using System.Collections;

public class PlayerCollisionTrigger : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.CompareTag("Collectable"))
		{
			Destroy(other.gameObject);
		}

	}

}
