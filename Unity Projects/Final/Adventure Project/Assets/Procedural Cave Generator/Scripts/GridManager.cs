using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace AdventureGame.CaveGenerator
{
	/// <summary>
	/// Singleton. Manages the grid of cells that represent the level. 
	/// </summary>
	[RequireComponent (typeof(TexturePack))]
	[RequireComponent (typeof(NodeClusterManager))]
	[RequireComponent (typeof(Utilities))]
	[ExecuteInEditMode, System.Serializable]
	public class GridManager : Singleton<GridManager>
	{
		public int Seed = 1;
		
		[Header ("Grid Size and Scale")]
		public Vector2
			gridSize;
		public Vector3 GridScale = Vector3.one;
		
		[Header ("Wall and Background")]
		public int
			NumberOfTransistionSteps = 0;
		public bool IsConnectedEnvironment = false;
		
		[Range (0, 1)]
		public float
			ChanceToBecomeWall = 0.40f;

		[Range (0, 8)]
		public int
			BackgroundToWallConversion = 4;

		[Range (0, 8)]
		public int
			WallToBackgroundConversion = 3;

		[Header ("Layer Masks")]
		public LayerMask
			WallLayer;
		public LayerMask BackgroundLayer;


		[Header ("Game Objects")]
		public GameObject
			CellPrefab;

		private GameObject parent;

		public NodeList grid { get; set; }

		public Node startNode { get; set; }

		public Node endNode { get; set; }

		public bool initialised {
			get {
				return grid != null;
			}
		}

        public List<Node> FloorNodes { get { return grid.GetNodesOfType(NodeType.Floor); } }

        private static readonly string FOREGROUND_SORTING_LAYER = "Foreground";
		private static readonly string GROUND_SORTING_LAYER = "Ground";
		private static readonly string WALL_SORTING_LAYER = "Above Ground";

        private NodeClusterManager m_NodeClusterManager;
		private TexturePack[] m_TexturePacks;
		private int m_CurrentTexturePack = 0;
		private int m_BackgroundLayer;
		private int m_WallLayer;


		public TexturePack texturePack {
			get {
				return m_TexturePacks [m_CurrentTexturePack];
			}
		}

		void Start ()
		{
			if (!CellPrefab) {
				Debug.LogError ("Cell prefab not set");
				enabled = false;
			}
		}

		public void SetupContainer ()
		{
			if (parent == null) {
				parent = Utilities.instance.CreateContainer (gameObject, "Generated Dungeon");
			}
		}

		public void ReGenerate ()
		{
			if (Utilities.instance.IsDebug)
				Debug.Log ("Destroying old environment if present");

			SetupContainer ();

			DestroyEnvironment ();
			Generate ();
		}

		public void Generate ()
		{
			if (EnvironmentGenerated ()) {
				return;
			}

			if (!m_NodeClusterManager) {
				m_NodeClusterManager = GetComponent<NodeClusterManager> ();
			}

			m_TexturePacks = GetTexturePacks ();
			
			string backgroundLayerName = BackgroundLayer.MaskToNames ().Length > 0 ? BackgroundLayer.MaskToNames () [0] : "Nothing";
			m_BackgroundLayer = LayerMask.NameToLayer (backgroundLayerName);
			
			string wallLayerName = WallLayer.MaskToNames ().Length > 0 ? WallLayer.MaskToNames () [0] : "Nothing";
			m_WallLayer = LayerMask.NameToLayer (wallLayerName);

			Random.InitState (Seed);

			float beginGeneratingTime = Time.realtimeSinceStartup;


			if (Utilities.instance.IsDebug)
				Debug.Log ("Generating environment");
			InitialiseEnvironment ();

			for (int step = 0; step < NumberOfTransistionSteps; step++) {
				if (Utilities.instance.IsDebug)
					Debug.Log ("Performing transition step: " + (step + 1));
				PerformTransistionStep ();
			}
				
			if (IsConnectedEnvironment) {
				m_NodeClusterManager.IdentifyClusters (grid, gridSize); 

				m_NodeClusterManager.ConnectClusters ();
			} 

			if (Utilities.instance.IsDebug)
				Debug.Log ("Placing Entrance and Exit");
		
			RemoveExtraneous ();
		
			// Identify clusters so they can be connected using Path manager 
			m_NodeClusterManager.IdentifyClusters (grid, gridSize);

		



			DefineWallTypes ();
			BuildFacade ();

			PlaceEntranceAndExit ();


	

			/*if(texturePack.ContainsBridgeSprites ())
			{
				CellPositionData[] bridgeChecks = new CellPositionData[] {
					new CellPositionData (new Vector2 (-1, 0), NodeType.WallBottomRight), 
					new CellPositionData (new Vector2 (0, 0), NodeType.Floor, texturePack.bridge[0]), 
					new CellPositionData (new Vector2 (1, 0), NodeType.Floor, texturePack.bridge[1]),
					new CellPositionData (new Vector2 (2, 0), NodeType.Floor, texturePack.bridge[2]),
					new CellPositionData (new Vector2 (3, 0), NodeType.WallBottomLeft)
				};

				new CellPlacement (CellPrefab, grid,
					parent, bridgeChecks, 3).Place ();

				CellPositionData[] wallChecks = new CellPositionData[] {
					new CellPositionData (new Vector2 (-1, 0), NodeType.Floor), 
					new CellPositionData (new Vector2 (0, 0), NodeType.FacadeLeft, texturePack.bridge[0]),
					new CellPositionData (new Vector2 (1, 0), NodeType.FacadeMiddle, texturePack.bridge[1]),
					new CellPositionData (new Vector2 (2, 0), NodeType.FacadeRight, texturePack.bridge[2])
				};

				new CellPlacement (CellPrefab, grid, 
					 parent, wallChecks, 3).Place (); 

				CellPositionData[] wallChecksTwo = new CellPositionData[] {
					new CellPositionData (new Vector2 (-1, 0), NodeType.Floor), 
					new CellPositionData (new Vector2 (0, 0), NodeType.FacadeLeft, texturePack.bridge[0]),
					new CellPositionData (new Vector2 (1, 0), NodeType.FacadeMiddle, texturePack.bridge[1]),
					new CellPositionData (new Vector2 (2, 0), NodeType.FacadeMiddle, texturePack.bridge[1]),
					new CellPositionData (new Vector2 (3, 0), NodeType.FacadeRight, texturePack.bridge[2])
				};

				new CellPlacement (CellPrefab, grid, 
					parent, wallChecksTwo, 3).Place (); 
			} */

			GenerateEnvironment ();

			m_NodeClusterManager.CalculateMainCluster ();

			if (Utilities.instance.IsDebug)
				Debug.Log ("Generated environment in " + (Time.realtimeSinceStartup - beginGeneratingTime) + " seconds"); 
			
		}

		public void RandomSeed ()
		{
			Seed = Random.Range (0, 1000000);
		}

		public void LoadNextTexturePack ()
		{
			m_CurrentTexturePack = (m_CurrentTexturePack + 1) % m_TexturePacks.Length;
		}


		public bool DestroyEnvironment ()
		{
			
			bool destroyed = EnvironmentGenerated ();

			parent.transform.ClearImmediate ();

			grid = null;

			return destroyed;
		}


		private bool EnvironmentGenerated ()
		{
			return parent.transform.childCount > 0;
		}

		private void InitialiseEnvironment ()
		{
			grid = new NodeList (gridSize);

			for (int x = 0; x < gridSize.x; x++) {
				for (int y = 0; y < gridSize.y; y++) {

	
					Vector2 coord = new Vector2 (x, y);

					NodeType cellType;

					if (IsEdge (coord)) {
						cellType = NodeType.Wall;
					} else {
						cellType = Random.value < ChanceToBecomeWall ? NodeType.Wall : NodeType.Floor;
					}

					grid.Add (new Node (coord, cellType));
				}
			}
		}

		private void PerformTransistionStep ()
		{
			
			NodeList newGrid = new NodeList (gridSize);
			
			for (int x = 0; x < gridSize.x; x++) {
				for (int y = 0; y < gridSize.y; y++) {

					Vector2 coord = new Vector2 (x, y);

					int neighbourCount = CountTileWallNeighbours (coord);
					
					Node oldCell = grid.GetNodeFromGridCoordinate (coord);
					Node newCell = new Node (coord);
					
					if (oldCell.nodeType == NodeType.Wall) {
						newCell.nodeType = (neighbourCount < WallToBackgroundConversion) ? NodeType.Floor : NodeType.Wall;
					} else {
						newCell.nodeType = (neighbourCount > BackgroundToWallConversion) ? NodeType.Wall : NodeType.Floor;
					}

					newGrid.Add (newCell);
				}
			}
			
			grid = newGrid;
			
			
		}



		private void RemoveExtraneous ()
		{
			for (int i = 0; i < 2; i++) {
				for (int x = 0; x < gridSize.x; x++) {
					for (int y = 0; y < gridSize.y; y++) {

						var coord = new Vector2 (x, y);

						var node = grid.GetNodeFromGridCoordinate (coord);

						if (node.nodeType != NodeType.Wall)
							continue;
					
						if (IsExtraneousCell (node.coordinates)) {
							node.nodeType = NodeType.Floor;
						}
					}
				}
			}

			RemoveLoneCells ();
		}

		private void RemoveLoneCells ()
		{
			for (int x = 0; x < gridSize.x; x++) {
				for (int y = 0; y < gridSize.y; y++) {
					
					var coord = new Vector2 (x, y);

					var node = grid.GetNodeFromGridCoordinate (coord);
					
					if (node.nodeType != NodeType.Wall)
						continue;

					if (IsLoneCell (node.coordinates)) {
						node.nodeType = NodeType.Floor;
					}
				}
			}
		}

		private void DefineWallTypes ()
		{
			for (int x = 0; x < gridSize.x; x++) {
				for (int y = 0; y < gridSize.y; y++) {
					var coord = new Vector2 (x, y);
					var node = grid.GetNodeFromGridCoordinate (coord);

					if (node.nodeType == NodeType.Wall) {
						DefineWallType (node);
					}
				}
			}
		}

		private void BuildFacade ()
		{
			for (int x = 0; x < gridSize.x; x++) {
				for (int y = 0; y < gridSize.y; y++) {
					var coord = new Vector2 (x, y);
					var node = grid.GetNodeFromGridCoordinate (coord);

				

					if (node.nodeType == NodeType.WallBottomLeft) {
						var coordBelow = new Vector2 (x, y - 1);
						SetNodeState (coordBelow, NodeType.FacadeLeft);
					} else if (node.nodeType == NodeType.WallBottomMiddle) {
						var coordBelow = new Vector2 (x, y - 1);
						SetNodeState (coordBelow, NodeType.FacadeMiddle);
					} else if (node.nodeType == NodeType.WallBottomRight) {
						var coordBelow = new Vector2 (x, y - 1);
						SetNodeState (coordBelow, NodeType.FacadeRight);
					}

				}
			}
		}

		private void SetNodeState (Vector2 coord, NodeType state)
		{
			var node = grid.GetNodeFromGridCoordinate (coord);

			if (node != null) {
				node.nodeType = state;
			}
		}

		/*	private bool IsFloorCell (Vector2 coord)
		{
			var cellBelow = (Grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y - 1), NodeType.Wall));
			var floorLeft = (Grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x - 1, coord.y), NodeType.Floor));
			var floorRight = (Grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x + 1, coord.y), NodeType.Floor));
			var floorAbove = (Grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y + 1), NodeType.Floor));

			return cellBelow && floorLeft && floorRight && floorAbove;

		}*/

		private bool IsLoneCell (Vector2 coord)
		{
			var cellBelow = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y - 1), NodeType.Wall));
			var cellLeft = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x - 1, coord.y), NodeType.Wall));
			var cellRight = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x + 1, coord.y), NodeType.Wall));
			var cellAbove = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y + 1), NodeType.Wall));

			if (!cellLeft && !cellRight && !cellBelow && !cellAbove)
				return true;

			return false;
		}

		private bool IsExtraneousCell (Vector2 coord)
		{
			var cellBelow = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y - 1), NodeType.Wall));
			var cellLeft = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x - 1, coord.y), NodeType.Wall));
			var cellRight = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x + 1, coord.y), NodeType.Wall));
			var cellAbove = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y + 1), NodeType.Wall));

			var floorBelow = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y - 1), NodeType.Floor));
			var floorLeft = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x - 1, coord.y), NodeType.Floor));
			var floorRight = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x + 1, coord.y), NodeType.Floor));
			var floorAbove = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y + 1), NodeType.Floor));
	

			if (!cellLeft && !cellRight && !cellBelow && !cellAbove)
				return true;

			if (!cellLeft && !cellAbove && !cellRight)
				return true;

			if (!cellLeft && !cellAbove && !cellBelow)
				return true;

			if (!cellBelow && !cellAbove && !cellRight)
				return true;

			if (!cellBelow && !cellLeft && !cellRight)
				return true;

			if (floorLeft && floorRight && cellAbove && cellBelow)
				return true;

			if (floorAbove && floorBelow && cellRight && cellLeft)
				return true;

			
			return false;
		}

		private void GenerateEnvironment ()
		{
			
			for (int x = 0; x < gridSize.x; x++) {
				for (int y = 0; y < gridSize.y; y++) {

					var coord = new Vector2 (x, y);
					var node = grid.GetNodeFromGridCoordinate (coord);
					var cell = ObjectManager.instance.GetObject (CellPrefab, Utilities.instance.GetNodePosition (node)); 

					var collider = GetCellCollider (cell);
					var renderer = GetCellsRenderer (cell);

					// Reset collider recieved from object pool.
					collider.size = new Vector2 (1f, 1f);
					collider.offset = new Vector2 (0f, 0f);


					if (node.nodeType == NodeType.Floor || node.nodeType == NodeType.Entry || node.nodeType == NodeType.Exit) {
						collider.enabled = true;
						collider.isTrigger = true;

						if (m_BackgroundLayer >= 0 && m_BackgroundLayer <= 31) {
							cell.layer = m_BackgroundLayer;
						}

						renderer.sortingLayerName = GROUND_SORTING_LAYER;
					} else {
						if (node.nodeType == NodeType.FacadeLeft ||
						    node.nodeType == NodeType.FacadeMiddle ||
						    node.nodeType == NodeType.FacadeRight) {
							collider.enabled = true;
							collider.isTrigger = true;
							collider.size = new Vector2 (1f, 0.5f);
							collider.offset = new Vector2 (0f, 0.5f);
						} else {
							collider.enabled = true;
							collider.isTrigger = false;
						}

						renderer.sortingLayerName = WALL_SORTING_LAYER;

						if (node.nodeType == NodeType.WallTopLeft
						    || node.nodeType == NodeType.WallTopMiddle || node.nodeType == NodeType.WallTopRight) {
							collider.enabled = false;
							collider.isTrigger = true;
							renderer.sortingLayerName = FOREGROUND_SORTING_LAYER;
						}
			

						if (m_WallLayer >= 0 && m_WallLayer <= 31) {
							cell.layer = m_WallLayer;
						}
					} 
												
					UpdateNodeSprite (cell, node.nodeType);
					node.Position = cell.transform.position;
					node.Cell = cell;
					cell.transform.SetParent (parent.transform);

					cell.SetActive (true);
				
				}
			}

		}

		private BoxCollider2D GetCellCollider (GameObject cell)
		{
			var collider2D = cell.GetComponent<BoxCollider2D> ();

			if (!collider2D) {
				collider2D = cell.AddComponent<BoxCollider2D> ();
			}

			return collider2D;
		}

		private SpriteRenderer GetCellsRenderer (GameObject cell)
		{
			var renderer = cell.GetComponent<SpriteRenderer> ();

			if (!renderer) {
				renderer = cell.AddComponent<SpriteRenderer> ();
			}

			return renderer;
		}

		private void DefineWallType (Node node)
		{
			var coord = node.coordinates;
			var floorBelow = grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y - 1), NodeType.Floor);
			var floorLeft = grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x - 1, coord.y), NodeType.Floor);
			var floorRight = grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x + 1, coord.y), NodeType.Floor);
			var floorAbove = grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y + 1), NodeType.Floor);

			var floorAboveLeft = grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x - 1, coord.y + 1), NodeType.Floor);

			if (!floorAboveLeft && floorLeft && !floorRight && !floorAbove && floorBelow) { 
				node.nodeType = NodeType.WallBottomLeft;
			} else if (!floorBelow && !floorLeft && floorAbove && floorRight) {
				node.nodeType = NodeType.WallTopRight;
			} else if (!floorBelow && !floorLeft && floorAbove && !floorRight) {
				node.nodeType = NodeType.WallTopMiddle;
			} else if (!floorBelow && floorLeft && floorAbove && !floorRight) {
				node.nodeType = NodeType.WallTopLeft;
			} else if (floorBelow && !floorLeft && !floorAbove && floorRight) {
				node.nodeType = NodeType.WallBottomRight;
			} else if (floorBelow && !floorLeft && !floorAbove && !floorRight) {
				node.nodeType = NodeType.WallBottomMiddle;
			} else if (floorBelow && floorLeft && !floorAbove && !floorRight) {
				node.nodeType = NodeType.WallBottomLeft;
			} else if (!floorBelow && !floorAbove && floorLeft && !floorRight) {
				node.nodeType = NodeType.WallMiddleLeft;
			} else if (!floorBelow && !floorAbove && !floorLeft && floorRight) {
				node.nodeType = NodeType.WallMiddleRight;
			} else {
				node.nodeType = NodeType.WallMiddle;
			}

		}

		private bool IsOutsideWall (Node wallNode)
		{
			if (wallNode.coordinates.x == 0) {
				return true;
			}

			if (wallNode.coordinates.y == 0) {
				return true;
			}

			if (wallNode.coordinates.x == gridSize.x - 1) {
				return true;
			}

			if (wallNode.coordinates.y == gridSize.y - 1) {
				return true;
			}
			
			return false;
		}


		private void UpdateNodeSprite (GameObject node, NodeType nodeType)
		{
			SpriteRenderer spriteRenderer = node.GetComponent<SpriteRenderer> ();

			if (spriteRenderer) {
				spriteRenderer.sprite = texturePack.GetSpriteFromCellType (nodeType);  
			}
				
		}

		private bool IsEdge (Vector2 coordinate)
		{
			return ((int)coordinate.x == 0 ||
			(int)coordinate.x == (int)gridSize.x - 1 ||
			(int)coordinate.y == 0 ||
			(int)coordinate.y == (int)gridSize.y - 1);
		}

		public int CountTileWallNeighbours (Vector2 coord)
		{

			int wallCount = 0;

			int x = (int)coord.x;
			int y = (int)coord.y;

			for (int i = -1; i < 2; i++) {
				for (int j = -1; j < 2; j++) {

					if (i == 0 && j == 0) {
						continue;
					}
					
					Vector2 neighborCoordinate = new Vector2 (x + i, y + j);

					if ((!grid.IsValidCoordinate (neighborCoordinate) || (grid.GetNodeFromGridCoordinate (neighborCoordinate).nodeType != NodeType.Floor))) {
						wallCount += 1;
					}					
				}
			}
			return wallCount;
		}

		private void PlaceEntranceAndExit ()
		{

			/*var entranceCell = GetRandomFloorNode ();

			if (entranceCell == null) {
				return;
			}
		
			grid.GetNodeFromGridCoordinate (entranceCell.Coordinates).NodeState = NodeType.Entry;

			startNode = entranceCell; */

			Node exitCell = GetRandomFloorNode (); // GetFloorNodeMinDistanceFromStartNode (minDistanceBetweenStartAndEnd, entranceCell);

			grid.GetNodeFromGridCoordinate (exitCell.coordinates).nodeType = NodeType.Exit;

			endNode = exitCell;
		}

		private bool IsValidEntrance (Node node)
		{
			var coord = node.coordinates;
			var floorAbove = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y + 1), NodeType.Floor));
			var cellBelow = (grid.ContainsNodeTypeAtPosition (new Vector2 (coord.x, coord.y - 1), NodeType.Wall));

			return floorAbove && cellBelow;
		}

		private Node GetMinDistanceNodeFromCluster (List<Node> cluster, float minDistance, Node startNode)
		{
			int mainClusterCount = cluster.Count - 1;

			float currentDistance = 0f;

			Node node = null;

			int count = 0;
			do {
				// Do not want an infinite loop, where you cannot find a node far enough away so we decrement the distance until a node is found.
				count++;
				if (count == 10) { 
					if (minDistance > 0.05f) {
						minDistance -= (minDistance * 0.1f);
					}

					count = 0;
				}

				node = cluster [(int)(Random.value * mainClusterCount)];		

				int a = (int)(node.coordinates.x - startNode.coordinates.x);
				int b = (int)(node.coordinates.y - startNode.coordinates.y);
				currentDistance = Mathf.Sqrt (a * a + b * b);

			} while (currentDistance < minDistance); 

			return node;
		}



		private Node GetMaxDistanceNodeFromCluster (List<Node> cluster, float maxDistance, Node startNode)
		{
			int mainClusterCount = cluster.Count - 1;
			
			float currentDistance = 0f;
			
			Node node = null;
			
			int count = 0;
			do {
				// Do not want an infinite loop, where you cannot find a node far enough away so we decrement the distance until a node is found.
				count++;
				if (count == 10) { 
					if (maxDistance < 40f) {
						maxDistance += (maxDistance * 0.1f);
					}
					
					count = 0;
				}

				node = cluster [(int)(Random.value * mainClusterCount)];		
				
				int a = (int)(node.coordinates.x - startNode.coordinates.x);
				int b = (int)(node.coordinates.y - startNode.coordinates.y);
				currentDistance = Mathf.Sqrt (a * a + b * b);
				
			} while (currentDistance > maxDistance); 
			
			return node;
		}

		public Node GetFloorNodeMaxDistanceFromStartNode (float maxDistance, Node startNode)
		{
			return GetMaxDistanceNodeFromCluster (FloorNodes, maxDistance, startNode);
		}

		public Node GetFloorNodeMinDistanceFromStartNode (float minDistance, Node startNode)
		{
			return GetMinDistanceNodeFromCluster (FloorNodes, minDistance, startNode);		
		}

		public Node GetRandomBackgroundNode ()
		{
			List<Node> cluster = m_NodeClusterManager.MainCluster.Nodes;
					
			return cluster [(int)(Random.value * cluster.Count - 1)];
			
		}

		public List<Node> GetBackgroundNodes ()
		{
			return m_NodeClusterManager.MainCluster.Nodes;
		}

		public Node GetRandomFloorNode ()
		{
			if (FloorNodes.Count == 0)
				return null;

			return FloorNodes [Random.Range (0, FloorNodes.Count)];
		}

		public Node GetRandomFloorNode (Node exclusion)
		{
			if (FloorNodes.Count == 0)
				return null;

			int attempts = 20;

			Node node = exclusion;

			do {
				node = FloorNodes [Random.Range (0, FloorNodes.Count)];

				if (attempts-- <= 0) {
					return null;
				}
			} while(node == exclusion);

			return node;
		}

		public Node GetRandomFloorNodeAdjacentToNodeType (NodeType type, Direction direction)
		{
			if (FloorNodes.Count == 0)
				return null;

			Vector2 dir = Utilities.instance.GetDirectionVector (direction);

			foreach (var node in FloorNodes) {
				if (grid.ContainsNodeTypeAtPosition (
					   new Vector2 (node.coordinates.x + dir.x, node.coordinates.y + dir.y), type)) {
					return node;
				}
			}

			return null;
		}

		public void RemoveFloorNode (Node node)
		{
			FloorNodes.Remove (node);
		}

		private TexturePack[] GetTexturePacks ()
		{
			var texturePacks = GetComponents<TexturePack> ();
			
			
			if (texturePacks.Length == 0) {
				Debug.LogError ("No texture packs found");
				enabled = false;
			}
			
			return texturePacks;
			
		}


	}
}
