using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public class PlayerKeyboardController : MonoBehaviour, PlayerControllerType
	{
		public Vector2 GetVelocity ()
		{
			return new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		}

		public bool ShouldRaiseProjectile ()
		{
			return Input.GetKey (KeyCode.R);
		}

		public bool ShouldReleaseProjectile ()
		{
			return Input.GetKeyUp (KeyCode.R);
		}

		public bool ShouldMelee ()
		{
			return Input.GetKeyDown (KeyCode.E);
		}
	}
}
