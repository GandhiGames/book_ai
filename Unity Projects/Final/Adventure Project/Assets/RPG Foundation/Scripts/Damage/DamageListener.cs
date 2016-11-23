using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public interface DamageListener
	{
		void ApplyDamage (int damageAmount, Vector2 force, Vector2 worldPos);
	}
}