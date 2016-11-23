using UnityEngine;
using System.Collections;

namespace AdventureGame
{
    public interface PlayerHealthIncrement
    {
        void DoIncrement();
    }

    [RequireComponent(typeof(Health))]
    public class PlayerHealthIncrementImpl : MonoBehaviour, PlayerHealthIncrement
    {
        public int incrementAmount = 2;

        private Health m_Health;

        void Awake()
        {
            m_Health = GetComponent<Health>();
        }

        public void DoIncrement()
        {
            m_Health.Increment(incrementAmount);
        }

   
    }
}