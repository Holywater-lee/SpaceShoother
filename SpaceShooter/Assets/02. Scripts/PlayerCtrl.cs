using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 20181220 이성수

public class PlayerCtrl : MonoBehaviour
{
	enum MoveState { Normal, Fast, Slow };
	private float h = 0.0f;
	private float v = 0.0f;
	private Transform tr;
	public float moveSpeed = 10f;
	public float rotSpeed = 100f;

	float applySpeed; // 실제로 적용될 이동속도에 관한 변수

	MoveState currentMoveState = MoveState.Normal;

	Animator anim;
	bool isMove = false;

	public int hp = 100;
	private int initHp;

	void Start()
	{
		initHp = hp;

		tr = GetComponent<Transform>();
		anim = GetComponentInChildren<Animator>();

		applySpeed = moveSpeed;
		anim.SetFloat("moveSpeed", applySpeed / moveSpeed);
	}

	void Update()
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		Vector3 moveDir = new Vector3(h, 0, v);

		anim.SetBool("isMove", isMove);
		anim.SetFloat("hMove", h, 1f, Time.deltaTime * 10f);
		anim.SetFloat("vMove", v, 1f, Time.deltaTime * 10f);
		isMove = Vector3.Magnitude(moveDir) != 0 ? true : false;

		ApplyMoveState();

		tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
		tr.Translate(moveDir.normalized * applySpeed * Time.deltaTime, Space.Self);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PUNCH"))
		{
			hp -= 10;
			Debug.Log("플레이어 hp: " + hp);
			GameUI.Instance.imgHPBar.fillAmount = (float)hp / (float)initHp;

			if (hp <= 0)
			{
				PlayerDie();
			}
		}
	}

	void PlayerDie()
	{
		Debug.Log("플레이어 사망");
		GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

		foreach (GameObject m in monsters)
		{
			m.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
		}
		gameObject.SetActive(false);
	}

	void ChangeMoveState(MoveState state)
	{
		switch(currentMoveState)
		{
			case MoveState.Normal:
				break;

			case MoveState.Fast:
				break;

			case MoveState.Slow:
				break;
		}

		currentMoveState = state;

		switch (state)
		{
			case MoveState.Normal:
				applySpeed = moveSpeed;
				break;

			case MoveState.Fast:
				applySpeed = moveSpeed * 1.5f;
				break;

			case MoveState.Slow:
				applySpeed = moveSpeed * 0.5f;
				break;
		}
		anim.SetFloat("moveSpeed", applySpeed / moveSpeed);
	}

	void ApplyMoveState()
	{
		switch (currentMoveState)
		{
			case MoveState.Normal:
				if (Input.GetKeyDown(KeyCode.LeftShift))
				{
					ChangeMoveState(MoveState.Fast);
				}
				if (Input.GetKeyDown(KeyCode.LeftControl))
				{
					ChangeMoveState(MoveState.Slow);
				}
				break;

			case MoveState.Fast:
				if (Input.GetKeyUp(KeyCode.LeftShift))
				{
					ChangeMoveState(MoveState.Normal);
				}
				break;

			case MoveState.Slow:
				if (Input.GetKeyUp(KeyCode.LeftControl))
				{
					ChangeMoveState(MoveState.Normal);
				}
				break;
		}
	}
}
