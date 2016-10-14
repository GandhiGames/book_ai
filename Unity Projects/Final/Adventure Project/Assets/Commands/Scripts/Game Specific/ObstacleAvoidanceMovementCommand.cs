using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventureGame.ActionManagement
{
	public class ObstacleAvoidanceMovementCommand : Command
	{
		private static readonly float RAY_LENGTH = 0.45f;
		private static readonly float RAY_ANGLE = 160;
		private static readonly int RAY_COUNT = 5;
		private static readonly float WEIGHT = 0.1f;

		private MovementHandler m_Movement;
		private float m_Speed;
		private LayerMask m_ObstacleMask;

		public ObstacleAvoidanceMovementCommand (Transform character, 
		                                         Vector2 dir, 
		                                         MovementHandler movement, 
		                                         float speed,
		                                         LayerMask obstacleMask,
		                                         Action actionOnExecute = null) : base (character, dir, actionOnExecute)
		{
			m_Movement = movement;
			m_Speed = speed;
			m_ObstacleMask = obstacleMask;
		}

		
		protected override void DoCommand ()
		{
			var force = m_Direction + (GetForce () * WEIGHT);	

			m_Movement.SetVelocity (force * m_Speed * Time.deltaTime);
		}


		private Vector2 GetForce ()
		{

			//Vector2 pos = new Vector2 (m_Character.position.x + (m_Direction.normalized.x * (m_ColliderSize.x * 0.501f)), 
			//	              m_Character.position.y + (m_Direction.normalized.y * (m_ColliderSize.y * 0.501f)));

			var rayList = Utilities.instance.CreateRays (m_Character.position, 
				              m_Direction, RAY_ANGLE, RAY_COUNT);
			
			foreach (Ray2D ray in rayList) {

				RaycastHit2D[] hits = Physics2D.RaycastAll (ray.origin, ray.direction, 
					                      RAY_LENGTH, m_ObstacleMask); //, 1 << NameManager.instance.ObstacleMask);

		
				if (Utilities.instance.IsDebug) {
					Debug.DrawLine (ray.origin, ray.origin + ray.direction * RAY_LENGTH, Color.red);
				}

				foreach (var hit in hits) {
				
					if (hit.transform.GetInstanceID () != m_Character.GetInstanceID ()) {						
						Vector2 overShoot = (ray.origin.normalized + (ray.direction.normalized * RAY_LENGTH)) - hit.point;
					
						//Vector2 reflect = (Vector2)Vector3.Reflect (ray.direction, hit.normal);

						if (Utilities.instance.IsDebug) {
							Debug.DrawLine (m_Character.position, hit.normal * overShoot.magnitude, Color.yellow);
						}

						return hit.normal * overShoot.magnitude;
					}
				}
				
			} 

			return Vector2.zero;
		}

		float FrontFacingRayLength = 0.25f;
		float LeftRightFacingRayLength = 0.15f;
		float ObstacleRayAngle = 45f;

		public Vector2 GetForce2 ()
		{
			
			var frontRay = CreateFrontFacingRay ();
			
			var frontHit = Physics2D.Raycast (frontRay.origin, frontRay.direction, 
				               FrontFacingRayLength, m_ObstacleMask);

			
			if (frontHit.collider != null && !frontHit.collider.OverlapPoint (frontRay.origin)) {
				Debug.Log ("Here front");
				var overShoot = (frontRay.origin + (frontRay.direction * FrontFacingRayLength)) - frontHit.point;
				
				//var reflect = (Vector2)Vector3.Reflect (frontRay.direction, frontHit.normal);
				
				if (Utilities.instance.IsDebug)
					Debug.DrawRay (m_Character.position, (Vector2)frontHit.normal, Color.yellow);

				Debug.LogError (overShoot.magnitude);
				
				return frontHit.normal * (overShoot.magnitude * 2f);
			}
			
			
			
			var rayList = CreateLeftRightRays ();
			
			foreach (Ray2D ray in rayList) {

				var hit = Physics2D.Raycast (ray.origin, ray.direction, 
					          LeftRightFacingRayLength, m_ObstacleMask);
				
				
				if (hit.collider != null && !hit.collider.OverlapPoint (ray.origin)) {

					Debug.Log ("Here side");

					var overShoot = (ray.origin + (ray.direction * LeftRightFacingRayLength)) - hit.point;
					
					//var reflect = (Vector2)Vector3.Reflect (ray.direction, hit.normal);


					if (Utilities.instance.IsDebug)
						Debug.DrawRay (m_Character.position, (Vector2)hit.normal, Color.yellow);

					return hit.normal * (overShoot.magnitude * 2f);
				}
				
			}
			
			return Vector2.zero;
		}

		private Ray2D CreateFrontFacingRay ()
		{

			//Vector2 pos = new Vector2 (m_Character.position.x + (m_Direction.normalized.x * (m_ColliderSize.x * 0.501f)), 
			//	              m_Character.position.y + (m_Direction.normalized.y * (m_ColliderSize.y * 0.501f)));


			var frontRay = new Ray2D (m_Character.position, m_Movement.heading.normalized);
			
			if (Utilities.instance.IsDebug)
				Debug.DrawRay (frontRay.origin, frontRay.direction, Color.blue);
			
			return frontRay;
		}

		private List<Ray2D> CreateLeftRightRays ()
		{
			var rayList = new List <Ray2D> ();

			//Vector2 pos = new Vector2 (m_Character.position.x + (m_Direction.normalized.x * (m_ColliderSize.x * 0.501f)), 
			//	              m_Character.position.y + (m_Direction.normalized.y * (m_ColliderSize.y * 0.501f)));


			//right
			Vector2 rightDir = Quaternion.AngleAxis (-ObstacleRayAngle, Vector3.forward) * m_Movement.heading.normalized; //  physics.Heading;
			Ray2D rightRay = new Ray2D (m_Character.position, rightDir);
			rayList.Add (rightRay);

			if (Utilities.instance.IsDebug)
				Debug.DrawRay (rightRay.origin, rightRay.direction, Color.blue);
			
		
			

			//left
			Vector2 leftDir = Quaternion.AngleAxis (ObstacleRayAngle, Vector3.forward) * m_Movement.heading.normalized; // physics.Heading;
			Ray2D leftRay = new Ray2D (m_Character.position, leftDir);
			rayList.Add (leftRay);
			
			if (Utilities.instance.IsDebug)
				Debug.DrawRay (leftRay.origin, leftRay.direction, Color.blue);
			;
			
			
			return rayList;
		}

		private Vector2 CollisionAvoidance ()
		{


			var MAX_SEE_AHEAD = 1f;

			if ((Vector2)m_Character.position == m_Movement.heading.normalized) {
				return Vector2.zero;
			}

			var ahead = (Vector2)m_Character.position + m_Movement.heading.normalized * FrontFacingRayLength;
			//var ahead2 = (Vector2)character.position + physics.Heading.normalized * MAX_SEE_AHEAD * 0.5f;
			
			var mostThreatening = FindMostThreateningObstacle (MAX_SEE_AHEAD);

			var avoidance = Vector2.zero;
			
			if (mostThreatening != null) {
				avoidance.x = ahead.x - mostThreatening.offset.x;
				avoidance.y = ahead.y - mostThreatening.offset.y;
				
				avoidance.Normalize ();
			} 

			if (avoidance != Vector2.zero) {
				Debug.DrawLine ((Vector2)m_Character.position, (Vector2)m_Character.position + avoidance, Color.yellow);
			}
			
			return avoidance;
		}

		private Collider2D FindMostThreateningObstacle (float lookAhead)
		{
			var frontRay = CreateFrontFacingRay ();

			Debug.DrawRay (frontRay.origin, frontRay.direction, Color.red);
						
			var hit = Physics2D.Raycast (frontRay.origin, frontRay.direction, lookAhead, m_ObstacleMask);

			if (hit.collider != null) {
							
				return hit.collider;
			}

			var rayList = CreateLeftRightRays ();

			foreach (var ray in rayList) {
				Debug.DrawRay (ray.origin, ray.direction, Color.red);
				hit = Physics2D.Raycast (ray.origin, ray.direction, lookAhead, m_ObstacleMask);
				
				if (hit.collider != null) {
					
					return (BoxCollider2D)hit.collider;
				}
			}
			
			return null;
		}

	}
}

