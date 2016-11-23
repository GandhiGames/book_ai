using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public class PlayerGamepadController : MonoBehaviour, PlayerControllerType
	{
		public Vector2 GetVelocity ()
		{
			return Vector2.zero;
		}

		public bool ShouldRaiseProjectile ()
		{
			return false;
		}

		public bool ShouldReleaseProjectile ()
		{
			return false;
		}

		public bool ShouldMelee ()
		{
			return false;
		}
	}
}