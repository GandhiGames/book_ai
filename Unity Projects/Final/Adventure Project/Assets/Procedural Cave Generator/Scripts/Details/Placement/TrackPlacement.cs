using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AdventureGame.CaveGenerator
{
	public class TrackPlacement : DetailsPlacement
	{
		public PathManager pathManager;
		public GameObject prefab;
		public Sprite[] horizontalTrack = new Sprite[3];
		public Sprite[] verticalTrack = new Sprite[3];
		public Sprite[] trackCorners = new Sprite[4];

		private enum TrackDirection
		{
			Up,
			Down,
			Left,
			Right,
			None
		}


		protected override bool PlaceOnCurrentLevel ()
		{
			if (!GridManager.instance.initialised) {
				return false;
			}

			GridManager.instance.grid.ClearNodeList (NodeType.Floor);

			Node start = GridManager.instance.GetRandomFloorNode ();
			Node end = GridManager.instance.GetRandomFloorNode (start);

			List<Node> path = pathManager.GetShortestPath (start, end, 100f, false);

			for (int i = 0; i < path.Count; i++) {
				var node = path [i];

				Vector2 position = Utilities.instance.GetNodePosition (node);

				var track = ObjectManager.instance.GetObject (prefab, position);

				var renderer = track.GetComponent<SpriteRenderer> ();

				renderer.sprite = GetTrackSprite (i == 0 ? null : path[i - 1], node, i == path.Count - 1 ? null : path[i + 1]);

				track.transform.SetParent (container.transform);

				GridManager.instance.RemoveFloorNode (node);
			}

			return true;
		}

		private Sprite GetTrackSprite (Node previousNode, Node node, Node nextNode)
		{
			TrackDirection previousTrackDir = TrackDirection.None;

			if (previousNode != null) {
				previousTrackDir = GetRelativeTrackDirection (node, previousNode);
			}

			TrackDirection nextTrackDir = TrackDirection.None;

			if (nextNode != null) {
				nextTrackDir = GetRelativeTrackDirection (node, nextNode);
			}

			if(previousTrackDir == TrackDirection.None){
				if (nextTrackDir == TrackDirection.Up) {
					return verticalTrack [0];
				} else if (nextTrackDir == TrackDirection.Down) {
					return verticalTrack [2];
				} else if (nextTrackDir == TrackDirection.Left) {
					return horizontalTrack [2];
				} else {
					return horizontalTrack [0];
				}
			} else if(nextTrackDir == TrackDirection.None){
				if (previousTrackDir == TrackDirection.Up) {
					return verticalTrack [0];
				} else if (previousTrackDir == TrackDirection.Down) {
					return verticalTrack [2];
				} else if (previousTrackDir == TrackDirection.Left) {
					return horizontalTrack [2];
				} else {
					return horizontalTrack [0];
				}
			}

			if ((previousTrackDir == TrackDirection.Left && nextTrackDir == TrackDirection.Right)
				|| (previousTrackDir == TrackDirection.Right && nextTrackDir == TrackDirection.Left)) {
				return horizontalTrack[1];
			} else if ((previousTrackDir == TrackDirection.Up && nextTrackDir == TrackDirection.Down)
				||  (previousTrackDir == TrackDirection.Down && nextTrackDir == TrackDirection.Up)){
				return verticalTrack[1];
			}


			if (previousTrackDir == TrackDirection.Left && nextTrackDir == TrackDirection.Down) {
				return trackCorners [3];
			} else if (previousTrackDir == TrackDirection.Left && nextTrackDir == TrackDirection.Up) {
				return trackCorners [1];
			} else if (previousTrackDir == TrackDirection.Right && nextTrackDir == TrackDirection.Down) {
				return trackCorners [2];
			} else if (previousTrackDir == TrackDirection.Right && nextTrackDir == TrackDirection.Up) {
				return trackCorners [0];
			} 

			if (previousTrackDir == TrackDirection.Down && nextTrackDir == TrackDirection.Left) {
				return trackCorners [3];
			} else if (previousTrackDir == TrackDirection.Down && nextTrackDir == TrackDirection.Right) {
				return trackCorners [2];
			} else if (previousTrackDir == TrackDirection.Up && nextTrackDir == TrackDirection.Left) {
				return trackCorners [1];
			} else if (previousTrackDir == TrackDirection.Up && nextTrackDir == TrackDirection.Right) {
				return trackCorners [0];
			} 


			return horizontalTrack[1];
		}

		private TrackDirection GetRelativeTrackDirection (Node first, Node second)
		{
			Vector2 firstCoord = first.coordinates;
			Vector2 secondCoord = second.coordinates;

			var cellBelow = new Vector2 (firstCoord.x, firstCoord.y - 1);
			var cellLeft = new Vector2 (firstCoord.x - 1, firstCoord.y);
			var cellRight = new Vector2 (firstCoord.x + 1, firstCoord.y);

			if (secondCoord == cellBelow) {
				return TrackDirection.Down;
			} else if (secondCoord == cellLeft) {
				return TrackDirection.Left;
			} else if (secondCoord == cellRight) {
				return TrackDirection.Right;
			} else {
				return TrackDirection.Up;
			}
		}
	}
}
