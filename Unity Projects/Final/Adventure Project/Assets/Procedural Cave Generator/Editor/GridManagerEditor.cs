using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	[CustomEditor (typeof(GridManager))]
	public class GridManagerEditor : Editor
	{
		private GridManager m_Target;

		void Awake ()
		{
			m_Target = (GridManager)target;
		}

		public override void OnInspectorGUI ()
		{
			DrawDefaultInspector ();

			m_Target.SetupContainer ();

			EditorGUILayout.HelpBox ("Depending on the size of the grid, it may take a while to destroy/create. Please be patient!", MessageType.Info);

			GUILayout.BeginHorizontal ();
			
			if (GUILayout.Button ("Generate")) {
				m_Target.ReGenerate ();
				EditorSceneManager.MarkSceneDirty (EditorSceneManager.GetActiveScene());
			}

			if (GUILayout.Button ("Delete")) {
				if (m_Target.DestroyEnvironment ()) {
					EditorSceneManager.MarkSceneDirty (EditorSceneManager.GetActiveScene ());
				}
			}
			
			GUILayout.EndHorizontal ();
		}
	}
}
