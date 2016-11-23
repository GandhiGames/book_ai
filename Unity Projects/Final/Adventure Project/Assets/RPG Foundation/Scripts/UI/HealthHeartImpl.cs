using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AdventureGame
{

    public interface HealthHeart
    {
        void OnPlayerHit();
        bool IsEmpty();
    }

    [RequireComponent(typeof(Image))]
    public class HealthHeartImpl : MonoBehaviour, HealthHeart
    {
        public Sprite[] healthSprites;

        private static readonly int EMPTY = 0;

        private int m_CurrentSpriteIndex;
        private Image m_Renderer;

        void Awake()
        {
            m_Renderer = GetComponent<Image>();
        }

        void Start()
        {
            m_CurrentSpriteIndex = healthSprites.Length - 1;
            m_Renderer.sprite = healthSprites[m_CurrentSpriteIndex];
        }

        public void OnPlayerHit()
        {
            m_CurrentSpriteIndex -= 1;

            if(m_CurrentSpriteIndex >= EMPTY)
            {
                m_Renderer.sprite = healthSprites[m_CurrentSpriteIndex];
            }
        }

        public bool IsEmpty()
        {
            return m_CurrentSpriteIndex <= EMPTY;
        }

    }
}