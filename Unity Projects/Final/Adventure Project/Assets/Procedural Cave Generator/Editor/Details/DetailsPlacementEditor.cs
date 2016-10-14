using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	public class DetailsPlacementEditor<T> : Editor where T : DetailsPlacement
	{
		protected T m_Target;

		protected virtual void Awake ()
		{
			m_Target = (T)target;
		}

		protected void DisplayGenerateDeleteButtons ()
		{

			
			GUILayout.BeginHorizontal ();

			if (GUILayout.Button ("Generate")) {
				m_Target.EditorSetup ();

				if (m_Target.Generate ()) {
					EditorSceneManager.MarkSceneDirty (EditorSceneManager.GetActiveScene ());
				}

			}

			if (GUILayout.Button ("Delete")) {
				m_Target.EditorSetup ();

				if (m_Target.Remove ()) {
					EditorSceneManager.MarkSceneDirty (EditorSceneManager.GetActiveScene ());
				}
			}

			GUILayout.EndHorizontal ();
		}
			
		protected void ShowContainerName()
		{
			m_Target.containerName = EditorGUILayout.TextField ("Container Name:", m_Target.containerName);
		}
	}
}