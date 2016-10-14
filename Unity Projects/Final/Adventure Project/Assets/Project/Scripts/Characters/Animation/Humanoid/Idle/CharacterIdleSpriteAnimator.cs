using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public class CharacterIdleSpriteAnimator : MonoBehaviour, AnimatedCharacterIdleComponent
	{
		public string spriteSheetName;

		private Sprite[] m_idleSprites;
		private SpriteRenderer m_renderer;

		void Awake ()
		{
			m_renderer = GetComponent<SpriteRenderer> ();
		}

		void Start ()
		{
			m_idleSprites = Resources.LoadAll<Sprite> (spriteSheetName);
		}

		public void SetIdleDown ()
		{
			m_renderer.sprite = m_idleSprites [18];
		}

		public void SetIdleUp ()
		{
			m_renderer.sprite = m_idleSprites [0];
		}

		public void SetIdleLeft ()
		{
			m_renderer.sprite = m_idleSprites [9];
		}

		public void SetIdleRight ()
		{
			m_renderer.sprite = m_idleSprites [27];
		}

	}
}
