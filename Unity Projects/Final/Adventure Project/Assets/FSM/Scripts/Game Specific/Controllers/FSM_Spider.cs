using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AdventureGame.CaveGenerator;

namespace AdventureGame.StateManagement
{
	/// <summary>
	/// Attach to any character who you wish to have the following behaviour:
	///		Wanders around the map, following a path to a random location (when no other state is true)
	///		Digs (when an obstacle is in the way)
	/// 	Seeks and attacks player (when player attacks them)
	/// 	Seeks and picks up collectible (when collectible in sight)
	///
	/// This state machine creates a thief character, who will wander around the map and steal any collectibles they see.
	/// They are mostly peacefuly and will only attack when they are attacked.
	/// </summary>
	[RequireComponent(typeof(MovementHandler), typeof(WaypointManager), typeof(Animator))]
	public class FSM_Spider : FSM_Brain
	{
		[Header("Layer Masks")]
		public LayerMask obstacleMask;
		public LayerMask avoidanceMask;
		public LayerMask hitMask;

		[Header("Movement")]
		public float followPathSpeed = 120f;
		public float seekSpeed = 150f;

		[Header("Attack")]
		public float delayBetweenAttacks = 1f;
		public float attackDamage = 1f;
		public float attackKnockbackForce = 200f;
		public float attackRange = 2f;


		/// <summary>
		/// Construct a state mahcine to provide the behaviour outlined in the class comment.
		/// Individual states are created and transitions added to them. These transitions are
		/// used in the states Reason method.
		/// Once all states and transitions have been created the states are added to the state machine,
		/// which will handle transitions.
		/// </summary>
		protected override void ConstructFSM ()
		{
			// Get necessary components.
			var movementHandler = GetComponent<MovementHandler> ();
			var animator = GetComponent<Animator> ();
			var waypointManager = GetComponent<WaypointManager> ();

			// Build FSM actions.
			var followPathAction = new FollowPathToRandomAction (transform, followPathSpeed, avoidanceMask, waypointManager, movementHandler);
			var seekPlayerAction = new SeekTargetAction (transform, player, movementHandler, seekSpeed, avoidanceMask, attackRange * 0.8f);
			var attackPlayerAction = new MeleeTowardsTargetAction (transform, player, delayBetweenAttacks, 
				attackDamage, attackKnockbackForce, attackRange, hitMask, animator);

			// Build FSM reasons.
			var playerInSightReason = new TargetInSightReason (this, player, obstacleMask, GlobalStateData.FSMStateID.SeekTarget);
			var playerNotInSightReason = new TargetNotInSightReason (this, player, obstacleMask, GlobalStateData.FSMStateID.FollowPathToTarget);
			var playerInRangeReason = new TargetInRangeReason (this, player, attackRange, GlobalStateData.FSMStateID.AttackTarget);
			var playerNotInRangeReason = new TargetNotInRangeReason (this, player, attackRange, GlobalStateData.FSMStateID.SeekTarget);
			//var hitObstacleReason = new HitObstacleReason (this, movementHandler, obstacleMask, GlobalStateData.FSMStateID.FollowPathToTarget);


			// Create states.
			// Follow path ro random tile.
			var findPlayerActions = new List<FSMAction> () { followPathAction };
			var findPlayerReasons = new List<FSMReason> () { playerInSightReason };
			stateMachine.AddState (new State (GlobalStateData.FSMStateID.FollowPathToTarget, findPlayerActions, findPlayerReasons));

			// Seek to players position.
			var seekPlayerActions = new List<FSMAction> () { seekPlayerAction };
			var seekPlayerReasons = new List<FSMReason> () { playerInRangeReason, playerNotInSightReason };
			stateMachine.AddState (new State (GlobalStateData.FSMStateID.SeekTarget, seekPlayerActions, seekPlayerReasons));

			// Attack player.
			var attackActions = new List<FSMAction> (){ seekPlayerAction, attackPlayerAction };
			var attackPlayerReasons = new List<FSMReason> () { playerNotInRangeReason };
			stateMachine.AddState (new State (GlobalStateData.FSMStateID.AttackTarget, attackActions, attackPlayerReasons));	
				
		}

	}
}
