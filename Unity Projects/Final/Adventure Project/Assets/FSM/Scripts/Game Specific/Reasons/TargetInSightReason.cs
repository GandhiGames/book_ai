using UnityEngine;
using System.Collections;

namespace AdventureGame.StateManagement
{
	public class TargetInSightReason : FSMReason
	{

		private static readonly string SCRIPT_NAME = typeof(TargetInSightReason).Name;

		private FSM_Brain m_Controller;
		private Transform m_Character;
		private Transform m_Target;
		private LayerMask m_ObstacleMask;

		public TargetInSightReason (FSM_Brain controller, Transform target,
		                                LayerMask obstacleMask, GlobalStateData.FSMStateID goToState)
		{
			transition = GlobalStateData.FSMTransistion.TargetInSight;
			this.goToState = goToState;

			m_Character = controller.transform;
			m_ObstacleMask = obstacleMask;
			m_Target = target;
			m_Controller = controller;		
		}

		public override bool ChangeState ()
		{
			if (!Utilities.instance.LayerInPath (m_Target.position, m_Character.position, m_ObstacleMask)) {
				Debug.Log (SCRIPT_NAME + ": switching state to: " + goToState);
				m_Controller.SetTransistion (transition);
				return true;
			}

			return false;
		}
	}
}
