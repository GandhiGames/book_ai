using UnityEngine;
using System;
using System.Collections;

namespace AdventureGame.ActionManagement
{
	// <summary>
	/// Projectile attack.
	/// </summary>
	public class ProjectileCommand : Command
	{
		public ProjectileCommand (Transform character, Vector2 dir, Action actionOnExecute = null) : base (character, dir, actionOnExecute)
		{

		}
			
		protected override void DoCommand ()
		{

		}

	}
}
