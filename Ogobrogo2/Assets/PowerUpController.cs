using UnityEngine;
using System.Collections;
using System.Timers;

public class PowerUpController : MonoBehaviour 
{
	public AudioSource audioSource;
	public Animator animator;
	public Rigidbody2D rigidBody;
	public float PowerUpMaxTime = 2f;

	//private float startingGravity; 
	//private float startingMass;

	private float currentTime = 0f;
	public bool IsOnPowerUp = false;

	private Coroutine coroutine;

	public RigidBodyToggler[] RigidBodyTogglers;

	void Start()
	{
		//startingGravity = rigidBody.gravityScale;
		//startingMass = rigidBody.mass;

		Physics2D.IgnoreLayerCollision(8,9);
	}

	void Update()
	{
		if(IsOnPowerUp)
		{
			currentTime -= Time.deltaTime;
			if(currentTime <= 0f)
			{
				animator.SetBool("PowerUp", false);
				//rigidBody.gravityScale = startingGravity;
				//rigidBody.mass = startingMass;

				IsOnPowerUp = false;

				//toggleRigidBodies();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.CompareTag("PowerUp"))
		{
			Destroy(other.gameObject);

			animator.SetBool("PowerUp", true);

			//rigidBody.gravityScale = 0.05f;
			//rigidBody.mass = 5000;

			audioSource.Play();

			currentTime = PowerUpMaxTime;

			IsOnPowerUp = true;

			//toggleRigidBodies();
		}
	}

	/*private void toggleRigidBodies()
	{
		for(int i=0; i < RigidBodyTogglers.Length; i++)
		{
			RigidBodyTogglers[i].PowerUp = IsOnPowerUp;
		}
	}*/

}
