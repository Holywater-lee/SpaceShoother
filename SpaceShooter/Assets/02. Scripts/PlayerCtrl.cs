using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
	private float h = 0.0f;
	private float v = 0.0f;
	private Transform tr;
	public float moveSpeed = 10f;
	public float rotSpeed = 100f;

	// 20181220 이성수
	Animator anim;
	bool isMove = false;

	void Start()
	{
		tr = GetComponent<Transform>();
		anim = GetComponentInChildren<Animator>();
	}

	void Update()
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		anim.SetBool("isMove", isMove); // 애니메이터로 애니메이션 구현하였습니다.
		anim.SetFloat("hMove", h, 1f, Time.deltaTime * 10f);
		anim.SetFloat("vMove", v, 1f, Time.deltaTime * 10f);

		tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
	}

	private void FixedUpdate()
	{
		Vector3 moveDir = new Vector3(h, 0, v);
		isMove = Vector3.Magnitude(moveDir) != 0 ? true : false; // 이동중인지는 벡터 길이가 0인지 아닌지로 체크합니다.

		tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
	}
}
