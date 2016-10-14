using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace AdventureGame.CaveGenerator
{
	public class CellPlacement
	{
		private GameObject m_Container;
		private CellPositionData[] m_Checks;
		private int m_MaxToSpawn = 0;

		struct PlacementDetails
		{
			public Node node;
			public GameObject prefab;

			public PlacementDetails (Node node, GameObject prefab)
			{
				this.node = node;
				this.prefab = prefab;
			}
		}

		public CellPlacement (GameObject container, CellPositionData[] checks, int maxToSpawn)
		{
			m_Container = container;
			m_Checks = checks;
			m_MaxToSpawn = maxToSpawn;
		}

		public void Place ()
		{
			var cellLocations = GetSuitableCellLocations ();

			if (cellLocations.IsNullOrEmpty ()) {
				return;
			}
				
			PlaceAtLocation (cellLocations);
		}

		private List<PlacementDetails> GetSuitableCellLocations ()
		{
			List<Node> foundNodes = new List<Node> ();
			
			int required = m_Checks.Length - 1;

			int selected = 0;
			int initialCheckIndex = GetInitialCheckIndex ();

			var starterNodes = GetInitialNodes (initialCheckIndex);

			foreach (var node in starterNodes) {
				int matches = NumberOfMatchesForNode (node, initialCheckIndex);

				if (matches == required) {
					foundNodes.Add (node);

					if (selected++ >= m_MaxToSpawn - 1) {
						break;
					}
				}
			}

			if (foundNodes.Count > 0) {
				return BuildLocations (foundNodes, initialCheckIndex);
			}

			return null;
		}

		private int GetInitialCheckIndex ()
		{
			for (int i = 0; i < m_Checks.Length; i++) {
				if (m_Checks [i].offset.Equals (Vector2.zero)) {
					return i;
				}
			}

			return -1;
		}


		private void PlaceAtLocation (List<PlacementDetails> worldDetail)
		{
			for (int i = 0; i < worldDetail.Count; i++) {
				var position = Utilities.instance.GetNodePosition (worldDetail [i].node);
				var objectToPlace = ObjectManager.instance.GetObject (worldDetail [i].prefab, position, false);
				objectToPlace.transform.SetParent (m_Container.transform);
				NotifyDetailOfPlacement (objectToPlace);
			}
		}

		private void NotifyDetailOfPlacement (GameObject detail)
		{
			var placementListeners = detail.GetComponents<DetailPlacementListener> ();

			foreach (var listener in placementListeners) {
				listener.Notify ();
			}
		}

		private IList<Node> GetInitialNodes (int initialIndex)
		{
			NodeType initialType = m_Checks [initialIndex].type;

			var starterNodes = GridManager.instance.grid.GetNodesOfType (initialType);

			var shuffledNodes = starterNodes.Clone ();
			shuffledNodes.Shuffle ();

			return shuffledNodes;
		}

		private int NumberOfMatchesForNode (Node node, int initialIndex)
		{
			int hits = 0;

			for (int i = 0; i < m_Checks.Length; i++) {

				var check = m_Checks [i];

				if (i != initialIndex &&
				    GridManager.instance.grid.ContainsNodeTypeAtPosition (node.coordinates + check.offset, check.type)) {
					hits++;
				}
			}

			return hits;
		}

		private List<PlacementDetails> BuildLocations (List<Node> foundNodes, int initialIndex)
		{
			List<PlacementDetails> locations = new List<PlacementDetails> ();

			foreach (var node in foundNodes) {
				foreach (var check in m_Checks) {
					if (check.HasPrefab ()) {
						Node nodeToAdd = GridManager.instance.grid.GetNodeFromGridCoordinate (node.coordinates + check.offset);
						locations.Add (new PlacementDetails (nodeToAdd, check.prefab));
					}
				}
			}

			return locations;
		}
	}

	[System.Serializable]
	public class CellPositionData
	{
		public Vector2 offset;
		public NodeType type;
		public GameObject prefab;

		public CellPositionData (Vector2 offset, NodeType type)
			: this (offset, type, null)
		{
		}

		public CellPositionData (Vector2 offset, NodeType type, GameObject prefab)
		{
			this.offset = offset;
			this.type = type;
			this.prefab = prefab;
		}

		public bool HasPrefab ()
		{
			return prefab != null;
		}
	}
}
