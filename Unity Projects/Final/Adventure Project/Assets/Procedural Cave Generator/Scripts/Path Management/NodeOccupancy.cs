using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AdventureGame.CaveGenerator
{
	/// <summary>
	/// Used to increase the path finding cost of the current occupied grid.
	/// </summary>
	public class NodeOccupancy
	{
		private Node previousNode;
		private Node currentNode;

		public Node CurrentNode { get { return currentNode; } }

		public void Update (Vector2 pos)
		{

			previousNode = currentNode;
			currentNode = GridManager.instance.grid.GetNodeFromPosition (pos);

			if (previousNode == null) {
				return;
			}
		
			if (!previousNode.Equals (currentNode)) {	
				previousNode.isOccupied = false;
				currentNode.isOccupied = true;
			}
		
		}

		public void SetCurrentNodeInUse (Vector2 pos, bool inUse)
		{
			GridManager.instance.grid.GetNodeFromPosition (pos).isOccupied = inUse;

		}

		public void MarkListInUse (List<Node> path, bool inUse)
		{
			foreach (var node in path) {
				node.isOccupied = inUse;
			}
		}
	}
}