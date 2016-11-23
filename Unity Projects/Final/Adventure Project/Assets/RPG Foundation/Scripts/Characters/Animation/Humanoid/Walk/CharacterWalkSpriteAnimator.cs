using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public class CharacterWalkSpriteAnimator : MonoBehaviour, AnimatedCharacterWalkComponent
	{
		public string spriteSheetName;

		private Sprite[] m_walkSprites;
		private SpriteRenderer m_renderer;

		void Awake ()
		{
			m_renderer = GetComponent<SpriteRenderer> ();
		}

		void Start ()
		{
			m_walkSprites = Resources.LoadAll<Sprite> (spriteSheetName);
		}

		public void SetWalkDown (int index)
		{
			m_renderer.sprite = m_walkSprites [index + 19];
		}

		public void SetWalkUp (int index)
		{
			m_renderer.sprite = m_walkSprites [index + 1];
		}

		public void SetWalkLeft (int index)
		{
			m_renderer.sprite = m_walkSprites [index + 10];
		}

		public void SetWalkRight (int index)
		{
			m_renderer.sprite = m_walkSprites [index + 28];
		}
	}
}
