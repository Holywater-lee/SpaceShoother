using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
	public GameObject bullet;
	public Transform firePos;
	public AudioClip gunFireClip;

	public float accuracy = 5f;

	public float cooldown = 200f;
	float nextCooldown;

	AudioSource audioSource;
	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = gunFireClip;
	}

	private void Update()
	{
		Shot();
	}

	void Shot()
	{
		if (Input.GetMouseButton(0))
		{
			if (Time.time >= nextCooldown)
			{
				Fire();
				PlayFireSound();
				nextCooldown = Time.time + cooldown / 1000f;
			}
		}
	}

	float GetRandom(float a)
	{
		return Random.Range(-a, a);
	}

	void Fire()
	{
		GetComponentInChildren<Animator>().SetTrigger("Attack");
		Vector3 tempPos = firePos.forward * 100 + new Vector3(GetRandom(accuracy), GetRandom(accuracy), GetRandom(accuracy));
		Quaternion fakeLook = Quaternion.LookRotation(tempPos);
		Instantiate(bullet, firePos.position, fakeLook);
		nextCooldown = Time.time + cooldown / 1000f;
	}

	void PlayFireSound()
	{
		audioSource.Play();
	}
}
