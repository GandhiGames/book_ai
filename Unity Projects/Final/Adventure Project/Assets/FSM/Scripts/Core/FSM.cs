using UnityEngine;
using System.Collections.Generic;

namespace AdventureGame.StateManagement
{
	public class FSM : MonoBehaviour
	{
		public FSMState previousState { get; private set; }

		public FSMState currentState { get; private set; }

		private static readonly string SCRIPT_NAME = typeof(FSM).Name;

		private List<FSMState> m_FsmStates = new List<FSMState> ();
		private FSMState m_DefaultState;

		void OnDisable ()
		{
			if (currentState != null)
				currentState.Exit ();
		}

		public void AddState (FSMState state)
		{

			if (state == null) {
				Debug.LogWarning (SCRIPT_NAME + ": null state not allowed");
				return;
			}

			// First State inserted is also the Initial state
			//   the state the machine is in when the simulation begins
			if (m_FsmStates.Count == 0) {
				m_FsmStates.Add (state);
				currentState = state;
				m_DefaultState = state;
				return;
			}

			// Add the state to the List if it´s not inside it
			foreach (FSMState tmpState in m_FsmStates) {
				if (state.id == tmpState.id) {
					Debug.LogError (SCRIPT_NAME + ": Trying to add a state that was already inside the list, " + state.id);
					return;
				}
			}

			//If no state in the current then add the state to the list
			m_FsmStates.Add (state);
		}

		public void DeleteState (GlobalStateData.FSMStateID stateID)
		{

			if (stateID == GlobalStateData.FSMStateID.None) {
				Debug.LogWarning (SCRIPT_NAME + ": no state id");
				return;
			}


			// Search the List and delete the state if it´s inside it
			foreach (FSMState state in m_FsmStates) {
				if (state.id == stateID) {
					m_FsmStates.Remove (state);
					return;
				}
			}

			Debug.LogError (SCRIPT_NAME + ": The state passed was not on the list");

		}

		public void PerformTransition (GlobalStateData.FSMTransistion trans)
		{
			// Check for NullTransition before changing the current state
			if (trans == GlobalStateData.FSMTransistion.None) {
				Debug.LogError (SCRIPT_NAME + ": Null transition is not allowed");
				return;
			}

			// Check if the currentState has the transition passed as argument
			GlobalStateData.FSMStateID id = currentState.GetOutputState (trans);
			if (id == GlobalStateData.FSMStateID.None) {
				Debug.LogError (SCRIPT_NAME + ": Current State does not have a target state for this transition");
				return;
			}


			// Update the currentStateID and currentState		
			//currentStateID = id;
			foreach (FSMState state in m_FsmStates) {
				if (state.id == id) {
					// Store previous state and call exit method.
					previousState = currentState;
					previousState.Exit ();

					// Update current state and call enter method.
					currentState = state;
					currentState.Enter ();

					break;
				}
			}
		}

		public void ClearStates ()
		{
			m_FsmStates.Clear ();
		}

		public void Reset ()
		{
			currentState = m_DefaultState;
			if (currentState != null) {
				currentState.Enter ();
			}
		}

	}
}

