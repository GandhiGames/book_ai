using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	[CustomEditor (typeof(TrackPlacement))]
	public class TrackPlacementEditor : DetailsPlacementEditor<TrackPlacement>
	{
		

		public override void OnInspectorGUI ()
		{
			ShowContainerName ();
			m_Target.pathManager = (PathManager)EditorGUILayout.ObjectField ("Pathmanager:", m_Target.pathManager, typeof(PathManager), true);
			m_Target.prefab = (GameObject)EditorGUILayout.ObjectField ("Prefab:", m_Target.prefab, typeof(GameObject), true);

			EditorGUIUtility.fieldWidth = 0.1f;
			EditorGUIUtility.labelWidth = 0.1f;
			EditorGUIUtility.wideMode = false;

			if (m_Target.horizontalTrack.IsNullOrEmpty ()) {
				m_Target.horizontalTrack = new Sprite[3];
			}

			EditorGUILayout.BeginHorizontal ();
			m_Target.horizontalTrack [0] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.horizontalTrack [0], typeof(Sprite), true);
			m_Target.horizontalTrack [1] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.horizontalTrack [1], typeof(Sprite), true);
			m_Target.horizontalTrack [2] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.horizontalTrack [2], typeof(Sprite), true);
			EditorGUILayout.EndHorizontal ();

			if (m_Target.verticalTrack.IsNullOrEmpty ()) {
				m_Target.verticalTrack = new Sprite[3];
			}

			EditorGUILayout.BeginHorizontal ();
			m_Target.verticalTrack [0] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.verticalTrack [0], typeof(Sprite), true);
			m_Target.verticalTrack [1] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.verticalTrack [1], typeof(Sprite), true);
			m_Target.verticalTrack [2] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.verticalTrack [2], typeof(Sprite), true);
			EditorGUILayout.EndHorizontal ();

			if (m_Target.trackCorners.IsNullOrEmpty ()) {
				m_Target.trackCorners = new Sprite[4];
			}
			EditorGUILayout.BeginHorizontal ();
			m_Target.trackCorners [0] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.trackCorners [0], typeof(Sprite), true);
			m_Target.trackCorners [1] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.trackCorners [1], typeof(Sprite), true);
			m_Target.trackCorners [2] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.trackCorners [2], typeof(Sprite), true);
			m_Target.trackCorners [3] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.trackCorners [3], typeof(Sprite), true);
			EditorGUILayout.EndHorizontal ();


			EditorGUILayout.HelpBox ("Make sure you generate the environment before adding track.", MessageType.Info);
			DisplayGenerateDeleteButtons ();
		}
	}
}
