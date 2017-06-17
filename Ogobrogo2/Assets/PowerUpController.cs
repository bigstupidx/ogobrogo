﻿using UnityEngine;
using System.Collections;
using System.Timers;

public class PowerUpController : MonoBehaviour 
{
	public GameObject KillZone;
	public AudioSource audioSource;
	public Animator animator;
	public Rigidbody2D rigidBody;
	public float PowerUpMaxTime = 2f;

	private float currentTime = 0f;
	public bool IsOnPowerUp = false;

	private Coroutine coroutine;

	void Start()
	{
	}

	void Update()
	{
		if(IsOnPowerUp)
		{
			currentTime -= Time.deltaTime;
			if(currentTime <= 0f)
			{
				SetPowerUp(false);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.CompareTag("PowerUp"))
		{
			Destroy(other.gameObject);

			SetPowerUp(true);

			audioSource.Play();

			currentTime = PowerUpMaxTime;


		}
	}

	public void SetPowerUp(bool value)
	{
		animator.SetBool("PowerUp", value);
		IsOnPowerUp = value;
		KillZone.GetComponent<Collider2D>().isTrigger = !value;
	}

}
