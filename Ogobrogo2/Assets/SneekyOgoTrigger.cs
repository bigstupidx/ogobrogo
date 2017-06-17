using UnityEngine;
using System.Collections;

public class SneekyOgoTrigger : MonoBehaviour 
{
	public Animator animator;

	private void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.name == "Player")
		{
			animator.SetTrigger("OgoStart");
		}
	}
}
