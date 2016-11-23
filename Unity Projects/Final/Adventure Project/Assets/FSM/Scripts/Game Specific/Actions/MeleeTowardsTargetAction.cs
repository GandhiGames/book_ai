using UnityEngine;
using System.Collections;
using AdventureGame.ActionManagement;

namespace AdventureGame.StateManagement
{
	public class MeleeTowardsTargetAction : FSMAction
	{
		private static readonly int ATTACK_HASH = Animator.StringToHash ("Attack");
		
		private Transform m_Character;
		private Transform m_Target;

		private float m_DelayBetweenAttacks;
		private int m_DamageAmount;
		private float m_KnockbackForce;
		private float m_Range;
		private LayerMask m_HitMask;
		private Animator m_Animator;
		private float m_CurrentDelay;

		public MeleeTowardsTargetAction (Transform character, Transform target, float delayBetweenAttacks,
		                                 int damageAmount, float knockbackForce, 
			float range, LayerMask hitMask, Animator animator)
		{
			m_Character = character;
			m_Target = target;
			m_DelayBetweenAttacks = delayBetweenAttacks;
			m_DamageAmount = damageAmount;
			m_KnockbackForce = knockbackForce;
			m_Range = range;
			m_HitMask = hitMask;
			m_Animator = animator;
		}

		public override void Enter ()
		{
			m_CurrentDelay = 0f;
		}

		protected override bool OkToAct ()
		{
			m_CurrentDelay += Time.deltaTime;

			if (m_CurrentDelay >= m_DelayBetweenAttacks) {
				m_CurrentDelay = 0f;
				return true;
			}

			return false;
		}

		protected override Command BuildAction ()
		{
			var heading = m_Target.position - m_Character.position;
			var distance = heading.magnitude;
			var direction = heading / distance;

			return new MeleeCommand (m_Character, direction, 
				m_DamageAmount, m_KnockbackForce, m_Range, m_HitMask, StartAttackAnimation);

		}

		private void StartAttackAnimation()
		{
			m_Animator.SetTrigger (ATTACK_HASH);
		}
	}
}
