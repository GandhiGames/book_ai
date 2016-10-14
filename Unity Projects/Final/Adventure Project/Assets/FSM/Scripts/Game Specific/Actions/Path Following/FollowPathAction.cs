using UnityEngine;
using System.Collections;
using AdventureGame.CaveGenerator;
using AdventureGame.ActionManagement;

namespace AdventureGame.StateManagement
{
	public abstract class FollowPathAction : FSMAction
	{
		protected Transform m_Character;
		protected WaypointManager m_WaypointManager;

		private static readonly float PATH_REINITIALISE_WAIT_TIME = 1f;

		private MovementHandler m_MovementHandler;
		private float m_FollowPathSpeed;
		private LayerMask m_ObstacleMask;
		private bool m_ShouldFindPath;

		public FollowPathAction (Transform character, 
		                         float followPathSpeed, 
		                         LayerMask obstacleMask,
		                         WaypointManager waypointManager,
		                         MovementHandler movementHandler)
		{
			m_Character = character;
			m_FollowPathSpeed = followPathSpeed;
			m_ObstacleMask = obstacleMask;

			m_WaypointManager = waypointManager;
			m_WaypointManager.isComplete = true;

			m_MovementHandler = movementHandler;

		}

		protected abstract void InitialiseWaypoints ();

		protected override Command BuildAction ()
		{
			Vector2 newPos = m_WaypointManager.GetNextReactivePosition (m_Character.position);

			return new ObstacleAvoidanceMovementCommand (
				m_Character, 
				newPos, 
				m_MovementHandler,
				m_FollowPathSpeed,
				m_ObstacleMask);

		}

		protected override bool OkToAct ()
		{
			return m_WaypointManager.initialised && !m_WaypointManager.isComplete;
		}

		public override void Enter ()
		{
			m_WaypointManager.isComplete = true;

			m_ShouldFindPath = true;
			m_WaypointManager.StartCoroutine (InitialiseWaypoint ());
		}

		public override void Exit ()
		{
			m_ShouldFindPath = false;
		}

		private IEnumerator InitialiseWaypoint ()
		{
			while (m_ShouldFindPath) {
				if (!m_WaypointManager.initialised || m_WaypointManager.isComplete) {
					InitialiseWaypoints ();
				}
				yield return new WaitForSeconds (PATH_REINITIALISE_WAIT_TIME);
			}
		}
				
	}
}
