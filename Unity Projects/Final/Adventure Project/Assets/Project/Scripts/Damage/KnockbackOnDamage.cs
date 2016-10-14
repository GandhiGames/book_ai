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

		public void ApplyDamage (float damageAmount, Vector2 force)
		{
			m_MovementHandler.AddForce (force);
		}
	}
}