using UnityEngine;
using System.Collections;
using System.Timers;

public class PowerUpController : MonoBehaviour 
{
	public AudioSource audioSource;
	public Animator animator;
	public Rigidbody2D rigidBody;
	public double PowerUpMaxTime = 2000;
	private float startingGravity; 
	private Timer powerUpTimer;

	private Coroutine coroutine;

	void Start()
	{
		startingGravity = rigidBody.gravityScale;

		powerUpTimer = new Timer(); 
		powerUpTimer.Elapsed += new ElapsedEventHandler(onTimer); 
		powerUpTimer.Interval = PowerUpMaxTime; 
	}


	void onTimer(object source, ElapsedEventArgs e) 
	{ 
		animator.SetBool("PowerUp", false);
		rigidBody.gravityScale = startingGravity;
		powerUpTimer.Stop(); 
	}


	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.CompareTag("PowerUp"))
		{
			Destroy(other.gameObject);

			animator.SetBool("PowerUp", true);

			rigidBody.gravityScale = 3.5f;

			audioSource.Play();

			powerUpTimer.Start(); 
		}
	}

}
