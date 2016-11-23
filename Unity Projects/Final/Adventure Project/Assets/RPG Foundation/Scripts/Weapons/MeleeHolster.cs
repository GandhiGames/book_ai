using UnityEngine;

namespace AdventureGame
{
	public class MeleeHolster : MonoBehaviour, Holster<MeleeWeapon>
	{
		private MeleeWeapon m_equippedWeapon;
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

		public void WeaponEquipped (MeleeWeapon weapon)
		{
			m_equippedWeapon = weapon;
		}

		/// <summary>
		/// Weapon DoDamage wrapper. Invoked by animation.
		/// </summary>
		public void DoMeleeDamage ()
		{
			if (m_equippedWeapon != null) {
				m_equippedWeapon.DoDamage (m_heading);
			}
		}

		public MeleeWeapon GetWeapon ()
		{
			return m_equippedWeapon;
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
