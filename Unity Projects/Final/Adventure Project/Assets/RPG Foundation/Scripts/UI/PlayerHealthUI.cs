using UnityEngine;
using System.Collections.Generic;

namespace AdventureGame
{
    public class PlayerHealthUI : MonoBehaviour
    {
        public GameObject heartUIPrefab;
        public float initialX = 30f;
        public float xOffset = 45f;

        private Health m_PlayerHealth;

        private List<HealthHeart> m_Hearts;
        private int m_CurrentHeart;

        void Awake()
        {
            m_PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        void Start()
        {
            int currentHealth = m_PlayerHealth.currentHitPoints;

            bool evenHitpoints = currentHealth % 2 == 0;

            if(!evenHitpoints)
            {
                Debug.Log("Player should have even number of hit points");
            }

            m_Hearts = new List<HealthHeart>();

            int numToSpawn = currentHealth / 2;

            for(int i= 0; i < numToSpawn; i++)
            {
                float x = initialX + xOffset * i;
                SpawnHeart(x);
            }

            m_CurrentHeart = numToSpawn - 1;
        }

        void OnEnable()
        {
            m_PlayerHealth.onHealthIncrement += IncrementHearts;
            m_PlayerHealth.onHit += OnHit;
        }

        void OnDisable()
        {
            m_PlayerHealth.onHealthIncrement -= IncrementHearts;
            m_PlayerHealth.onHit -= OnHit;
        }

        private void IncrementHearts()
        {
            int numToSpawn = m_PlayerHealth.currentHitPoints / 2;

            numToSpawn -= m_Hearts.Count;

            int offsetMulti = m_Hearts.Count;

            for (int i = 0; i < numToSpawn; i++)
            {
                float x = initialX + xOffset * (i + offsetMulti);

                SpawnHeart(x);
            }
        }

        private void OnHit()
        {
            if (m_CurrentHeart >= 0)
            {
                var curHeart = m_Hearts[m_CurrentHeart];

                curHeart.OnPlayerHit();

                if (curHeart.IsEmpty())
                {
                    m_CurrentHeart--;
                }
            }
        }

        private void SpawnHeart(float xPos)
        {
            Vector2 spawnPos = new Vector2(xPos, 0f);
            var heart = (GameObject)Instantiate(heartUIPrefab, spawnPos, Quaternion.identity);
            heart.transform.SetParent(transform, false);

            m_Hearts.Add(heart.GetComponent<HealthHeart>());
        }
    }
}