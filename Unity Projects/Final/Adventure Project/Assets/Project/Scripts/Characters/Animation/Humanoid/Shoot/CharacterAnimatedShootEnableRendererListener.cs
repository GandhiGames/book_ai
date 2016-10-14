using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class CharacterAnimatedShootEnableRendererListener : MonoBehaviour, 
																AnimatedCharacterShootComponent
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

		public void SetRaiseProjectileUp (int index)
		{
			spriteAnimationSpriteEnabler.CalculateAndSetEnabled (index);
		}

		public void SetRaiseProjectileLeft (int index)
		{
			spriteAnimationSpriteEnabler.CalculateAndSetEnabled (index);
		}

		public void SetRaiseProjectileDown (int index)
		{
			spriteAnimationSpriteEnabler.CalculateAndSetEnabled (index);
		}

		public void SetRaiseProjectileRight (int index)
		{
			spriteAnimationSpriteEnabler.CalculateAndSetEnabled (index);
		}

		public void SetHoldUp ()
		{
		}

		public void SetHoldLeft ()
		{
		}

		public void SetHoldDown ()
		{
		}

		public void SetHoldRight ()
		{
		}

		public void ReleaseProjectileUp ()
		{
		}

		public void ReleaseProjectileLeft ()
		{
		}

		public void ReleaseProjectileDown ()
		{
		}

		public void ReleaseProjectileRight ()
		{
		}

		public void GetNewProjectileUp (int index)
		{
		}

		public void GetNewProjectileLeft (int index)
		{
		}

		public void GetNewProjectileDown (int index)
		{
		}

		public void GetNewProjectileRight (int index)
		{
		}

	}
}