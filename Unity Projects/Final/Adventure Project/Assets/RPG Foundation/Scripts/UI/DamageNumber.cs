using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AdventureGame
{

    public class DamageNumber : MonoBehaviour
    {
        private Text m_Text;
        private Vector2 m_WorldPos;

        void Awake()
        {
            m_Text = GetComponentInChildren<Text>();
        }

        public void Initialise(Vector2 worldPos, string text)
        {
            transform.localScale = Vector3.one;
            m_WorldPos = worldPos;
            transform.position = m_WorldPos;
            m_Text.text = text;
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        void Update()
        {
            transform.position = m_WorldPos;
        }
    }
}