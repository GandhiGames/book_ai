using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public interface DamageListener
	{
		void ApplyDamage (float damageAmount, Vector2 force);
	}
}