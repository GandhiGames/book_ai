using UnityEngine;
using System.Collections.Generic;

namespace AdventureGame
{
	public class HolsterAnimationListener : MonoBehaviour, WeaponEquippedListener<MeleeWeapon>, WeaponEquippedListener<RangedWeapon>
	{
		private List<SpriteRenderer> weaponRenderers = new List<SpriteRenderer> ();

		void Start ()
		{
			//AddRendererToList<RangedWeapon>();
			//AddRendererToList<MeleeWeapon>();
		}

		public void WeaponEquipped (MeleeWeapon weapon)
		{
			Debug.Log ("Equipped melee");
			var renderer = GetRendererForWeapon (weapon);

			AddRenderer (renderer);
		}

		public void WeaponEquipped (RangedWeapon weapon)
		{
			Debug.Log ("Equipped ranged");
			var renderer = GetRendererForWeapon (weapon);

			AddRenderer (renderer);
		}


		public void HideWeapons ()
		{
			SetWeaponsEnabled (false);
		}

		public void ShowWeapons ()
		{
			SetWeaponsEnabled (true);
		}

		private void AddRenderer (SpriteRenderer renderer)
		{
			if (!weaponRenderers.Contains (renderer)) {
				weaponRenderers.Add (renderer);
			}
		}

		private void RemoveRenderer (SpriteRenderer renderer)
		{
			int index = weaponRenderers.IndexOf (renderer);

			if (index != Utilities.instance.entryNotFound) {
				weaponRenderers.RemoveAt (index);
			}
		}

		private void SetWeaponsEnabled (bool enabled)
		{
			foreach (var r in weaponRenderers) {
				r.enabled = enabled;
			}
		}

		private void AddRendererToList<T> () where T : Weapon
		{
			T weapon;

			try {
				weapon = GetWeapon<T> ();
			} catch (WeaponNotFoundException) {
				return;
			}

			var renderer = GetRendererForWeapon (weapon);

			AddRenderer (renderer);

		}

		private T GetWeapon<T> () where T : Weapon
		{
			T weapon = GetWeaponFromHolster<T> ();

			if (weapon == null) {
				throw new WeaponNotFoundException ("No weapon found in holster");
			}

			return weapon;
		}

		private SpriteRenderer GetRendererForWeapon (Weapon weapon)
		{ 
			return weapon.GetComponent<SpriteRenderer> (); 
		}

		private T GetWeaponFromHolster<T> () where T : Weapon
		{
			var holster = (Holster<T>)GetComponent<Holster<T>> ();

			if (holster == null) {
				throw new WeaponNotFoundException ("No holster found");
			}

			return holster.GetWeapon ();
		}
	}
}