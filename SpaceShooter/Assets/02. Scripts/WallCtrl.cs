using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCtrl : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Bullet"))
		{
			int t_fxNum;
			ParticleStorage.Instance.particlePrefabsDic.TryGetValue("Spark", out t_fxNum);
			var fx_spark = Instantiate(ParticleStorage.Instance.particlePrefabs[t_fxNum], collision.transform.position, Quaternion.identity);

			Destroy(fx_spark, fx_spark.GetComponent<ParticleSystem>().main.duration + 0.2f);
			Destroy(collision.gameObject);
		}
	}
}
