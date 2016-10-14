using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public interface Holster<T> : WeaponEquippedListener<T> where T : Weapon
	{
		T GetWeapon ();
	}
}