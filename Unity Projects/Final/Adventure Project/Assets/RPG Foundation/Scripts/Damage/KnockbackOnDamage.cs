using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(MovementHandler))]
	public class KnockbackOnDamage : MonoBehaviour, DamageListener
	{
		private MovementHandler m_MovementHandler;

		void Awake ()
		{
			m_MovementHandler = GetComponent<MovementHandler> ();
		}

		public void ApplyDamage (int damageAmount, Vector2 force, Vector2 worldPos)
		{
			m_MovementHandler.AddForce (force);
		}
	}
}