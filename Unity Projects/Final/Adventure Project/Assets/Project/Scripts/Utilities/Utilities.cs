using System.Collections.Generic;
using UnityEngine;
using AdventureGame.CaveGenerator;

namespace AdventureGame
{
	public enum Direction
	{
		Up = 0,
		Left = 1,
		Down = 2,
		Right = 3
	}


	public class Utilities : Singleton<Utilities>
	{
		public float entryNotFound { get { return -1; } }

		public int sortingOrderBelowCharacter { get { return -100; } }

		public int sortingOrderAboveCharacter { get { return 100; } }

		public bool IsDebug = false;

		private static readonly string SCRIPT_NAME = typeof(Utilities).Name;

		private Vector3 LocalScaleOfNodes = Vector3.one;

		private Vector2? tileSize;

		public Vector2? TileSize { get { return tileSize; } }



		private Vector2[] m_Directions = new Vector2[] {
			new Vector2 (0, 1),
			new Vector2 (-1, 0),
			new Vector2 (0, -1),
			new Vector2 (1, 0)
		};

		public Vector2 GetDirectionVector (Direction dir)
		{
			return m_Directions [(int)dir];
		}

		/// <summary>
		/// Returns world position of a specified node 
		/// </summary>
		public Vector2 GetNodePosition (Node node)
		{
			if (!tileSize.HasValue) {
				tileSize = GridManager.instance.texturePack.GetSpriteSize (node.nodeType, LocalScaleOfNodes);
			}

			if (node.Position.HasValue) {
				return node.Position.Value;
			}
				
			node.Position = new Vector2 (node.coordinates.x * tileSize.Value.x + tileSize.Value.x / 2f, 
				node.coordinates.y * tileSize.Value.y + tileSize.Value.y / 2f); 

			return node.Position.Value;

		}


		/// <summary>
		/// Returns grid coordinates for a node at 
		/// </summary>
		public Vector2? GetGridCoordinateForPosition (Vector2 position)
		{	
			if (!tileSize.HasValue) {
				Debug.LogError (SCRIPT_NAME + ": tileSize needs to be initialised");
				return null;
			}
			return new Vector2 (position.x / tileSize.Value.x, position.y / tileSize.Value.y);
		}


		/// <summary>
		/// Increments n towards target based on acceleration. 
		/// Useful in bespoke physics engine to increment a characters position towards their toarget.
		/// </summary>
		public float IncrementTowards (float n, float target, float acc)
		{
			if (n == target) {
				return n;	
			} else {
				float dir = Mathf.Sign (target - n); // must n be increased or decreased to get closer to target
				n += acc * Time.deltaTime * dir;
				return (dir == Mathf.Sign (target - n)) ? n : target; // if n has now passed target then return target, otherwise return n
			}
		}


		public List<Ray2D> CreateRays (Vector2 position, Vector2 heading, float angle, int number)
		{
			var rayList = new List <Ray2D> ();

			heading.Normalize ();

			angle /= number;

			int leftCount = 0;
			int rightCount = 0;

			for (int i = 0; i < number; i++) {
				Ray2D ray;

				if (i < (number / 2)) {
					Vector2 dir = Quaternion.AngleAxis (-angle * ++leftCount, Vector3.forward) * heading;
					ray = new Ray2D (position, dir);

				} else if (i > (number / 2)) {
					Vector2 dir = Quaternion.AngleAxis (angle * ++rightCount, Vector3.forward) * heading;
					ray = new Ray2D (position, dir);

				} else {
					ray = new Ray2D (position, heading);

				}



				rayList.Add (ray);

			}


			return rayList;
		}

		public List<Ray2D> CreateRays (Vector2 position, Vector2 heading, float angle)
		{
			return CreateRays (position, heading, angle, 3);
		}

		public GameObject CreateContainer (GameObject owner, string name)
		{
			
			var obj = owner.transform.Find (name);

			if (obj != null) {
				return obj.gameObject;
			} 

			var container = new GameObject (name);
			container.transform.SetParent (owner.transform);
			return container;

		}

		/// <summary>
		/// Used to determine if an object with a specific mask lies between two positions.
		/// Can be used to determine if a character has a clean shot at the player.
		/// </summary> 
		public bool LayerInPath (Vector2 from, Vector2 to, LayerMask mask)
		{
			var heading = from - to;
			var distance = heading.magnitude;
			var direction = heading / distance;

			Ray2D ray = new Ray2D (to, direction);

			if (Utilities.instance.IsDebug) {
				Debug.DrawRay (ray.origin, ray.direction, Color.black);
			}


			return Physics2D.Raycast (ray.origin, ray.direction, distance, mask).collider != null;
		}

	}
}