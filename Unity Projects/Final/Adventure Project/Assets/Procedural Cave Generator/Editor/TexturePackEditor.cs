using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	[CustomEditor (typeof(TexturePack))]
	public class TexturePackEditor : Editor
	{

		private TexturePack m_Target;

		void Awake ()
		{
			m_Target = (TexturePack)target;
		}

		public override void OnInspectorGUI ()
		{
			//DrawDefaultInspector ();

			EditorGUIUtility.fieldWidth = 0.1f;
			EditorGUIUtility.labelWidth = 0.1f;
			EditorGUIUtility.wideMode = false;
		
			if (m_Target.topRow.IsNullOrEmpty ()) {
				m_Target.topRow = new Sprite[3];
			}

			EditorGUILayout.BeginHorizontal ();
			m_Target.topRow [0] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.topRow [0], typeof(Sprite), true);
			m_Target.topRow [1] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.topRow [1], typeof(Sprite), true);
			m_Target.topRow [2] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.topRow [2], typeof(Sprite), true);
			EditorGUILayout.EndHorizontal ();

			if (m_Target.middleRow.IsNullOrEmpty ()) {
				m_Target.middleRow = new Sprite[3];
			}

			EditorGUILayout.BeginHorizontal ();
			m_Target.middleRow [0] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.middleRow [0], typeof(Sprite), true);
			m_Target.middleRow [1] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.middleRow [1], typeof(Sprite), true);
			m_Target.middleRow [2] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.middleRow [2], typeof(Sprite), true);
			EditorGUILayout.EndHorizontal ();

			if (m_Target.bottomRow.IsNullOrEmpty ()) {
				m_Target.bottomRow = new Sprite[3];
			}
			EditorGUILayout.BeginHorizontal ();
			m_Target.bottomRow [0] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.bottomRow [0], typeof(Sprite), true);
			m_Target.bottomRow [1] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.bottomRow [1], typeof(Sprite), true);
			m_Target.bottomRow [2] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.bottomRow [2], typeof(Sprite), true);
			EditorGUILayout.EndHorizontal ();

			if (m_Target.facade.IsNullOrEmpty ()) {
				m_Target.facade = new Sprite[3];
			}
			EditorGUILayout.BeginHorizontal ();
			m_Target.facade [0] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.facade [0], typeof(Sprite), true);
			m_Target.facade [1] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.facade [1], typeof(Sprite), true);
			m_Target.facade [2] = (Sprite)EditorGUILayout.ObjectField ("", m_Target.facade [2], typeof(Sprite), true);
			EditorGUILayout.EndHorizontal ();

			m_Target.floor = (Sprite)EditorGUILayout.ObjectField ("Background: ", m_Target.floor, typeof(Sprite), true);
		}
	}
}
