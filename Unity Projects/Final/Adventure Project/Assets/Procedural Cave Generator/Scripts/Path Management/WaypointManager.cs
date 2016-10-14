using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AdventureGame.CaveGenerator
{
	[RequireComponent (typeof(PathManager))]
	public class WaypointManager : MonoBehaviour
	{
		public LayerMask obstacleMask;

		//When entity is within this distance to waypoint it will be registered as visited
		public float waypointProximity = 0.1f;

		public bool isComplete { get; set; }

		public bool initialised {
			get {
				return !m_Waypoints.IsNullOrEmpty ();
			}
		}


		private static readonly int MAX_WAYPOINT_LOOK_AHEAD = 30;

		private const float WALL_WEIGHT = 3f;

		//list of waypoint objects - initialised in InitiailiseWayPoints
		private List<Node> m_Waypoints = new List<Node> ();

		//If not looped will end at last wapoint
		private bool m_IsLooped = false;

		private int m_CurrentWaypoint = 0;


		private PathManager m_PathManager;


		void Awake ()
		{
			isComplete = true;

			m_PathManager = GetComponent<PathManager> ();
		}


		public void InitialisePath (Node start, Node end, bool isLooped = false, float wallWeight = WALL_WEIGHT)
		{
			m_IsLooped = isLooped;
			
			var path = m_PathManager.GetShortestPath (start, end, wallWeight, false);
			InitialiseWaypointsFromNodes (path);
		}


		private void InitialiseWaypointsFromNodes (List<Node> path)
		{

			if (m_Waypoints.Count > 0) {
				m_Waypoints.Clear ();
			}

			if (!path.IsNullOrEmpty ()) {
				m_Waypoints.AddRange (path); 
								
				isComplete = false;
				m_CurrentWaypoint = 0;	
			} else {
				isComplete = true;
			}

		}



		private void CheckHasReachedCurrentWaypointAndIncrement (Vector2 characterPosition)
		{
			if (HasReachedCurrentWaypoint (characterPosition)) {
				IncrementCurrentWaypoint ();
			}
		}


		public bool HasReachedCurrentWaypoint (Vector2 characterPosition)
		{
			return (Vector2.Distance (GetCurrentWaypointPosition (), characterPosition) < waypointProximity);
		}

		public Node GetCurrentWaypoint ()
		{
			return m_Waypoints [m_CurrentWaypoint];
		}


		private void IncrementCurrentWaypoint ()
		{

			if (m_CurrentWaypoint == m_Waypoints.Count - 1) {
				if (m_IsLooped) //seek to first waypoint
										m_CurrentWaypoint = 0;
				else
					isComplete = true;
			} else {
				m_CurrentWaypoint++;
			}
		}

		private void UpdateCurrentWaypoint (int newWaypoint)
		{
					
			m_CurrentWaypoint = newWaypoint;
						
		}

		private Vector2 GetCurrentWaypointPosition ()
		{
			return  Utilities.instance.GetNodePosition (m_Waypoints [m_CurrentWaypoint]);
		}


	

		private void DebugDrawPath ()
		{
			// Draw path for debug purposes.
			for (int i = m_CurrentWaypoint; i < m_Waypoints.Count - 1; i++) {
				Debug.DrawLine (Utilities.instance.GetNodePosition (m_Waypoints [i]),
					Utilities.instance.GetNodePosition (m_Waypoints [i + 1]));
			}
						
		}

		public Vector2 GetNextReactivePosition (Vector2 currentPosition)
		{
			//check reached current waypoint
			// if no return current waypoint position
			// else loop through each waypoint, find furthest waypoint with clear path and set that to current.
			
			bool reachedWaypoint = HasReachedCurrentWaypoint (currentPosition);


			if (reachedWaypoint && m_CurrentWaypoint == m_Waypoints.Count - 1) {
				if (m_IsLooped) {
					m_CurrentWaypoint = 0;
				} else {
					isComplete = true;
				}

			}

			int currentLookAhead = 0;
		
			if (reachedWaypoint && !isComplete) {
				var newWaypoint = m_CurrentWaypoint;
				for (int i = m_CurrentWaypoint; i < m_Waypoints.Count; i++) {
					var newPos = Utilities.instance.GetNodePosition (m_Waypoints [i]);
					if (!Utilities.instance.LayerInPath (currentPosition, newPos, obstacleMask)) {
						newWaypoint = i;

						if (currentLookAhead++ >= MAX_WAYPOINT_LOOK_AHEAD) {
							break;
						}
					}
					
				}

				UpdateCurrentWaypoint (newWaypoint);
			}


			if (Utilities.instance.IsDebug) {
				DebugDrawPath ();
			}

			return  (GetCurrentWaypointPosition () - currentPosition).normalized; 

		}

		public Vector2 GetNextPosition (Vector2 currentPosition)
		{
			CheckHasReachedCurrentWaypointAndIncrement (currentPosition);			


			Vector2 newPos = (GetCurrentWaypointPosition () - currentPosition).normalized;

			if (Utilities.instance.IsDebug) {
				DebugDrawPath ();
			}

			return newPos; 

					
					
		}


	}
}
