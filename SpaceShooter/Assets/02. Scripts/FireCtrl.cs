using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
	public Transform bullet;
	public Transform firePos;

	public float cooldown = 500f;
	float nextCooldown;

	private void Start()
	{
		
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (Time.time >= nextCooldown)
				Fire();
		}
	}

	void Fire()
	{
		Instantiate(bullet, firePos.position, firePos.rotation);
		nextCooldown = Time.time + cooldown / 1000f;
	}
}
