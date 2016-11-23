using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[System.Serializable]
	public class CharacterAnimationSpriteSetActive
	{
		public bool shouldEnable = true;
		public int animationFrameToSetEnabled = 0;

		private SpriteRenderer m_renderer;

		public void AddRenderer (SpriteRenderer renderer)
		{
			m_renderer = renderer;
		}

		public void CalculateAndSetEnabled (int animationIndex)
		{
			if (ShouldSetEnabled (animationIndex)) {
				SetRendererEnabled ();
			}
		}

		private bool ShouldSetEnabled (int animationIndex)
		{
			return animationIndex == animationFrameToSetEnabled;
		}

		private void SetRendererEnabled ()
		{
			if (m_renderer != null) {
				m_renderer.enabled = shouldEnable;
			}
		}
	}
}
