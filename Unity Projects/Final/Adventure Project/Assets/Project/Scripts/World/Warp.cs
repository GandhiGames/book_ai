using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(Collider2D))]
	public class Warp : MonoBehaviour
	{
		public Transform target;

		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.CompareTag ("Player")) {
				other.gameObject.transform.position = target.position;
				Camera.main.transform.position = target.position + (Vector3.forward * Camera.main.transform.position.z);
			}
		}
	}
}
