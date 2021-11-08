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

	public float traceDist = 10.0f;
	public float attackDist = 2.0f;
	private bool isDie = false;

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
			Destroy(collision.gameObject);
			animator.SetTrigger("IsHit");
		}
	}
}
