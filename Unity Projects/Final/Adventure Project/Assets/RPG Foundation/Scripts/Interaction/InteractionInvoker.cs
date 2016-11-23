using UnityEngine;
using System;
using System.Collections.Generic;

namespace AdventureGame
{
    public class InteractionInvoker : MonoBehaviour
    {
        public float interactionDistance;
        private MovementHandler m_Movement;

        void Awake()
        {
            m_Movement = GetComponent<MovementHandler>();
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                var heading = m_Movement.heading;

                var ray = new Ray2D(transform.position, heading);

                var hits = Physics2D.RaycastAll(ray.origin, ray.direction,
                     interactionDistance);

                if (hits.Length > 0)
                {
                    var hitResults = new List<InteractableRaycastResult>();

                    foreach (var hit in hits)
                    {
                        var interactable = hit.collider.gameObject.GetComponent<Interactable>();
                        if (interactable != null)
                        {
                            hitResults.Add(new InteractableRaycastResult(interactable, hit.distance));
                        }
                    }

                    if(hitResults.Count > 0)
                    {
                        hitResults[0].interactable.OnInteracted(m_Movement);
                    }
                }

            }
        }
    }

    public class InteractableRaycastResult : IComparable<InteractableRaycastResult>
    {
        public Interactable interactable { get; private set; }
        private float m_Distance;

        public InteractableRaycastResult(Interactable interactable, float dist)
        {
            this.interactable = interactable;
            m_Distance = dist;
        }

        public int CompareTo(InteractableRaycastResult other)
        {
            return m_Distance.CompareTo(other.m_Distance);
        }
    }
}