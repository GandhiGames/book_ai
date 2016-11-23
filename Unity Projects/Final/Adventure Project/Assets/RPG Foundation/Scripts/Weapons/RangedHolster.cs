using UnityEngine;

namespace AdventureGame
{
	public class RangedHolster : MonoBehaviour, Holster<RangedWeapon>
	{
		private RangedWeapon m_equippedWeapon;
		private PlayerController m_controller;
		private Vector2 m_heading;

		void Awake ()
		{
			m_controller = GetComponent<PlayerController> ();
		}

		void OnEnable ()
		{
			m_controller.OnVelocityUpdated += UpdateHeading;
		}

		void OnDisable ()
		{
			m_controller.OnVelocityUpdated -= UpdateHeading;
		}

		public void WeaponEquipped (RangedWeapon weapon)
		{
			m_equippedWeapon = weapon;
		}

		public RangedWeapon GetWeapon ()
		{
			return m_equippedWeapon;
		}

		/// <summary>
		/// Weapon DoDamage wrapper. Invoked by animation.
		/// </summary>
		public void DoRangedDamage ()
		{
			if (m_equippedWeapon != null) {
				m_equippedWeapon.DoDamage (m_heading);
			}
		}

		private void UpdateHeading (Vector2 heading)
		{
			if (heading == Vector2.zero) {
				return;
			}

			m_heading = heading;
		}
	}
}
