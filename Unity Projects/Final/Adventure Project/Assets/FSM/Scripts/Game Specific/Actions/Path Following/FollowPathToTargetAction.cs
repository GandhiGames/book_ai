using UnityEngine;
using System.Collections;
using AdventureGame.CaveGenerator;

namespace AdventureGame.StateManagement
{
	/// <summary>
	/// Follow a path to target transform using WaypointManager. 
	/// When character reaches end of path, a new path to the targets current lcoation is created.
	/// Only acts when waypoint manager is initialised. 
	/// </summary>
	public class FollowPathToTargetAction : FollowPathAction
	{
		private Transform m_Target;

		public FollowPathToTargetAction (Transform character, 
		                                 Transform target, 
		                                 float movementSpeed, 
		                                 LayerMask obstacleMask, 
		                                 WaypointManager waypointManager, 
		                                 MovementHandler movementHandler)
			: base (character, movementSpeed, obstacleMask, waypointManager, movementHandler)
		{
			m_Target = target;
		}

		/// <summary>
		/// Initialises a new path to follow.
		/// Current position is start node of path and players position is end node of path.
		/// </summary>
		protected override void InitialiseWaypoints ()
		{
			if (GridManager.instance.initialised) {
				Node start = GridManager.instance.grid.GetNodeFromPosition (m_Character.position);
				Node end = GridManager.instance.grid.GetNodeFromPosition (m_Target.position);

				m_WaypointManager.InitialisePath (start, end);
			}
			
		}


	}
}
