using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class CharacterAnimatedWalkListener : MonoBehaviour, AnimatedCharacterWalkComponent
	{
		public Sprite[] walkDownSprites;
		public Sprite[] walkLeftSprites;
		public Sprite[] walkRightSprites;
		public Sprite[] walkUpSprites;

		private SpriteRenderer m_renderer;

		void Awake ()
		{
			m_renderer = GetComponent<SpriteRenderer> ();
		}

		public void SetWalkDown (int index)
		{
			m_renderer.sprite = walkDownSprites [index];
		}

		public void SetWalkUp (int index)
		{
			m_renderer.sprite = walkUpSprites [index];
		}

		public void SetWalkLeft (int index)
		{
			m_renderer.sprite = walkLeftSprites [index];
		}

		public void SetWalkRight (int index)
		{
			m_renderer.sprite = walkRightSprites [index];
		}

	}
}
