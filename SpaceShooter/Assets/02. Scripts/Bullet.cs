using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int damage = 20;
	public float speed = 100f;

	void Start()
	{
		GetComponent<Rigidbody>().AddForce(speed * transform.forward, ForceMode.Impulse);
		Destroy(gameObject, 3f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}
	}
}
