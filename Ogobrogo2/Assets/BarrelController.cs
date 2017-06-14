using UnityEngine;
using System.Collections;

public class BarrelController : MonoBehaviour 
{
	public PowerUpController powerUpCtrl;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Barrel"))
		{
			if(!powerUpCtrl.IsOnPowerUp)
			{
				//GetComponent<Rigidbody2D>().mass = 5000;
			}
		}

	}
}
