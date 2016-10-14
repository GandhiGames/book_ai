using UnityEngine;
using System;
using System.Collections;

namespace AdventureGame
{
	public class WeaponNotFoundException : Exception
	{
		public WeaponNotFoundException (string reason) : base (reason)
		{
		}
	}
}
