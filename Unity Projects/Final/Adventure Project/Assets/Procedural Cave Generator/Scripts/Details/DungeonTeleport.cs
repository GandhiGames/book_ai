using UnityEngine;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	public class DungeonTeleport : MonoBehaviour
	{
		private static readonly Vector3 m_TeleportOffset = new Vector3 (0f, 1f, 0f);
		
		private DungeonTeleport m_ToLocation;
		private bool m_CanTeleport;

		void Start()
		{
			if(m_ToLocation == null)
			{
				var teleports = FindObjectsOfType<DungeonTeleport>();

				if(teleports.Length > 2)
				{
					Debug.LogError ("More than two teleports placed in scene");
					return;
				}

				foreach(var t in teleports)
				{
					if(t.gameObject.GetInstanceID () != gameObject.GetInstanceID ())
					{
						t.SetTeleportLocation (this);
						SetTeleportLocation (t);
					}
				}

			}

			m_CanTeleport = true;

		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if(m_CanTeleport && other.CompareTag ("Player"))
			{
				m_ToLocation.Teleport (other.gameObject);
			}

		}

		void OnTriggerExit2D(Collider2D other)
		{
			if(other.CompareTag ("Player"))
			{
				m_CanTeleport = true;
			}
		}

		public void SetTeleportLocation(DungeonTeleport location)
		{
			m_ToLocation = location;
		}

		public void Teleport(GameObject player)
		{
			m_CanTeleport = false;
			player.transform.position =  gameObject.transform.position + m_TeleportOffset;
		}
	}
}