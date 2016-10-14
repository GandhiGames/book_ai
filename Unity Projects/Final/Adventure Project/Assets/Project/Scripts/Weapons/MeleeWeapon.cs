using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public class MeleeWeapon : Weapon
	{
		void Awake ()
		{
			Equip<MeleeWeapon> (this);
		}

		public override void DoDamage (Vector2 heading)
		{
			var hit = Physics2D.Raycast (transform.position, heading, range, hitMask);

			if (hit.collider != null) {
				ApplyDamage (hit.collider, heading);
			}
		}

		private void ApplyDamage (Collider2D other, Vector2 heading)
		{
			var damageListeners = other.gameObject.GetComponents<DamageListener> ();

			foreach (var damage in damageListeners) {
				damage.ApplyDamage (damageAmount, heading * knockbackForce);
			}
		}
	}
}
