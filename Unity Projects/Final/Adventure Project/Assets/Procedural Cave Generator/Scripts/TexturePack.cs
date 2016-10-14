using UnityEngine;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	/// <summary>
	/// Singleton. Provides a centralised location for block tetures. 
	/// Provides a method to retrieve a texture based on a node type.
	/// </summary>
	public class TexturePack : MonoBehaviour
	{
		public Sprite[] topRow = new Sprite[3];
		public Sprite[] middleRow = new Sprite[3];
		public Sprite[] bottomRow = new Sprite[3];

		public Sprite[] facade = new Sprite[3];

		public Sprite floor;

		/// <summary>
		/// Returns a texture based on a node type.
		/// </summary>
		public Sprite GetSpriteFromCellType (NodeType cellType)
		{

			Sprite sprite = null;

			switch (cellType) {
			case NodeType.WallTopLeft:
				sprite = topRow [0];
				break;
			case NodeType.WallTopMiddle:
				sprite = topRow [1];
				break;
			case NodeType.WallTopRight:
				sprite = topRow [2];
				break;
			case NodeType.WallMiddleLeft:
				sprite = middleRow [0];
				break;
			case NodeType.WallMiddle:
				sprite = middleRow [1];
				break;
			case NodeType.WallMiddleRight:
				sprite = middleRow [2];
				break;
			case NodeType.WallBottomLeft:
				sprite = bottomRow [0];
				break;
			case NodeType.WallBottomMiddle:
				sprite = bottomRow [1];
				break;
			case NodeType.WallBottomRight:
				sprite = bottomRow [2]; 
				break; 
			case NodeType.Floor:
				sprite = floor;
				break;
			case NodeType.Entry:
				sprite = floor;
				break;
			case NodeType.FacadeLeft:
				sprite = facade [0];
				break;
			case NodeType.FacadeMiddle:
				sprite = facade [1];
				break;
			case NodeType.FacadeRight:
				sprite = facade [2];
				break;
			default:
				sprite = floor;
				break;
			}

			if (!sprite) {
				Debug.LogError (cellType + " not set");
			}

			return sprite;

		}

		public Vector2 GetSpriteSize (NodeType cellType, Vector2 localScale)
		{
			Sprite sprite = GetSpriteFromCellType (cellType);

			return new Vector2 (sprite.bounds.size.x * localScale.x, sprite.bounds.size.y * localScale.y);
		}

	}


}
