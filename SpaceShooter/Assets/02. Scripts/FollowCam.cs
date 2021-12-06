using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 20181220 �̼���
/*
 lookPos��� Vector3 ������ targetHeight��� ������ �����
 ī�޶� ���� ������ ��Ȱ�ϰ� ���� �����ϰԲ� �����Ͽ����ϴ�.
 ī�޶�� ���� height�� ���̿��� targetTr + targetHeight��ŭ�� ���̸� �ٶ� ���Դϴ�.
 �ٸ� ������� ī�޶���̶�� �� ������Ʈ�� ����� ī�޶� �ش� ������Ʈ�� �ڽ� ������Ʈ�� ���� �Ŀ�
 ī�޶�� ������Ʈ�� ȸ����Ű�� ������ε� ������ �����մϴ�.
 �Ǵ� �÷��̾� �Ӹ� �κп� �� ������Ʈ�� ����� ī�޶� LookAt�Լ��� �Ӹ� �κ��� �� ������Ʈ�� �ٶ󺸰� �� ���� �ֽ��ϴ�.
 */

public class FollowCam : MonoBehaviour
{
	public Transform targetTr;
	public float dist = 10.0f;
	public float height = 3.0f;
	public float dampTrace = 30.0f; // �ε巯�� ������ ���� ����
	public float targetHeight = 2.0f;
	//LayerMask camColliderMask;

	private Transform tr;

	void Start()
	{
		tr = GetComponent<Transform>();

		if (targetTr == null)
		{
			targetTr = FindObjectOfType<PlayerCtrl>().transform;
		}
		//camColliderMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("PLAYER");
		//camColliderMask = ~camColliderMask;
	}

	void LateUpdate()
	{
		Vector3 lookPos = targetTr.position + Vector3.up * targetHeight;
		/*
		Vector3 destination = -targetTr.forward * dist + targetTr.up * height;
		float camDistance = Mathf.Sqrt(dist * dist + height * height);
		RaycastHit hitInfo;
		Physics.Raycast(targetTr.position, destination.normalized, out hitInfo, camDistance, camColliderMask);

		if (hitInfo.point != Vector3.zero)
		{
			tr.position = Vector3.Lerp(tr.position, hitInfo.point, Time.deltaTime * dampTrace);
		}
		else
		{
			tr.position = Vector3.Lerp(tr.position, targetTr.position - targetTr.forward * dist + targetTr.up * height, Time.deltaTime * dampTrace);
		}
		
		Debug.DrawRay(targetTr.position, destination.normalized * camDistance);
		*/
		tr.position = Vector3.Lerp(tr.position, targetTr.position - targetTr.forward * dist + targetTr.up * height, Time.deltaTime * dampTrace);
		tr.LookAt(lookPos);
	}
}
