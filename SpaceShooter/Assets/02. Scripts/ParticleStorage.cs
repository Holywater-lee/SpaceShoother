using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleStorage : MonoBehaviour
{
	public GameObject[] particlePrefabs;

	static ParticleStorage sm_instance;

	public Dictionary<string, int> particlePrefabsDic = new Dictionary<string, int>()
	{
		{"Spark", 0},
		{"Explosion", 1}
	};
	private void Awake()
	{
		if (sm_instance == null) sm_instance = this;
	}
	public static ParticleStorage Instance
	{
		get
		{
			if (sm_instance == null)
			{
				return null;
			}

			return sm_instance;
		}
	}
}