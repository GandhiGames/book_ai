using UnityEngine;
using System;
using System.Collections;

namespace AdventureGame
{
	public class Projectile : MonoBehaviour
	{
		public SpriteRenderer frontArrow;
		public SpriteRenderer backArrow;
	
		public float lookAheadRange = 0.2f;
	
		private static readonly string DEFAULT_DRAW_LAYER = "Characters";

		private Vector2 m_Direction;
		private float m_MaxRange;
		private Vector2 m_InitialPosition;
		private float m_KnockbackForce;
		private LayerMask m_HitMask;
		private LayerMask m_ObstacleMask;
		private int m_DamageAmount;

		private MovementHandler m_Movement;

		private bool m_ShouldUpdate = false;

		void Awake ()
		{
			m_Movement = GetComponent<MovementHandler> ();
		}

		void OnEnable ()
		{
			frontArrow.sortingLayerName = DEFAULT_DRAW_LAYER;
			backArrow.sortingLayerName = DEFAULT_DRAW_LAYER;
			DrawAboveCharacters ();
		}

		void OnDisable ()
		{
			StopHitChecks ();
		}

		public void Initialise (Vector2 direction, float movementForce, float range, 
			float knockbackForce, LayerMask hitMask, LayerMask obstacleMask, int damageAmount)
		{
			if (direction == Utilities.instance.GetDirectionVector (Direction.Up)) {
				DrawBelowCharacters ();
			}

			m_Direction = direction;
			m_MaxRange = range;
			m_InitialPosition = transform.position;
			m_KnockbackForce = knockbackForce;
			m_HitMask = hitMask;
			m_ObstacleMask = obstacleMask;
			m_DamageAmount = damageAmount;

			BeginHitChecks ();

			m_Movement.EnableMovement ();


			m_Movement.AddForce (m_Direction * movementForce);
		}

	
		void Update ()
		{
			if (!m_ShouldUpdate) {
				return;
			}
						
			CheckForHit ();

			if (Vector2.Distance (m_InitialPosition, transform.position) > m_MaxRange) {
				ObjectManager.instance.RemoveObject (gameObject);
			}
		}

		private void CheckForHit ()
		{
			// Enemy hit.
			var enemyHit = Physics2D.Raycast (transform.position, m_Direction, lookAheadRange, m_HitMask);

			if (enemyHit.collider != null) {
				OnEnemyHit (enemyHit.collider, enemyHit.point);
			}

			// Obstacle hit.
			var obstacleHit = Physics2D.Raycast (transform.position, m_Direction, lookAheadRange, m_ObstacleMask);

			if (obstacleHit.collider != null) {
				OnObstacleHit (obstacleHit.collider);
			}
		}

		private void OnEnemyHit(Collider2D other, Vector2 hitPos)
		{
			AttachToCollider (other);
			SetDrawOrder ();

			ApplyDamage (other, hitPos);

			m_Movement.DisableMovement ();

			StopHitChecks ();
		}

		private void OnObstacleHit(Collider2D other)
		{
			m_Movement.DisableMovement ();
			StopHitChecks ();

			var renderer = other.gameObject.GetComponent<SpriteRenderer> ();

			if (renderer) {
				SetDrawLayerToTargets (renderer);
				DrawBehindTarget (renderer);
			}
		}

		private void AttachToCollider (Collider2D other)
		{
			transform.SetParent (other.transform, true);

			m_Movement.SetVelocity (Vector2.zero);
		}

		private void SetDrawOrder ()
		{
			if (m_Direction == Utilities.instance.GetDirectionVector (Direction.Down)) {
				frontArrow.sortingOrder = Utilities.instance.sortingOrderAboveCharacter;
				backArrow.sortingOrder = Utilities.instance.sortingOrderBelowCharacter;
			} else {
				frontArrow.sortingOrder = Utilities.instance.sortingOrderBelowCharacter;
				backArrow.sortingOrder = Utilities.instance.sortingOrderAboveCharacter;
			}
		}

		private void SetDrawLayerToTargets (SpriteRenderer other)
		{
			frontArrow.sortingLayerID = other.sortingLayerID;
			backArrow.sortingLayerID = other.sortingLayerID;
		}

		private void DrawBehindTarget(SpriteRenderer other)
		{
			if (m_Direction == Utilities.instance.GetDirectionVector (Direction.Up)) {
				frontArrow.sortingOrder =  other.sortingOrder - 1;
				backArrow.sortingOrder =  other.sortingOrder + 1;
			} else {
				frontArrow.sortingOrder =  other.sortingOrder - 1;
				backArrow.sortingOrder = other.sortingOrder - 1;
			}
		}

		private void ApplyKnockbackForce (Collider2D other)
		{
			var rigidbody = other.gameObject.GetComponent<Rigidbody2D> ();

			if (rigidbody) {
				rigidbody.AddForce (m_Direction * m_KnockbackForce);
			}
		}

		private void ApplyDamage (Collider2D other, Vector2 hitPos)
		{
			var damageListeners = other.gameObject.GetComponents<DamageListener> ();

			foreach (var damage in damageListeners) {
				damage.ApplyDamage (m_DamageAmount, m_Direction * m_KnockbackForce, hitPos);
			}
		}

		private void DrawBelowCharacters ()
		{
			frontArrow.sortingOrder = Utilities.instance.sortingOrderBelowCharacter;
			backArrow.sortingOrder = Utilities.instance.sortingOrderBelowCharacter;
		}

		private void DrawAboveCharacters ()
		{
			frontArrow.sortingOrder = Utilities.instance.sortingOrderAboveCharacter;
			backArrow.sortingOrder = Utilities.instance.sortingOrderAboveCharacter;
		}

		private void BeginHitChecks ()
		{
			m_ShouldUpdate = true;
		}

		private void StopHitChecks ()
		{
			m_ShouldUpdate = false;
		}
			
	}
}
