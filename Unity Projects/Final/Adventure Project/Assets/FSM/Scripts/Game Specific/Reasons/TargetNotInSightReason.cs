using UnityEngine;
using System.Collections;

namespace AdventureGame.StateManagement
{
	public class TargetNotInSightReason : FSMReason
	{
		private static readonly string SCRIPT_NAME = typeof(TargetNotInSightReason).Name;
	
		private FSM_Brain m_Controller;
		private Transform m_Target;
		private Transform m_Character;
		private LayerMask m_ObstacleMask;

		public TargetNotInSightReason (FSM_Brain controller, Transform target,
			LayerMask obstacleMask, GlobalStateData.FSMStateID goToState)
		{
			transition = GlobalStateData.FSMTransistion.TargetNotInSight;
			this.goToState = goToState;
		
			m_Character = controller.transform;
			m_Target = target;
			m_ObstacleMask = obstacleMask;
			m_Controller = controller;

		}

		public override bool ChangeState ()
		{
			if (Utilities.instance.LayerInPath (m_Target.position, m_Character.position, m_ObstacleMask)) {
				Debug.Log(SCRIPT_NAME + ": switching state to: " + goToState);
				m_Controller.SetTransistion (transition);
				return true;
			}

			return false;
		}

	}
}
