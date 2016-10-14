using UnityEngine;
using System.Collections;
using AdventureGame.CaveGenerator;

namespace AdventureGame.StateManagement
{
	public class FollowPathToRandomAction : FollowPathAction
	{
		public FollowPathToRandomAction (Transform character, 
		                                float speed, LayerMask obstacleMask, 
		                                 WaypointManager waypointManager, MovementHandler movementHandler)
			: base (character, speed, obstacleMask, waypointManager, movementHandler)
		{
		}

		/// <summary>
		/// Initialises a new path to follow.
		/// Current position is start node of path and a random node a minimun distance from the character is the end node.
		/// </summary>
		protected override void InitialiseWaypoints ()
		{
			if (GridManager.instance.initialised) {
				Node start = GridManager.instance.grid.GetNodeFromPosition (m_Character.position);
			
				Node end = GridManager.instance.GetRandomFloorNode (); // GetFloorNodeMinDistanceFromStartNode (m_WanderDistance, start);
						
				m_WaypointManager.InitialisePath (start, end);
			}
			
		}
	}
}
