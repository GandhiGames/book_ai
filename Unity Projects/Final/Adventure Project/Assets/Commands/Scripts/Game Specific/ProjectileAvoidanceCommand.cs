using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventureGame.ActionManagement
{
	public class ProjectileAvoidanceCommand : Command
	{
		//Increases the strength of the force - lower numbers equals a stronger force
		private static readonly float MAG_OFFSET = 0.1f;
		private static readonly float WEIGHT = 1f;
		private static readonly float BUFFER = 0.25f;

		private MovementHandler m_Movement;
		private float m_Speed;
		private string m_ProjectileTag;
		private float m_SightRange;

		public ProjectileAvoidanceCommand (Transform character, float sightRange, 
			MovementHandler movement, float force, string projectileTag, Action actionOnExecute = null) : base (character, 
				default(Vector2), actionOnExecute)
		{
			m_Movement = movement;
			m_SightRange = sightRange;
			m_Speed = force;
			m_ProjectileTag = projectileTag;

			if (m_SightRange <= BUFFER) {
				m_SightRange = BUFFER + 0.5f;
			}
		}

		protected override void DoCommand ()
		{
			var moveForce = GetForce () * WEIGHT;

			m_Movement.SetVelocity (moveForce * m_Speed * Time.deltaTime);
		}



		private Vector2 GetForce ()
		{			
			var entities = GetEntitiesInSight (m_SightRange);
			var steeringForce = Vector2.zero;
			foreach (var obj in entities) {
				Vector2 toAgent = m_Character.transform.position - obj.transform.position;
				steeringForce += toAgent.normalized / (toAgent.magnitude * MAG_OFFSET);
			}
						
			
			return steeringForce;
		}

		private List<GameObject> GetEntitiesInSight (float sightRadius)
		{

			var retVals = new List<GameObject> ();

			var entities = GameObject.FindGameObjectsWithTag (m_ProjectileTag);
			
			foreach (var obj in entities) {



				float to = (obj.transform.position - m_Character.transform.position).sqrMagnitude;
					
				if (to < (sightRadius * sightRadius) && to > (BUFFER * BUFFER)) {
					retVals.Add (obj);
				}


			}
			
			return retVals;
		}
		

	}
}
