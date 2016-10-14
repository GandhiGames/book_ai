using UnityEngine;
using System.Collections;

namespace AdventureGame.StateManagement
{
	[RequireComponent(typeof(MovementHandler))]
	public class FSM_Worm : FSM_Brain
	{
		public LayerMask obstacleMask;

		public float seekSpeed = 150f;
		
		protected override void ConstructFSM ()
		{
			// Get necessary components.
			var movementHandler = GetComponent<MovementHandler> ();

			// Build FSM actions.
			var idleAction = new IdleAction ();

			// Build FSM reasons.
			var playerInSightReason = new TargetInSightReason (this, player, obstacleMask, GlobalStateData.FSMStateID.SeekTarget);

			// Create states.
			// Idle action: do nothing until player in sight
			stateMachine.AddState (new State(GlobalStateData.FSMStateID.Idle, idleAction, playerInSightReason));

			// Seek player.



			
		}
	}
}