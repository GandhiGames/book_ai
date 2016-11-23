using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AdventureGame
{
    public class DialogBox : MonoBehaviour
    {
        private Text m_Text;

        void Awake()
        {
            m_Text = GetComponentInChildren<Text>();
        }

        void Start()
        {
            Hide();
        }

        public void Show(string text)
        {
            m_Text.text = text;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public bool IsVisible()
        {
            return gameObject.activeSelf;
        }

    }
}