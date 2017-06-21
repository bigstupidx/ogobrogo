using UnityEngine;
using System.Collections;

public class ToggleColliders : MonoBehaviour 
{
	public bool CollidersOn = true;


	public void OnToggleColliders(bool value)
	{
		CollidersOn = value;

		Collider2D[] colliders = GetComponentsInChildren<Collider2D>();

		for(int i=0; i < colliders.Length; i++)
		{
			colliders[i].enabled = CollidersOn;
		}

	}
}
