using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class CharacterAnimatedSwingEnableRendererListener : MonoBehaviour, AnimatedCharacterSwingComponent
	{
		public CharacterAnimationSpriteSetActive spriteAnimationSpriteEnabler;

		private SpriteRenderer m_renderer;

		void Awake ()
		{
			m_renderer = GetComponent<SpriteRenderer> ();
		}

		void Start ()
		{
			spriteAnimationSpriteEnabler.AddRenderer (m_renderer);
		}

		public void SetSwingUp (int index)
		{
			spriteAnimationSpriteEnabler.CalculateAndSetEnabled (index);
		}

		public void SetSwingLeft (int index)
		{
			spriteAnimationSpriteEnabler.CalculateAndSetEnabled (index);
		}

		public void SetSwingDown (int index)
		{
			spriteAnimationSpriteEnabler.CalculateAndSetEnabled (index);
		}

		public void SetSwingRight (int index)
		{
			spriteAnimationSpriteEnabler.CalculateAndSetEnabled (index);
		}
	}
}
