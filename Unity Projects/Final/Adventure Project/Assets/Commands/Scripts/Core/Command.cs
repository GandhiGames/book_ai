using UnityEngine;
using System;
using System.Collections;

namespace AdventureGame.ActionManagement
{
	/// <summary>
	/// Base abstract command. All commands (e.g. movement, attack) should inherit form this class.
	/// By recording a characters commands they can be played in reverse (i.e. rewound).
	/// </summary>
	public abstract class Command
	{
		protected Transform m_Character;
		protected Vector2 m_Direction;

		private Action m_ActionOnExecute;

		public Command (Transform character, Vector2 dir, Action actionOnExecute)
		{
			m_Character = character;
			m_Direction = dir;
			m_ActionOnExecute = actionOnExecute;
		}

		public void Execute ()
		{
			DoCommand ();

			if(m_ActionOnExecute != null)
			{
				m_ActionOnExecute ();
			}

		}

		protected abstract void DoCommand();
	}
}
