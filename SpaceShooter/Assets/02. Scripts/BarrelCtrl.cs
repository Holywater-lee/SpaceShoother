using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
	int hitCount;
	bool isRemoved = false;
	void ExplosionBarrel()
	{
		isRemoved = true;
		GameObject fx_explosion = Instantiate(ParticleStorage.Instance.FindPrefabWithString("Explosion"), transform.position, Quaternion.identity);

		Collider[] colls = Physics.OverlapSphere(transform.position, 10f);
		foreach (Collider coll in colls)
		{
			Rigidbody rigid = coll.GetComponent<Rigidbody>();
			if (rigid != null)
			{
				rigid.mass = 1f;
				rigid.AddExplosionForce(1000f, transform.position, 300f);
			}
		}

		Destroy(fx_explosion, fx_explosion.GetComponent<ParticleSystem>().main.duration);
		Destroy(gameObject, 5f);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Bullet"))
		{
			Destroy(collision.gameObject);

			if (++hitCount >= 3 && !isRemoved)
			{
				ExplosionBarrel();
			}
		}
	}
}
