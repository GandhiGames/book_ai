using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent(typeof(Camera))]
	public class CameraFollow : MonoBehaviour 
	{
		public Transform target;
		public float smoothFollowSpeed = 0.1f;

		void Start () 
		{
			if(transform == null)
			{
				Debug.LogWarning("No target set. Disabling camera follow");
				enabled = false;
			}
		}

		void LateUpdate () 
		{
			transform.position = (Vector3)(Vector2.Lerp (transform.position, target.position, smoothFollowSpeed)) 
				+ (Vector3.forward * transform.position.z);
		}
	}
}
