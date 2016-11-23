using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public interface AnimatedCharacterIdleComponent
	{
		void SetIdleDown ();

		void SetIdleUp ();

		void SetIdleLeft ();

		void SetIdleRight ();
	}
}