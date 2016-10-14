using UnityEngine;

namespace AdventureGame.StateManagement
{
	public abstract class FSMReason
	{
		public GlobalStateData.FSMTransistion transition { get; set; }

		public GlobalStateData.FSMStateID goToState { get; set; }

		public abstract bool ChangeState ();

		public virtual void Enter ()
		{
		}

		public virtual void Exit ()
		{
		}


		protected bool CloseToObject (Vector2 objPos, Vector2 characterPos, float distance)
		{
			return Vector2.Distance (characterPos, objPos) < distance;
		}
	}
}
