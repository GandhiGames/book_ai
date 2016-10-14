using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public interface MovementHandler
	{
		bool movementEnabled { get;}
		Vector2 velocity { get; }
		Vector2 heading { get; }

		void SetVelocity (Vector2 velocity);

		void AddForce (Vector2 force);

		void DisableMovement();
		void EnableMovement();
	}
}