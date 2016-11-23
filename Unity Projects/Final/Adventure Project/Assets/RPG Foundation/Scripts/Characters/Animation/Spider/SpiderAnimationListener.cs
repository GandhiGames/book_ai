using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class SpiderAnimationListener : MonoBehaviour
	{
		private static readonly string RESOURCE_PATH = "SpriteSheets/Creatures/Spiders/LPC_spider_";

		private SpriteRenderer m_renderer;
		private Sprite[] m_Sprites;

		void Awake ()
		{
			m_renderer = GetComponent<SpriteRenderer> ();
		}

		void Start ()
		{
			m_Sprites = Resources.LoadAll<Sprite> (RESOURCE_PATH + Random.Range (0, 11));
		}

		public void SetIdle (Direction direction)
		{
			switch (direction) {
			case Direction.Up:
				m_renderer.sprite = m_Sprites [0];
				break;
			case Direction.Left:
				m_renderer.sprite = m_Sprites [10];
				break;
			case Direction.Down:
				m_renderer.sprite = m_Sprites [20];
				break;
			case Direction.Right:
				m_renderer.sprite = m_Sprites [30];
				break;
			}
		}

		public void AttackUp (int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex];
		}

		public void AttackLeft (int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex + 10];
		}

		public void AttackDown (int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex + 20];
		}

		public void AttackRight (int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex + 30];
		}

		public void WalkUp (int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex + 5];
		}

		public void WalkLeft (int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex + 15];
		}

		public void WalkDown (int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex + 25];
		}

		public void WalkRight (int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex + 35];
		}

		public void Death (int animationIndex)
		{
			m_renderer.sprite = m_Sprites [animationIndex + 40];
		}
	}
}