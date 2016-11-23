using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(Renderer))]
	public class VariableSortingOrder : MonoBehaviour
	{
		public string playerAboveLayerName = "Foreground";
		public string playerBelowLayerName = "Above Ground";

		private static Transform m_player;
		private Renderer m_renderer;

		void Awake ()
		{
			m_renderer = GetComponent<Renderer> ();
		}

		void Start ()
		{
			if (m_player == null) {
				var playerObj = GameObject.FindGameObjectWithTag ("Player");

				if (playerObj) {
					m_player = playerObj.transform;
				}
			}
		}

		void Update ()
		{
			if (PlayerAbove ()) {
				m_renderer.sortingLayerName = playerAboveLayerName;
			} else {
				m_renderer.sortingLayerName = playerBelowLayerName;
			}
		}

		private bool PlayerAbove ()
		{
			return m_player.transform.position.y > transform.position.y;
		}
	}
}
