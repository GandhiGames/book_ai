using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(Animator), typeof(PlayerController))]
	public class PlayerAnimationController : MonoBehaviour
	{
		private static readonly int SPEED_HASH = Animator.StringToHash ("Speed");
		private static readonly int INPUT_X_HASH = Animator.StringToHash ("Input X");
		private static readonly int INPUT_Y_HASH = Animator.StringToHash ("Input Y");
		private static readonly int RAISE_PROJECTILE_HASH = Animator.StringToHash ("Raise Projectile");
		private static readonly int RELEASE_PROJECTILE_HASH = Animator.StringToHash ("Release Projectile");
		private static readonly int SWING_HASH = Animator.StringToHash ("Swing");

		private Animator m_animator;
		private PlayerController m_playerController;

		void Awake ()
		{
			m_animator = GetComponent<Animator> ();
			m_playerController = GetComponent<PlayerController> ();
		}

		void OnEnable ()
		{
			m_playerController.OnVelocityUpdated += UpdateWalkAnimation;
			m_playerController.OnRaiseProjectileRequested += UpdateRaiseProjectileAnimation;
			m_playerController.OnReleaseProjectileRequested += StartReleaseProjectileAnimation;
			m_playerController.OnMeleeRequested += UpdateSwingAnimation;
		}

		void OnDisable ()
		{
			m_playerController.OnVelocityUpdated -= UpdateWalkAnimation;
			m_playerController.OnRaiseProjectileRequested -= UpdateRaiseProjectileAnimation;
			m_playerController.OnReleaseProjectileRequested -= StartReleaseProjectileAnimation;
			m_playerController.OnMeleeRequested -= UpdateSwingAnimation;
		}

		public void UpdateWalkAnimation (Vector2 newVelocity)
		{
			bool updateInput = newVelocity != Vector2.zero;
		
			m_animator.SetFloat (SPEED_HASH, newVelocity.sqrMagnitude);

			if (updateInput) {
				m_animator.SetFloat (INPUT_X_HASH, newVelocity.x);
				m_animator.SetFloat (INPUT_Y_HASH, newVelocity.y);
			}
		}

		public void UpdateRaiseProjectileAnimation (bool shouldRaise)
		{
			if (shouldRaise) {
				m_animator.ResetTrigger (RELEASE_PROJECTILE_HASH);
			}

			m_animator.SetBool (RAISE_PROJECTILE_HASH, shouldRaise);
		}

		public void StartReleaseProjectileAnimation ()
		{
			m_animator.SetTrigger (RELEASE_PROJECTILE_HASH);
		}

		public void UpdateSwingAnimation ()
		{
			m_animator.SetTrigger (SWING_HASH);
		}
	}
}
