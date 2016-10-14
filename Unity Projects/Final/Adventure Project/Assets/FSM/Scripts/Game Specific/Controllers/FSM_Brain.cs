using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AdventureGame.CaveGenerator;

namespace AdventureGame.StateManagement
{
	/// <summary>
	/// Base class for any character that will use the Finite-state machine. 
	/// Provides functionality to switch between states and call the current states update method.
	/// </summary>
	[RequireComponent (typeof(FSM))]
	public abstract class FSM_Brain : MonoBehaviour
	{
		public GlobalStateData.FSMStateID currentState;

		protected FSM stateMachine;
	
		// Every state is passed the players transform through their Reason and Act methods.
		protected Transform player;

		private NodeOccupancy occupiedNode;

		void Start()
		{
			if (stateMachine) {
				stateMachine.Reset ();
			} else {
				Init ();
			}
		}


		/// <summary>
		/// Initialises the statemachine and rewind (if not already).  
		/// Sets supportRewind to true if a rewind component is found and enabled.
		/// Calls ConstructFSM method, which setups a character specific state machine.
		/// </summary>
		void Init ()
		{
			if (!player) {
				InstantiatePlayerTransform ();
			}

			if (!stateMachine) {
				stateMachine = GetComponent<FSM> ();
				ConstructFSM ();	
				stateMachine.currentState.Enter ();
			}

			if (occupiedNode == null) {
				occupiedNode = new NodeOccupancy ();
			}

		}

		/// <summary>
		/// Using the object pool, a character is added and removed from the scene. WHen they are re-added to a
		/// scene it is important that the FSM is reset.
		/// </summary>
		void OnEnable ()
		{
			if (stateMachine) {
				stateMachine.Reset ();
			} else {
				Init ();
			}
		}

		/// <summary>
		/// Returns true if the player is initilised and in close proximity (set by SeekDistance)
		/// and either:
		/// 	Rewind is supported and is not currently eecuting (i.e. rewinding the scene)
		///		Rewind is not supported.
		/// </summary>
		private bool OkToAct ()
		{
			return player != null;
		}

		void Update ()
		{
			occupiedNode.Update (transform.position);
		}

		/// <summary>
		/// Instantiates player transform and then if it is ok to act, the current states
		/// reason and act methods are called. Further Information on these methods can be found in
		/// the FSMState class.
		/// </summary>
		void FixedUpdate ()
		{

						
			if (OkToAct ()) {
				stateMachine.currentState.Reason ();
				stateMachine.currentState.Act ();
			}

			// For debug purposes so you can see an NPC's current state in the inspector.
			currentState = stateMachine.currentState.id;
		}

		private void InstantiatePlayerTransform ()
		{
			var playerObj = GameObject.FindGameObjectWithTag ("Player"); 

			if (playerObj) {
				player = playerObj.transform;
			}
		}

		/// <summary>
		/// This performs a transistion from one state to another and is invoked in an individual states Reason method.
		/// </summary>
		public void SetTransistion (GlobalStateData.FSMTransistion tran)
		{
			stateMachine.PerformTransition (tran);
		}

		
		/// <summary>
		/// Constructs a character specfic state machine. This is abstract as each characters state machine will
		/// be different.
		/// </summary>
		protected abstract void ConstructFSM ();

	}
}
