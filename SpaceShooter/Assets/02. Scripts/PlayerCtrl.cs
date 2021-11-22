using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 20181220 이성수

[System.Serializable]
public class Anim
{
	public AnimationClip idle;
	public AnimationClip runForward;
	public AnimationClip runBackward;
	public AnimationClip runRight;
	public AnimationClip runLeft;
}

public class PlayerCtrl : MonoBehaviour
{
	enum MoveState { Normal, Fast, Slow };
	private float h = 0.0f;
	private float v = 0.0f;
	private Transform tr;
	public float moveSpeed = 10f;
	public float rotSpeed = 100f;

	float applySpeed; // 실제로 적용될 이동속도에 관한 변수입니다.

	MoveState currentMoveState = MoveState.Normal;

	Animator anim;
	bool isMove = false;
	//public Anim anim;
	//public Animation _animation;

	public int hp = 100;
	private int initHp;

	void Start()
	{
		initHp = hp;

		tr = GetComponent<Transform>();
		anim = GetComponentInChildren<Animator>();

		//_animation = GetComponentInChildren<Animation>();
		//_animation.clip = anim.idle;
		//_animation.Play();
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

		ChangeMoveState();
		ChangeMoveSpeed();

		tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
		tr.Translate(moveDir.normalized * applySpeed * Time.deltaTime, Space.Self); // applySpeed를 적용하였습니다.

		/*
		if (v >= 0.1f)
		{
			_animation.CrossFade(anim.runForward.name, 0.3f);
		}
		else if (v <= -0.1f)
		{
			_animation.CrossFade(anim.runBackward.name, 0.3f);
		}
		else if (h >= 0.1f)
		{
			_animation.CrossFade(anim.runRight.name, 0.3f);
		}
		else if (h <= -0.1f)
		{
			_animation.CrossFade(anim.runLeft.name, 0.3f);
		}
		else
		{
			_animation.CrossFade(anim.idle.name, 0.3f);
		}*/
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

	void ChangeMoveState()
	{
		switch (currentMoveState)
		{
			case MoveState.Normal:
				if (Input.GetKeyDown(KeyCode.LeftControl))
				{
					currentMoveState = MoveState.Fast;
				}
				if (Input.GetKeyDown(KeyCode.LeftShift))
				{
					currentMoveState = MoveState.Slow;
				}
				break;

			case MoveState.Fast:
				if (Input.GetKeyUp(KeyCode.LeftControl))
				{
					currentMoveState = MoveState.Normal;
				}
				break;

			case MoveState.Slow:
				if (Input.GetKeyUp(KeyCode.LeftShift))
				{
					currentMoveState = MoveState.Normal;
				}
				break;
		}
	}

	void ChangeMoveSpeed()
	{
		switch (currentMoveState)
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
	}
}
