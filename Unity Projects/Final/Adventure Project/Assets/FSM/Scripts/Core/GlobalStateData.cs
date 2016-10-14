namespace AdventureGame.StateManagement
{
	public static class GlobalStateData
	{
		/// <summary>
		/// FSM transistion.
		/// </summary>
		public enum FSMTransistion
		{
			None = 0,
			ReachedTarget,
			TargetInSight,
			TargetNotInSight,
			HitObstacle
		}


		/// <summary>
		/// Eash state requires a unique id.
		/// </summary>
		public enum FSMStateID
		{
			None = 0,
			FollowPathToTarget,
			SeekTarget,
			AttackTarget,
			Idle
		}
	}
}
