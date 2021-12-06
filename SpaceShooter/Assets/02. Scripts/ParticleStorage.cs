using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticleStorage : MonoBehaviour
{
	[SerializeField]
	private GameObject[] particlePrefabs;

	static ParticleStorage sm_instance;

	private Dictionary<string, int> particlePrefabsDic = new Dictionary<string, int>()
	{
		{"Spark", 0},
		{"Explosion", 1},
		{"BloodEffect", 2},
		{"BloodDecal", 3}
	};

	public GameObject FindPrefabWithString(string name)
	{
		int tempInt;
		particlePrefabsDic.TryGetValue(name, out tempInt);
		return particlePrefabs[tempInt];
	}

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