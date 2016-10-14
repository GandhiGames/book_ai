using UnityEngine;
using System;
using System.Collections;

namespace AdventureGame.ActionManagement
{
	/// <summary>
	/// Movement command.
	/// </summary>
	public class MovementCommand : Command
	{
		private MovementHandler m_Movement;
		private float m_Speed;

		public MovementCommand (Transform character, Vector2 dir, 
			MovementHandler movement, float speed, Action actionOnExecute = null) : base (character, dir, actionOnExecute)
		{
			m_Movement = movement;
			m_Speed = speed;
		}
			
		protected override void DoCommand ()
		{
			m_Movement.SetVelocity (m_Direction * m_Speed * Time.deltaTime);
		}

	}
}
