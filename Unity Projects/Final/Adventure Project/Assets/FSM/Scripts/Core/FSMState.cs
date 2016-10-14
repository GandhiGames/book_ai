using UnityEngine;
using System.Collections.Generic;

namespace AdventureGame.StateManagement
{
	/// <summary>
	/// Abstract base class for a state
	/// Provides functionality for storing and retrieving transitions between states.
	/// </summary>
	public abstract class FSMState
	{
		public GlobalStateData.FSMStateID id { get; protected set; }

		// Stores the transition and the stateid of the state to transistion to.
		protected Dictionary<GlobalStateData.FSMTransistion, GlobalStateData.FSMStateID> m_TransitionMap =
			new Dictionary<GlobalStateData.FSMTransistion, GlobalStateData.FSMStateID> ();

		private static readonly string SCRIPT_NAME = typeof(FSMState).Name;

		/// <summary>
		/// Initialises rewind handler if present. Can be null as the ability to rewind is optional.
		/// </summary>
		public FSMState ()
		{

		}

		/// <summary>
		/// Called when entering the state (before Reason and Act). 
		/// Place any initialisation code here.
		/// </summary>
		public abstract void Enter ();

		/// <summary>
		/// Called when leaving a state. 
		/// Place any clean-up code here.
		/// </summary>
		public abstract void Exit ();

		/// <summary>
		/// Decides if the state should transition to another on its list.
		/// While the state is active this method is called each time step.
		/// </summary>
		public abstract void Reason ();

		/// <summary>
		/// Place the states implementation of the behaviour in this method.
		/// While the state is active this method is called each time step.
		/// </summary>
		public abstract void Act ();

		/// <summary>
		/// When implemented should return true when it is ok for the character to perform the actions in the Act method.
		/// Place behaviour specfic tests in this method.
		/// </summary>
		protected abstract bool OkToAct ();


		/// <summary>
		/// Adds a transition, stateID pair to the transition map. Every transition called 
		/// in the states Reason method should have a corresponding state id in the map.
		/// </summary>
		public void AddTransition (GlobalStateData.FSMTransistion transition, GlobalStateData.FSMStateID id)
		{
			// Ensure valid transition.
			if (transition == GlobalStateData.FSMTransistion.None || id == GlobalStateData.FSMStateID.None) {
				Debug.LogWarning (SCRIPT_NAME + " : Null transition not allowed");
				return;
			}

			// Ensure transition not already present in map.
			if (m_TransitionMap.ContainsKey (transition)) {
				Debug.LogWarning (SCRIPT_NAME + ": transition is already inside the map | " + transition);
				return;
			}

			m_TransitionMap.Add (transition, id);
		}

		/// <summary>
		/// Deletes a transition, stateID pair from the transition map.
		/// </summary>
		public void DeleteTransition (GlobalStateData.FSMTransistion transition)
		{
			// Ensures valid transition.
			if (transition == GlobalStateData.FSMTransistion.None) {
				Debug.LogError (SCRIPT_NAME + ": None transition is not allowed");
				return;
			}

			// Ensure map contains transition before attempting delete.
			if (m_TransitionMap.ContainsKey (transition)) {
				m_TransitionMap.Remove (transition);
				return;
			}

			Debug.LogError (SCRIPT_NAME + ": transition passed was not on this State´s List | " + transition);
		}

		public void ClearCurrentTransistions ()
		{
			m_TransitionMap.Clear ();
		}

		/// <summary>
		/// This method returns the state that would be transitioned into based on the FSMTransition.
		/// </summary>
		public GlobalStateData.FSMStateID GetOutputState (GlobalStateData.FSMTransistion transition)
		{
			// Ensures valid transition.
			if (transition == GlobalStateData.FSMTransistion.None) {
				Debug.LogError (SCRIPT_NAME + ": None transition is not allowed");
				return GlobalStateData.FSMStateID.None;
			}

			// Ensure map contains transition before returning new state.
			if (m_TransitionMap.ContainsKey (transition)) {
				return m_TransitionMap [transition];
			}

			Debug.LogError (SCRIPT_NAME + ": transition passed to the State was not on the list | " + transition);

			// Transition not in map.
			return GlobalStateData.FSMStateID.None;

		}
	}
}
