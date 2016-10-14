﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AdventureGame.CaveGenerator
{
	/// <summary>
	/// Encapsulates 2D array of all active nodes in game.
	/// Provides helper methods such as Add and Contains that mimick there list counterparts.
	/// </summary>
	[System.Serializable]
	public class NodeList
	{
		public int Count {
			get {
				return m_Nodes.Length;
			}
		}

		private Node[,] m_Nodes;
		private Vector2 m_Size;

		private Dictionary<NodeType, List<Node>> m_NodeLookup = new Dictionary<NodeType, List<Node>> ();

		public NodeList (Vector2 gridSize)
		{
			m_Nodes = new Node[(int)gridSize.x, (int)gridSize.y];
			m_Size = new Vector2 (gridSize.x, gridSize.y);
		}

		/// <summary>
		/// Contains the specified node.
		/// </summary>
		/// <param name="node">Node.</param>
		public bool Contains (Node node)
		{
			Vector2 coord = node.coordinates;
			if (IsValidCoordinate (coord)) {
				Node obj = m_Nodes [(int)coord.x, (int)coord.y];
		
				return obj != null;
			} else {
				return false;
			}

		}

		public bool IsValidCoordinate (Vector2 coord)
		{
			return !(coord.x < 0 ||
			coord.x >= m_Size.x ||
			coord.y < 0 ||
			coord.y >= m_Size.y);
		}

		public bool ContainsNodeTypeAtPosition (Vector2 coord, NodeType nodeType)
		{
			if (!IsValidCoordinate (coord)) {
				return false;
			}

			return m_Nodes [(int)coord.x, (int)coord.y].nodeType == nodeType;
		}

		/// <summary>
		/// Add the specified node to array.
		/// </summary>
		/// <param name="node">Node.</param>
		public void Add (Node node)
		{
			Vector2 coord = node.coordinates;

			if (IsValidCoordinate (coord)) {
				m_Nodes [(int)coord.x, (int)coord.y] = node;
			}
		}

		/// <summary>
		/// Returns first Node.
		/// </summary>
		public Node First ()
		{
			if (this.m_Nodes.Length > 0)
				return m_Nodes [0, 0];

			return null;
		}

		
		/// <summary>
		/// Replace the specified origNode with newNode.
		/// </summary>
		/// <param name="origNode">Original node.</param>
		/// <param name="newNode">New node.</param>
		public void Replace (Node origNode, Node newNode)
		{
			Vector2 coord = origNode.coordinates;

			if (IsValidCoordinate (coord)) {
				m_Nodes [(int)coord.x, (int)coord.y] = newNode;
			}
		}

		/// <summary>
		/// Replace the node with origNodeCoord, with the node newNode.
		/// </summary>
		/// <param name="origNodeCoord">Original node coordinate.</param>
		/// <param name="newNode">New node.</param>
		public void Replace (Vector2 origNodeCoord, Node newNode)
		{
			if (IsValidCoordinate (origNodeCoord)) {
				m_Nodes [(int)origNodeCoord.x, (int)origNodeCoord.y] = newNode;
			}
		}

		/// <summary>
		/// Returns a list of adjacent nodes with or without obstacles.
		/// </summary>
		/// <returns>The adjacent nodes.</returns>
		/// <param name="cellCoordinate">Cell coordinate or original node.</param>
		/// <param name="includeObstacles">If set to <c>true</c> include obstacles.</param>
		public List<Node> GetAdjacentNodes (Vector2 cellCoordinate, bool includeObstacles)
		{
			if (includeObstacles) {
				return GetAdjacentNodes (cellCoordinate);
			} else {
				return GetAdjacentNodesMinusObstacles (cellCoordinate);
			}
		}

		/// <summary>
		/// Return adjacent nodes minus obstacles.
		/// </summary>
		/// <returns>The adjacent nodes minus obstacles.</returns>
		/// <param name="cellCoordinate">Cell coordinate or original node.</param>
		private List<Node> GetAdjacentNodesMinusObstacles (Vector2 cellCoordinate)
		{
			List<Node> cells = new List<Node> ();


			// Top
			Vector2 top = new Vector2 (cellCoordinate.x, cellCoordinate.y - 1);
			Node node = GetValidNode (top);
			if (node != null) {
				cells.Add (node);
			}

	

			// Left
			Vector2 left = new Vector2 (cellCoordinate.x - 1, cellCoordinate.y);
			node = GetValidNode (left);
			if (node != null) {
				cells.Add (node);
			}
			
			// Below
			Vector2 below = new Vector2 (cellCoordinate.x, cellCoordinate.y + 1);
			node = GetValidNode (below);
			if (node != null) {
				cells.Add (node);
			}

				
			// Right
			Vector2 right = new Vector2 (cellCoordinate.x + 1, cellCoordinate.y);
			node = GetValidNode (right);
			if (node != null) {
				cells.Add (node);
			} 
			
			return cells;
		}

		/// <summary>
		/// return the adjacent nodes including obstacles.
		/// </summary>
		/// <returns>The adjacent nodes.</returns>
		/// <param name="cellCoordinate">Cell coordinate of original node.</param>
		private List<Node> GetAdjacentNodes (Vector2 cellCoordinate)
		{
			List<Node> cells = new List<Node> ();
			
			// Top
			Vector2 top = new Vector2 (cellCoordinate.x, cellCoordinate.y - 1);
			if (IsValidCoordinate (top)) {
				cells.Add (m_Nodes [(int)top.x, (int)top.y]);
			}

					
			// Left
			Vector2 left = new Vector2 (cellCoordinate.x - 1, cellCoordinate.y);
			if (IsValidCoordinate (left)) {
				cells.Add (m_Nodes [(int)left.x, (int)left.y]);
			}
			
			// Bellow
			Vector2 bellow = new Vector2 (cellCoordinate.x, cellCoordinate.y + 1);
			if (IsValidCoordinate (bellow)) {
				cells.Add (m_Nodes [(int)bellow.x, (int)bellow.y]);
			}
								
			
			// Right
			Vector2 right = new Vector2 (cellCoordinate.x + 1, cellCoordinate.y);
			if (IsValidCoordinate (right)) {
				cells.Add (m_Nodes [(int)right.x, (int)right.y]);
			}
			
			return cells;
		}

		/// <summary>
		/// Returns a node at a specified (on-screen) position that is not an obstacle.
		/// </summary>
		/// <returns>The valid node.</returns>
		/// <param name="pos">Position of node.</param>
		private Node GetValidNode (Vector2 pos)
		{
			Node node = GetNodeFromGridCoordinate (pos);

			if (!node.IsObstacle) {
				return node;
			} else {
				return null;
			}
		}

		/// <summary>
		/// Returns a node for a specified on-screen position.
		/// </summary>
		/// <returns>The node from position.</returns>
		/// <param name="position">Position.</param>
		public Node GetNodeFromPosition (Vector2 position)
		{
			// First need to convert position into coordinates. If no node found then null is returned.
			Vector2? coord = Utilities.instance.GetGridCoordinateForPosition (position);

			if (coord.HasValue) {
				return GetNodeFromGridCoordinate (coord.Value);
			}

			return null;
		}

		/// <summary>
		/// Returns a node with a specified grid coordinate.
		/// </summary>
		/// <returns>The node from grid coordinate.</returns>
		/// <param name="coord">Coordinate.</param>
		public Node GetNodeFromGridCoordinate (Vector2 coord)
		{
			if (IsValidCoordinate (coord)) {
				return m_Nodes [(int)coord.x, (int)coord.y];
			}
			
			
			return null;
		}

		public void ClearNodeList (NodeType type)
		{
			if (m_NodeLookup.ContainsKey (type)) {
				m_NodeLookup.Remove (type);
			}
		}

		public List<Node> GetNodesOfType (NodeType type)
		{
			if (m_NodeLookup.ContainsKey (type)) {
				return m_NodeLookup [type];
			}
			
			var nodes = GetNodes (type);

			m_NodeLookup.Add (type, nodes);

			return nodes;

		}

		private List<Node> GetNodes(NodeType type)
		{
			List<Node> returnNodes = new List<Node> ();

			for (int x = 0; x < m_Nodes.GetLength (0); x++) {
				for (int y = 0; y < m_Nodes.GetLength (1); y++) {
					if (m_Nodes [x, y].nodeType == type) {
						returnNodes.Add (m_Nodes [x, y]);
					}
				}
			}

			return returnNodes;
		}

	}
}
