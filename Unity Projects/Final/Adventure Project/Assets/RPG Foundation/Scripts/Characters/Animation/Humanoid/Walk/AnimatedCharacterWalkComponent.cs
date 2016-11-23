using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public interface AnimatedCharacterWalkComponent
	{
		void SetWalkDown (int index);

		void SetWalkUp (int index);

		void SetWalkLeft (int index);

		void SetWalkRight (int index);
	}
}