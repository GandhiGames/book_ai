using UnityEngine;
using System;
using System.Collections;

namespace AdventureGame
{
	public class Health : MonoBehaviour, DamageListener
	{
		public float HitPoints;

		public Action OnDeath;

		private float m_CurrentHitPoints;

		void OnEnable ()
		{
			m_CurrentHitPoints = HitPoints;
		}

		public void ApplyDamage (float damageAmount, Vector2 force)
		{
			m_CurrentHitPoints -= damageAmount;

			if (m_CurrentHitPoints <= 0f) {
				if (OnDeath != null) {
					OnDeath ();
				} else {
					Debug.LogWarning ("No death method set for " + gameObject.name + ". Character will live forever!");
				}
			}
		}
	}
}
