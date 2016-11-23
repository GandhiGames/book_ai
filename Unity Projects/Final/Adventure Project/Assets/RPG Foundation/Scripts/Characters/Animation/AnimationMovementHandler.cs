using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent(typeof(MovementHandler), typeof(Animator))]
	public class AnimationMovementHandler : MonoBehaviour
	{
		private static readonly int SPEED_HASH = Animator.StringToHash ("Speed");
		private static readonly int INPUT_X = Animator.StringToHash ("Input X");
		private static readonly int INPUT_Y = Animator.StringToHash ("Input Y");

		private MovementHandler m_MovementHandler;
		private Animator m_Animator;

		void Awake()
		{
			m_Animator = GetComponent<Animator> ();
			m_MovementHandler = GetComponent<MovementHandler> ();
		}

		public void Update()
		{
			var velocity = m_MovementHandler.velocity;

			if (!Vector2.Equals (velocity, Vector2.zero)) {
				if (Mathf.Abs (velocity.x) > Mathf.Abs (velocity.y)) {
					m_Animator.SetFloat (INPUT_X, velocity.x > 0 ? 1f : -1f);
					m_Animator.SetFloat (INPUT_Y, 0f);
				} else if (velocity.y != 0f) {
					m_Animator.SetFloat (INPUT_X, 0f);
					m_Animator.SetFloat (INPUT_Y, velocity.y > 0 ? 1f : -1f);
				}
			}

			m_Animator.SetFloat (SPEED_HASH, velocity.magnitude);

		}
	}
}