using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class CharacterAnimatedShootListener : MonoBehaviour, AnimatedCharacterShootComponent
	{
		[Header ("Raise")]
		public Sprite[] raiseProjectileUpSprites;
		public Sprite[] raiseProjectileLeftSprites;
		public Sprite[] raiseProjectileDownSprites;
		public Sprite[] raiseProjectileRightSprites;

		[Header ("Release")]
		public Sprite releaseProjectileUpSprite;
		public Sprite releaseProjectileLeftSprite;
		public Sprite releaseProjectileDownSprite;
		public Sprite releaseProjectileRightSprite;

		[Header ("Get New Projectile")]
		public Sprite[] getNewProjectileUpSprites;
		public Sprite[] getNewProjectileLeftSprites;
		public Sprite[] getNewProjectileDownSprites;
		public Sprite[] getNewProjectileRightSprites;

		private SpriteRenderer m_renderer;

		void Awake ()
		{
			m_renderer = GetComponent<SpriteRenderer> ();
		}

		public void SetRaiseProjectileUp (int index)
		{
			m_renderer.sprite = raiseProjectileUpSprites [index];
		}

		public void SetRaiseProjectileLeft (int index)
		{
			m_renderer.sprite = raiseProjectileLeftSprites [index];
		}

		public void SetRaiseProjectileDown (int index)
		{
			m_renderer.sprite = raiseProjectileDownSprites [index];
		}

		public void SetRaiseProjectileRight (int index)
		{
			m_renderer.sprite = raiseProjectileRightSprites [index];
		}

		public void SetHoldUp ()
		{
			m_renderer.sprite = raiseProjectileUpSprites [raiseProjectileUpSprites.Length - 1];
		}

		public void SetHoldLeft ()
		{
			m_renderer.sprite = raiseProjectileLeftSprites [raiseProjectileLeftSprites.Length - 1];
			;
		}

		public void SetHoldDown ()
		{
			m_renderer.sprite = raiseProjectileDownSprites [raiseProjectileDownSprites.Length - 1];
			;
		}

		public void SetHoldRight ()
		{
			m_renderer.sprite = raiseProjectileRightSprites [raiseProjectileRightSprites.Length - 1];
			;
		}

		public void ReleaseProjectileUp ()
		{
			m_renderer.sprite = releaseProjectileUpSprite;
		}

		public void ReleaseProjectileLeft ()
		{
			m_renderer.sprite = releaseProjectileLeftSprite;
		}

		public void ReleaseProjectileDown ()
		{
			m_renderer.sprite = releaseProjectileDownSprite;
		}

		public void ReleaseProjectileRight ()
		{
			m_renderer.sprite = releaseProjectileRightSprite;
		}

		public void GetNewProjectileUp (int index)
		{
			m_renderer.sprite = getNewProjectileUpSprites [index];
		}

		public void GetNewProjectileLeft (int index)
		{
			m_renderer.sprite = getNewProjectileLeftSprites [index];
		}

		public void GetNewProjectileDown (int index)
		{
			m_renderer.sprite = getNewProjectileDownSprites [index];
		}

		public void GetNewProjectileRight (int index)
		{
			m_renderer.sprite = getNewProjectileRightSprites [index];
		}
	}
}