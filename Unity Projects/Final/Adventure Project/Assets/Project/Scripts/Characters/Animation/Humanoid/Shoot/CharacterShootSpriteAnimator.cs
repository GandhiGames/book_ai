using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public class CharacterShootSpriteAnimator : MonoBehaviour, AnimatedCharacterShootComponent
	{
		public string spriteSheetName;

		private Sprite[] m_bowSprites;
		private SpriteRenderer m_renderer;

		void Awake ()
		{
			m_renderer = GetComponent<SpriteRenderer> ();
		}

		void Start ()
		{
			m_bowSprites = Resources.LoadAll<Sprite> (spriteSheetName);
		}

		public void SetRaiseProjectileUp (int index)
		{
			m_renderer.sprite = m_bowSprites [index];
		}

		public void SetRaiseProjectileLeft (int index)
		{
			m_renderer.sprite = m_bowSprites [index + 13];
		}

		public void SetRaiseProjectileDown (int index)
		{
			m_renderer.sprite = m_bowSprites [index + 26];
		}

		public void SetRaiseProjectileRight (int index)
		{
			m_renderer.sprite = m_bowSprites [index + 39];
		}

		public void SetHoldUp ()
		{
			m_renderer.sprite = m_bowSprites [9];
		}

		public void SetHoldLeft ()
		{
			m_renderer.sprite = m_bowSprites [21];
		}

		public void SetHoldDown ()
		{
			m_renderer.sprite = m_bowSprites [34];
		}

		public void SetHoldRight ()
		{
			m_renderer.sprite = m_bowSprites [47];
		}

		public void ReleaseProjectileUp ()
		{
			m_renderer.sprite = m_bowSprites [10];
		}

		public void ReleaseProjectileLeft ()
		{
			m_renderer.sprite = m_bowSprites [22];
		}

		public void ReleaseProjectileDown ()
		{
			m_renderer.sprite = m_bowSprites [35];
		}

		public void ReleaseProjectileRight ()
		{
			m_renderer.sprite = m_bowSprites [48];
		}

		public void GetNewProjectileUp (int index)
		{
			m_renderer.sprite = m_bowSprites [index + 10];
		}

		public void GetNewProjectileLeft (int index)
		{
			m_renderer.sprite = m_bowSprites [index + 23];
		}

		public void GetNewProjectileDown (int index)
		{
			m_renderer.sprite = m_bowSprites [index + 36];
		}

		public void GetNewProjectileRight (int index)
		{
			m_renderer.sprite = m_bowSprites [index + 49];
		}
	}
}