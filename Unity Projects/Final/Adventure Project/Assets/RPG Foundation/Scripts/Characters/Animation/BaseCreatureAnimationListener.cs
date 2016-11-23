using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class BaseCreatureAnimationListener : MonoBehaviour 
	{
		public string creatureName;
		
		private static readonly string RESOURCE_PATH = "SpriteSheets/Creatures/";

		private SpriteRenderer m_renderer;
		private Sprite[] m_Sprites;

		void Awake()
		{
			m_renderer = GetComponent<SpriteRenderer>();
		}

		void Start()
		{
			m_Sprites = Resources.LoadAll<Sprite>(RESOURCE_PATH + creatureName);
		}

		public void SetIdle(Direction direction)
		{
			switch(direction)
			{
			case Direction.Up:
				m_renderer.sprite = m_Sprites [0];
				break;
			case Direction.Left:
				m_renderer.sprite = m_Sprites[3];
				break;
			case Direction.Down:
				m_renderer.sprite = m_Sprites[6];
				break;
			case Direction.Right:
				m_renderer.sprite = m_Sprites[9];
				break;
			}
		}

		public void WalkUp(int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex];
		}

		public void WalkLeft(int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex + 3];
		}

		public void WalkDown(int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex + 6];
		}

		public void WalkRight(int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex + 9];
		}
	}
}