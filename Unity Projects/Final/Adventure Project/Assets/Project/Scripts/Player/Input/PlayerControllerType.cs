using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public interface PlayerControllerType
	{
		Vector2 GetVelocity ();

		bool ShouldRaiseProjectile ();

		bool ShouldReleaseProjectile ();

		bool ShouldMelee ();
	}
}