using UnityEngine;
using System;
using System.Collections;

namespace AdventureGame.ActionManagement
{
	/// <summary>
	/// Perform melee attack
	/// </summary>
	public class MeleeCommand : Command
	{
		private float m_DamageAmount;
		private float m_KnockbackForce;
		private float m_Range;
		private LayerMask m_HitMask;
		
		public MeleeCommand (Transform character, Vector2 dir,
			float damageAmount, float knockbackForce,
			float range, LayerMask hitmask, Action actionOnExecute = null) : base (character, dir, actionOnExecute)
		{
			m_DamageAmount = damageAmount;
			m_KnockbackForce = knockbackForce;
			m_Range = range;
			m_HitMask = hitmask;
		}


		protected override void DoCommand ()
		{
			var hit = Physics2D.Raycast (m_Character.position, m_Direction, m_Range, m_HitMask);

			if (hit.collider != null) {
				ApplyDamage (hit.collider);
			}
		}

		private void ApplyDamage (Collider2D other)
		{
			var damageListeners = other.gameObject.GetComponents<DamageListener> ();

			foreach (var damage in damageListeners) {
				damage.ApplyDamage (m_DamageAmount, m_Direction * m_KnockbackForce);
			}
		}
		

	}
}
