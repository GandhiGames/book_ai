using UnityEngine;
using System;
using System.Collections;

namespace AdventureGame.ActionManagement
{
	/// <summary>
	/// Perform no command.
	/// </summary>
	public class IdleCommand : Command
	{
		public IdleCommand (Transform character, Vector2 dir, Action actionOnExecute = null) : base (character, dir, actionOnExecute)
		{
		}

		protected override void DoCommand ()
		{

		}
	}
}
