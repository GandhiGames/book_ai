using UnityEngine;
using System;
using System.Collections;

namespace AdventureGame
{
	public class PlayerController : MonoBehaviour
	{
		public enum ControllerType
		{
			None = 0,
			Keyboard = 100,
			Controller = 200
		}

		public ControllerType controllerType;
		public float movementSpeedMultiplier = 1f;
		public LayerMask obstacleMask;

		public Action<Vector2> OnVelocityUpdated;
		public Action OnMeleeRequested;

		public Action<bool> OnRaiseProjectileRequested;
		public Action OnReleaseProjectileRequested;

		private static readonly float OBSTACLE_CHECK_RANGE = 0.5f;
		private static readonly float OBSTACLE_CHECK_RANGE_DOWN = 1f;

		private enum MovementDirection
		{
			Horizontal,
			Vertical
		}
			
		private PlayerControllerType m_controller;
		private Vector2 m_velocity;
		private Vector2 m_previousVelocity;
		private MovementHandler m_Movement;

		void Awake ()
		{
			switch (controllerType) {
			case ControllerType.Keyboard:
				m_controller = gameObject.AddComponent<PlayerKeyboardController> ();
				break;
			case ControllerType.Controller:
				m_controller = gameObject.AddComponent<PlayerGamepadController> ();
				break;
			}
				
			m_Movement = GetComponent<MovementHandler> ();
		}

		void Start ()
		{
			if (m_controller == null) {
				Debug.LogWarning ("No controller type selected for player, disabling input");
				enabled = false;
			}

			m_velocity = Utilities.instance.GetDirectionVector (Direction.Down);
		}

		void Update ()
		{
            if (m_Movement.movementEnabled)
            {
                HandleMovement();
            }

			HandleProjectiles ();

			HandleMelee ();
		}

		private void HandleMovement ()
		{
			m_previousVelocity = m_velocity;
			m_velocity = m_controller.GetVelocity ();

			DisableDiagonalMovement ();

			float range = GetRaycastRangeBasedOnMovementDirection ();

			if (ObstacleInPath (range)) {
				m_velocity = Vector2.zero;
			}
		}

		private void DisableDiagonalMovement ()
		{
			if (Mathf.Abs (m_velocity.x) > Mathf.Abs (m_velocity.y)) {
				m_velocity.y = 0f;
			} else {
				m_velocity.x = 0f;
			}
		}

		private bool ChangedDirection ()
		{
			return ((m_previousVelocity.x == 0 && m_velocity.x != 0) ||
			(m_previousVelocity.y == 0 && m_velocity.y != 0));
		}

		private float GetRaycastRangeBasedOnMovementDirection ()
		{
			return m_velocity.y < 0f ? OBSTACLE_CHECK_RANGE_DOWN : OBSTACLE_CHECK_RANGE;
		}

		private bool ObstacleInPath (float range)
		{
			return Physics2D.Raycast (transform.position, m_velocity, range, obstacleMask).collider != null;
		}

		private void HandleProjectiles ()
		{
			if (m_controller.ShouldReleaseProjectile () && OnReleaseProjectileRequested != null) {
				OnReleaseProjectileRequested ();
			} else if (OnRaiseProjectileRequested != null) {
				OnRaiseProjectileRequested (m_controller.ShouldRaiseProjectile ());
			} 
		}

		private void HandleMelee ()
		{
			if (m_controller.ShouldMelee () && OnMeleeRequested != null) {
				OnMeleeRequested ();
			}
		}

        void FixedUpdate()
        {
                if (OnVelocityUpdated != null)
                {
                    OnVelocityUpdated(m_velocity);
                }

                m_Movement.SetVelocity(m_velocity * movementSpeedMultiplier * Time.deltaTime);
            
        }
	}
}