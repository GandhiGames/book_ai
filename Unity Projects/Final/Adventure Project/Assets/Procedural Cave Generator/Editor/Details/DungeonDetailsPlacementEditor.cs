using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	[CustomEditor (typeof(DungeonDetailsPlacement))]
	public class DungeonDetailsPlacementEditor : DetailsPlacementEditor<DungeonDetailsPlacement>
	{
		private SerializedObject m_Object;
		private SerializedProperty m_Property;

		void OnEnable()
		{
			m_Object = new SerializedObject(m_Target);
			m_Property =  m_Object.FindProperty("positionData");
		}
		
		public override void OnInspectorGUI ()
		{
			ShowContainerName ();
			EditorGUILayout.PropertyField(m_Property, new GUIContent("Cell Data"), true);
			EditorGUILayout.HelpBox ("It will attempt to place at least this many details, " +
				"however if no suitable locations found none will be placed", MessageType.Info);
			m_Target.minNumberToPlace = EditorGUILayout.IntField ("Min number to place:", m_Target.minNumberToPlace);
			m_Target.maxNumberToPlace = EditorGUILayout.IntField ("Max number to place:", m_Target.maxNumberToPlace);
			DisplayGenerateDeleteButtons ();

			m_Object.ApplyModifiedProperties();
		}
	}
}
