﻿using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class CharacterMovementHandler : MonoBehaviour, MovementHandler
	{
		public Vector2 heading { get; private set; }
		public Vector2 velocity {get {return m_Rigidbody2D.velocity;}}
		public bool movementEnabled { get { return m_CanMove; } }

		private Rigidbody2D m_Rigidbody2D;
        private bool m_CanMove = true;

		void Awake()
		{
			m_Rigidbody2D = GetComponent<Rigidbody2D> ();
		}

        public void SetVelocity(Vector2 velocity)
        {
            if(!m_CanMove)
            {
                return;
            }

            // Should be performed in FixedUpdate.
            m_Rigidbody2D.velocity = velocity;

            if (velocity != Vector2.zero)
            {
                heading = velocity;
            }
        }

		public void AddForce (Vector2 force)
		{
            if (!m_CanMove)
            {
                return;
            }

            m_Rigidbody2D.AddForce (force);
			heading = m_Rigidbody2D.velocity;
		}

		public void DisableMovement()
		{
            m_CanMove = false;
		}

		public void EnableMovement()
		{
            m_CanMove = true;
		}
	}
}