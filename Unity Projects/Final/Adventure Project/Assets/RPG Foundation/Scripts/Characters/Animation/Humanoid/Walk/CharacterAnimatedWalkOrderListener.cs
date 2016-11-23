using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(SpriteRenderer))]
	public class CharacterAnimatedWalkOrderListener : MonoBehaviour, AnimatedCharacterWalkComponent
	{
		public int downSpriteOrder;
		public int leftSpriteOrder;
		public int rightSpriteOrder;
		public int upSpriteOrder;

		private SpriteRenderer m_renderer;

		void Awake ()
		{
			m_renderer = GetComponent<SpriteRenderer> ();
		}

		public void SetWalkDown (int index)
		{
			m_renderer.sortingOrder = downSpriteOrder;
		}

		public void SetWalkUp (int index)
		{
			m_renderer.sortingOrder = upSpriteOrder;
		}

		public void SetWalkLeft (int index)
		{
			m_renderer.sortingOrder = leftSpriteOrder;
		}

		public void SetWalkRight (int index)
		{
			m_renderer.sortingOrder = rightSpriteOrder;
		}
	}
}