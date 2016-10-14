using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class CharacterAnimatedSwingListener : MonoBehaviour, AnimatedCharacterSwingComponent
	{
		public Sprite[] swingUpSprites;
		public Sprite[] swingLeftSprites;
		public Sprite[] swingDownSprites;
		public Sprite[] swingRightSprites;

		private SpriteRenderer m_renderer;

		void Awake ()
		{
			m_renderer = GetComponent<SpriteRenderer> ();
		}

		public void SetSwingUp (int index)
		{
			m_renderer.sprite = swingUpSprites [index];
		}

		public void SetSwingLeft (int index)
		{
			m_renderer.sprite = swingLeftSprites [index];
		}

		public void SetSwingDown (int index)
		{
			m_renderer.sprite = swingDownSprites [index];
		}

		public void SetSwingRight (int index)
		{
			m_renderer.sprite = swingRightSprites [index];
		}
	}
}