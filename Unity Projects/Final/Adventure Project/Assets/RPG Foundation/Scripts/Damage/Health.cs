using UnityEngine;
using System;
using System.Collections;

namespace AdventureGame
{
	public class Health : MonoBehaviour, DamageListener
	{
		public int hitPoints;
		public Action onDeath;
        public Action onHit;
        public Action onHealthIncrement;
        public int currentHitPoints { get { return m_CurrentHitPoints; } }

		private int m_CurrentHitPoints;

        private static DamageNumbers DAMAGE_NUMBERS;

        void Awake()
        {
            if(DAMAGE_NUMBERS == null)
            {
                DAMAGE_NUMBERS = GameObject.FindObjectOfType<DamageNumbers>();
            }
        }

		void OnEnable ()
		{
			m_CurrentHitPoints = hitPoints;
		}

		public void ApplyDamage (int damageAmount, Vector2 force, Vector2 worldPos)
		{
            DAMAGE_NUMBERS.ShowDamageNumber(damageAmount, transform.position);

			m_CurrentHitPoints -= damageAmount;

            if (onHit != null)
            {
                onHit();
            }

            if (m_CurrentHitPoints <= 0f) {
				if (onDeath != null) {
					onDeath ();
				} else {
					Debug.LogWarning ("No death method set for " + gameObject.name + ". Character will live forever!");
				}
			} 
		}

        public void Increment(int amount)
        {
            hitPoints += amount;
            m_CurrentHitPoints = hitPoints;

            if(onHealthIncrement != null)
            {
                onHealthIncrement();
            }
        }

    }
}
