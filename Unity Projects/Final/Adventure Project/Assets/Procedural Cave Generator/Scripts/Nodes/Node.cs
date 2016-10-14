using UnityEngine;
using System;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	[System.Serializable]
	public enum NodeType
	{
		Invalid = -1,
		Wall,
		WallTopLeft,
		WallTopMiddle,
		WallTopRight,
		WallMiddleLeft,
		WallMiddle,
		WallMiddleRight,
		WallBottomLeft,
		WallBottomMiddle,
		WallBottomRight,
		Floor,
		Entry,
		Exit,
		FacadeLeft,
		FacadeMiddle,
		FacadeRight,
		Max
	};
	
	/// <summary>
	/// Logical representation of a block. Holds the node type (e.g. wall, floor etc),
	/// it's coordinates (a pointer to the node in a 2d array - not it's position on screen),
	/// it's position on screen and path finding variables.
	/// </summary>
	[System.Serializable]
	public class Node : IComparable
	{
		public NodeType nodeType { get; set; }
	
		public Vector2 coordinates { get; set; }

		public bool isOccupied { get; set;}

		public Vector2? Position { get; set; }
		
		public GameObject Cell { get; set; }
				
		// The cost to move into this node.
		public float GScore { get; set; }
				
		// Estimated cost to mvoe from this node to end node.
		public float HScore { get; set; }
				
		// Used when traversing a path.
		public Node Parent { get; set; }

		public bool IsObstacle {
			get { 
				return nodeType != NodeType.Floor; 
			} 
		}

		public Node () : this (Vector2.zero)
		{
		}
		
		public Node (Vector2 coordinates) : this (coordinates, NodeType.Invalid, false)
		{
		}


		public Node (Vector2 coordinates, NodeType state) : this (coordinates, state, false)
		{

		}

		public Node (Vector2 coordinates, NodeType state, bool isOccupied)
		{
			this.coordinates = coordinates;
			nodeType = state;
			this.isOccupied = false;

			HScore = 0f;
			GScore = 1f;
			Parent = null;
			Position = null;

		}

		/// <summary>
		/// Total score. Returns GScore + HScore
		/// </summary>
		public float GetFScore ()
		{
			return GScore + HScore;
		}


		public int CompareTo (object obj)
		{

			Node other = (Node)obj;

			if (this.HScore < other.HScore)
				return -1;

			if (this.HScore > other.HScore)
				return 1;

			return 0;
	
		}

	}
}
