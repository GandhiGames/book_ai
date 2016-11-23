using UnityEngine;
using System.Collections.Generic;

namespace AdventureGame.CaveGenerator
{
    public interface NodeOccupancy
    {
        void Update(Vector2 pos);
        void SetCurrentNodeInUse(Vector2 pos, bool inUse);
        void MarkListInUse(List<Node> path, bool inUse);
    }

	/// <summary>
	/// Used to increase the path finding cost of the current occupied grid.
	/// </summary>
	public class NodeOccupancyImpl : NodeOccupancy
	{
        public Node CurrentNode { get { return m_CurrentNode; } }

        private Node m_PreviousNode;
		private Node m_CurrentNode;


		public void Update (Vector2 pos)
		{
			m_PreviousNode = m_CurrentNode;
			m_CurrentNode = GridManager.instance.grid.GetNodeFromPosition (pos);

			if (m_PreviousNode == null) {
				return;
			}
		
			if (!m_PreviousNode.Equals (m_CurrentNode)) {	
				m_PreviousNode.isOccupied = false;
				m_CurrentNode.isOccupied = true;
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

    public class IdleNodeOccupancyImpl : NodeOccupancy
    {
        public void Update(Vector2 pos) { }
        public void SetCurrentNodeInUse(Vector2 pos, bool inUse) { }
        public void MarkListInUse(List<Node> path, bool inUse) { }
    }
}

