using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 20181220 이성수
/*
 왼쪽 쉬프트키를 누르면 이동속도가 0.5배가 됩니다.
 왼쪽 컨트롤키를 누르면 이동속도가 1.5배가 됩니다.
 두 키의 입력이 같이 이루어진 경우 먼저 입력된 키가 적용됩니다.
 */

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

	//Animator anim;
	//bool isMove = false;

	void Start()
	{
		tr = GetComponent<Transform>();
		//anim = GetComponentInChildren<Animator>();
	}

	void Update()
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		//anim.SetBool("isMove", isMove);
		//anim.SetFloat("hMove", h, 1f, Time.deltaTime * 10f);
		//anim.SetFloat("vMove", v, 1f, Time.deltaTime * 10f);
		//isMove = Vector3.Magnitude(moveDir) != 0 ? true : false;

		Vector3 moveDir = new Vector3(h, 0, v);

		ChangeMoveState();
		ChangeMoveSpeed();

		tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
		tr.Translate(moveDir.normalized * applySpeed * Time.deltaTime, Space.Self); // applySpeed를 적용하였습니다.
	}

	// 현재 이동 상태(currentMoveState)를 변경해주는 메서드입니다.
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

	// 현재 이동 상태(currentMoveState)에 따라 실제 이동속도(applySpeed)를 적용해주는 메서드입니다.
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
