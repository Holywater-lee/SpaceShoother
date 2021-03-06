using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
	public enum MonsterState { Idle, Trace, Attack, Die };
	public MonsterState monsterState = MonsterState.Idle;

	private Transform playerTr;
	private NavMeshAgent nvAgent;
	private Animator animator;
	//private GameUI gameUI;

	public float traceDist = 10.0f;
	public float attackDist = 2.0f;
	private bool isDie = false;

	private int hp = 100;

	private void Start()
	{
		playerTr = GameObject.FindWithTag("Player").transform;
		nvAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();

		StartCoroutine(CheckMonsterStateCoroutine());
		StartCoroutine(MonsterActionCoroutine());
	}

	IEnumerator CheckMonsterStateCoroutine()
	{
		while (!isDie)
		{
			yield return new WaitForSeconds(0.2f);

			float dist = Vector3.Distance(transform.position, playerTr.position);

			if (dist <= attackDist)
			{
				monsterState = MonsterState.Attack;
			}
			else if (dist <= traceDist)
			{
				monsterState = MonsterState.Trace;
			}
			else
			{
				monsterState = MonsterState.Idle;
			}
		}
	}

	IEnumerator MonsterActionCoroutine()
	{
		while (!isDie)
		{
			switch (monsterState)
			{
				case MonsterState.Idle:
					nvAgent.isStopped = true;
					animator.SetBool("IsAttack", false);
					animator.SetBool("IsTrace", false);
					break;
				case MonsterState.Trace:
					nvAgent.destination = playerTr.position;
					nvAgent.isStopped = false;
					animator.SetBool("IsAttack", false);
					animator.SetBool("IsTrace", true);
					break;
				case MonsterState.Attack:
					nvAgent.isStopped = false;
					animator.SetBool("IsAttack", true);
					break;
				default:
					break;
			}
			yield return new WaitForSeconds(0.2f);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Bullet"))
		{
			hp -= collision.gameObject.GetComponent<Bullet>().damage;
			Destroy(collision.gameObject);
			CreateBloodEffect(collision.transform.position);

			if (hp <= 0)
			{
				MonsterDie();
			}
			else
				animator.SetTrigger("IsHit");
		}
	}

	void MonsterDie()
	{
		StopAllCoroutines();
		isDie = true;
		monsterState = MonsterState.Die;
		nvAgent.isStopped = true;
		animator.SetTrigger("IsDie");

		gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

		foreach(Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
		{
			coll.enabled = false;
		}

		GameUI.Instance.DisplayScore(50);
		Invoke("DisableThis", 3f);
	}

	void DisableThis()
	{
		gameObject.SetActive(false);
	}

	void CreateBloodEffect(Vector3 pos)
	{
		GameObject blood_1 = Instantiate(ParticleStorage.Instance.FindPrefabWithString("BloodEffect"), pos, Quaternion.identity);
		Destroy(blood_1, blood_1.GetComponent<ParticleSystem>().main.duration);

		Vector3 decalPos = transform.position + Vector3.up * 0.05f;
		Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360));
		GameObject blood_2 = Instantiate(ParticleStorage.Instance.FindPrefabWithString("BloodDecal"), decalPos, decalRot);
		float scale = Random.Range(1.5f, 3.5f);
		blood_2.transform.localScale = Vector3.one * scale;
		Destroy(blood_2, 5f);
	}

	void OnPlayerDie()
	{
		StopAllCoroutines();
		nvAgent.isStopped = true;
		animator.SetTrigger("IsPlayerDie");
	}
}
