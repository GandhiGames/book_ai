using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class CharacterAnimatedIdleListener : MonoBehaviour, AnimatedCharacterIdleComponent
	{
		public Sprite idleDown;
		public Sprite idleLeft;
		public Sprite idleRight;
		public Sprite idleUp;

		private SpriteRenderer m_renderer;

		void Awake ()
		{
			m_renderer = GetComponent<SpriteRenderer> ();
		}

		public void SetIdleDown ()
		{
			m_renderer.sprite = idleDown;
		}

		public void SetIdleUp ()
		{
			m_renderer.sprite = idleUp;
		}

		public void SetIdleLeft ()
		{
			m_renderer.sprite = idleLeft;
		}

		public void SetIdleRight ()
		{
			m_renderer.sprite = idleRight;
		}

	}
}