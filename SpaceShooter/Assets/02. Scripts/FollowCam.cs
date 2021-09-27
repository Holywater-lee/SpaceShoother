using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 20181220 이성수
/*
 lookPos라는 Vector3 변수와 targetHeight라는 변수를 만들어
 카메라가 보는 방향을 원활하게 조정 가능하게끔 수정하였습니다.
 카메라는 이제 height의 높이에서 targetTr + targetHeight만큼의 높이를 바라볼 것입니다.
 다른 방식으로 카메라암이라는 빈 오브젝트를 만들고 카메라를 해당 오브젝트의 자식 오브젝트로 만든 후에
 카메라암 오브젝트를 회전시키는 방식으로도 구현이 가능합니다.
 또는 플레이어 머리 부분에 빈 오브젝트를 만들어 카메라가 LookAt함수로 머리 부분의 빈 오브젝트를 바라보게 할 수도 있습니다.
 */

public class FollowCam : MonoBehaviour
{
	public Transform targetTr;
	public float dist = 10.0f;
	public float height = 3.0f;
	public float dampTrace = 30.0f; // 부드러운 추적을 위한 변수
	public float targetHeight = 2.0f;

	private Transform tr;

	void Start()
	{
		tr = GetComponent<Transform>();

		if (targetTr == null)
		{
			targetTr = FindObjectOfType<PlayerCtrl>().transform;
		}
	}

	void LateUpdate()
	{
		Vector3 lookPos = targetTr.position + Vector3.up * targetHeight;
		tr.position = Vector3.Lerp(tr.position, targetTr.position - targetTr.forward * dist + Vector3.up * height, Time.deltaTime * dampTrace);

		tr.LookAt(lookPos);
	}
}
