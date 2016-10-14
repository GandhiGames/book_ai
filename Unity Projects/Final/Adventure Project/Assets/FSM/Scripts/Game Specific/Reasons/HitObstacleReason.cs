using UnityEngine;
using System.Collections;

namespace AdventureGame.StateManagement
{
	public class HitObstacleReason : FSMReason
	{
		private static readonly string SCRIPT_NAME = typeof(HitObstacleReason).Name;

		private static readonly float RAY_LENGTH = 0.45f;
		private static readonly float RAY_ANGLE = 160;
		private static readonly int RAY_COUNT = 5;

		private FSM_Brain m_Controller;
		private Transform m_Character;
		private MovementHandler m_Handler;
		private LayerMask m_ObstacleMask;

		public HitObstacleReason (FSM_Brain controller, MovementHandler movementHandler, 
		                         LayerMask obstacleMask,
		                         GlobalStateData.FSMStateID goToState)
		{
			transition = GlobalStateData.FSMTransistion.HitObstacle;

			m_Controller = controller;
			m_Character = controller.transform;
			m_Handler = movementHandler;
			m_ObstacleMask = obstacleMask;
			this.goToState = goToState;
		}

		public override bool ChangeState ()
		{
			var rayList = Utilities.instance.CreateRays (m_Character.position, 
				              m_Handler.velocity.normalized, RAY_ANGLE, RAY_COUNT);

			foreach (Ray2D ray in rayList) {

				RaycastHit2D[] hits = Physics2D.RaycastAll (ray.origin, ray.direction, 
					                      RAY_LENGTH, m_ObstacleMask); //, 1 << NameManager.instance.ObstacleMask);


				if (Utilities.instance.IsDebug) {
					Debug.DrawLine (ray.origin, ray.origin + ray.direction * RAY_LENGTH, Color.red);
				}

				foreach (var hit in hits) {

					if (hit.transform.GetInstanceID () != m_Character.GetInstanceID ()) {	
						Debug.Log(SCRIPT_NAME + ": switching state to: " + goToState);
						m_Controller.SetTransistion (transition);
						return true;
					}
				}
			}

			return false;
		}
	}
}