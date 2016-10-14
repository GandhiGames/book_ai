using UnityEngine;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	public class DungeonLevelTransition : MonoBehaviour, DetailPlacementListener
	{
		private GridManager m_DungeonGenerator;
		private DetailsPlacement[] m_DetailsPlacement;

		private bool m_InitRequired = true;

		public void Notify()
		{
			Debug.Log ("Notify");
			Initialise ();
		}

		void OnDisable()
		{
			m_InitRequired = true;
		}

		private void Initialise()
		{
			m_DungeonGenerator = gameObject.GetComponentInParent<GridManager> ();

			m_DetailsPlacement = gameObject.GetComponentsInParent<DetailsPlacement>();

			m_InitRequired = false;
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if(other.CompareTag ("Player"))
			{
				if(m_InitRequired)
				{
					Debug.Log ("Lazy Initialised");
					Initialise ();
				}

				m_DungeonGenerator.Seed = Random.Range (0, 999999);

				m_DungeonGenerator.ReGenerate ();

				foreach(var detail in m_DetailsPlacement)
				{
					detail.Generate ();
				}

				//other.gameObject.transform.position = Utilities.instance.GetNodePosition (m_DungeonGenerator.startNode);
			}
		}
	}
}