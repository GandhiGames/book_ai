using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public abstract class Weapon : MonoBehaviour
	{
		public Transform holsterOwner;
		public LayerMask hitMask;
		public LayerMask obstacleMask;
		public float damageAmount = 1f;
		public float range = 2f;
		public float knockbackForce = 150f;

		public abstract void DoDamage (Vector2 heading);

		protected void Equip<T> (T toEquip) where T: Weapon
		{
			var weaponEquipListeners = holsterOwner.GetComponents<WeaponEquippedListener<T>> ();

			if (weaponEquipListeners.IsNullOrEmpty ()) {
				Debug.LogWarning ("Holster for " + typeof(T).Name + " not found on parent");
				return;
			}

			foreach (var listener in weaponEquipListeners) {
				listener.WeaponEquipped (toEquip);
			}
		}
	}
}
