using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
	[SerializeField] Vector3 offset;
	[SerializeField][Range(0.01f,1f)] float smoothSpeed = 0.125f;
	private Vector3 velocityRef;

	private void FixedUpdate()
	{
		if(target)
		{
			Vector3 targetPosition = target.position + offset;
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocityRef, smoothSpeed);
		}
	}
}
