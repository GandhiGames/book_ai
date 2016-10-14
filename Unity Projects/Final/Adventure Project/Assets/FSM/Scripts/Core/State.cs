using System.Collections.Generic;

namespace AdventureGame.StateManagement
{
	public class State : FSMState
	{
		private List<FSMAction> m_Actions;
		private List<FSMReason> m_Reasons;
		private int m_CurrentAction = 0;


		public State (GlobalStateData.FSMStateID stateid, FSMAction action, FSMReason reason)
			:this(stateid, action.ToList (), reason.ToList ())
		{
		}
	
		public State (GlobalStateData.FSMStateID stateid, FSMAction action, List<FSMReason> reasons)
			:this(stateid, action.ToList (), reasons)
		{
		}

		public State (GlobalStateData.FSMStateID stateid, List<FSMAction> actions, FSMReason reason)
			:this(stateid, actions, reason.ToList ())
		{
		}


		public State (GlobalStateData.FSMStateID stateid, List<FSMAction> actions, List<FSMReason> reasons)
		{
			this.id = stateid;
			this.m_Actions = actions;
			this.m_Reasons = reasons;

			foreach (var reason in reasons) {
				AddTransition (reason.transition, reason.goToState);
			}
		}

		public override void Enter ()
		{
			m_CurrentAction = 0;

			foreach (var action in m_Actions) {
				action.Enter ();
			}

			foreach (var reason in m_Reasons) {
				reason.Enter ();
			}

		}

		public override void Exit ()
		{
			foreach (var action in m_Actions) {
				action.Exit ();
			}

			foreach (var reason in m_Reasons) {
				reason.Exit ();
			}
		}

		public override void Reason ()
		{
			foreach (var reason in m_Reasons) {
				if (reason.ChangeState ()) {
					break;
				}
			}
		}

		public override void Act ()
		{

			var action = m_Actions [m_CurrentAction];
			m_CurrentAction = (m_CurrentAction + 1) % m_Actions.Count;

			var command = action.GetAction ();

			if(command != null){
				command.Execute ();
			}

		}

		protected override bool OkToAct ()
		{
			return true;
		}

	}
}
