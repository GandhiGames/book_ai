using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public class CharacterSwingSpriteAnimator : MonoBehaviour, AnimatedCharacterSwingComponent
	{
		public string spriteSheetName;

		private Sprite[] m_swingSprites;
		private SpriteRenderer m_renderer;

		void Awake ()
		{
			m_renderer = GetComponent<SpriteRenderer> ();
		}

		void Start ()
		{
			m_swingSprites = Resources.LoadAll<Sprite> (spriteSheetName);
		}

		public void SetSwingUp (int index)
		{
			m_renderer.sprite = m_swingSprites [index + 1];
		}

		public void SetSwingLeft (int index)
		{
			m_renderer.sprite = m_swingSprites [index + 7];
		}

		public void SetSwingDown (int index)
		{
			m_renderer.sprite = m_swingSprites [index + 13];
		}

		public void SetSwingRight (int index)
		{
			m_renderer.sprite = m_swingSprites [index + 19];
		}
	}
}