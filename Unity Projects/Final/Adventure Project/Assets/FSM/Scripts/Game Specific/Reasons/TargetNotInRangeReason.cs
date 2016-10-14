using UnityEngine;
using System.Collections;

namespace AdventureGame.StateManagement
{
	public class TargetNotInRangeReason : FSMReason
	{
		private Transform m_Character;
		private FSM_Brain m_Controller;
		private Transform m_Target;
		private float m_Range;

		private static readonly string SCRIPT_NAME = typeof(TargetNotInRangeReason).Name;

		public TargetNotInRangeReason (FSM_Brain controller, Transform target,
		                            float range, GlobalStateData.FSMStateID goToState)
		{
			transition = GlobalStateData.FSMTransistion.ReachedTarget;

			this.goToState = goToState;
			m_Controller = controller;
			m_Character = controller.transform;
			m_Target = target;
			m_Range = range;


		}

		public override bool ChangeState ()
		{
			if (!CloseToObject (m_Target.position, m_Character.position, m_Range)) { // Player not in  range.
				Debug.Log(SCRIPT_NAME + ": switching state to: " + goToState);
				m_Controller.SetTransistion (transition);
				return true;
			}

			return false;
		}
	}
}
