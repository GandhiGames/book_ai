using UnityEngine;
using System.Collections;
using AdventureGame.ActionManagement;

namespace AdventureGame.StateManagement
{
	public class SeekTargetAction : FSMAction
	{

		private Transform m_Character;
		private Transform m_Target;
		private float m_SeekSpeed;
		private LayerMask m_ObstacleMask;
		private MovementHandler m_MovementHandler;
		private float m_Offset;


		public SeekTargetAction (Transform character, Transform target,
		                         MovementHandler movementHandler, float seekSpeed, 
		                         LayerMask obstacleMask, float offset = 0f)
		{
			m_Character = character;
			m_Target = target;
			m_SeekSpeed = seekSpeed;
			m_MovementHandler = movementHandler;
			m_ObstacleMask = obstacleMask;
			m_Offset = offset;
		}

		protected override bool OkToAct ()
		{
			return Vector2.Distance (m_Character.position, m_Target.position) > m_Offset;
		}

		protected override Command BuildAction ()
		{
			
			var heading = m_Target.position - m_Character.position;
			var distance = heading.magnitude;
			var dir = heading / distance;


			return new ObstacleAvoidanceMovementCommand (
				m_Character, 
				dir, 
				m_MovementHandler,
				m_SeekSpeed,
				m_ObstacleMask);
			
			
		}

	
	}
}
