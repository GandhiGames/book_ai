using UnityEngine;
using System.Collections;

namespace AdventureGame
{
    public class InteractableSign : MonoBehaviour, Interactable
    {
        public string signText;

        private static DialogBox DIALOG_BOX;

        void Awake()
        {
            if(DIALOG_BOX == null)
            {
                DIALOG_BOX = GameObject.FindObjectOfType<DialogBox>();
            }
        }

        public void OnInteracted(MovementHandler moveHandler)
        {
            if (DIALOG_BOX.IsVisible())
            {
                moveHandler.EnableMovement();
                DIALOG_BOX.Hide();
            }
            else
            {
                moveHandler.DisableMovement();
                DIALOG_BOX.Show(signText);
            }
        }

    }
}
