using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace EditorExtension
{
	public class EditorStylesWindow : EditorWindow
	{
		private static List<GUIStyle> m_GUIStyles = null;

		[MenuItem("EditorExtension/EditorStyles")]
		public static void ShowEditorStyles()
		{
			EditorWindow.GetWindow<EditorStylesWindow>("EditorStyles").Show();
			GetEditorStyles();
		}

		private static void GetEditorStyles()
		{
			m_GUIStyles = new List<GUIStyle>();
			foreach (PropertyInfo info in typeof(EditorStyles).GetProperties())
			{
				object style = info.GetValue(null, null) as GUIStyle;
				if (style.GetType() == typeof(GUIStyle))
				{
					m_GUIStyles.Add(style as GUIStyle);
				}
			}
		}

		private Vector2 scrollViewPosition = Vector2.zero;
		private void OnGUI()
		{
			scrollViewPosition = EditorGUILayout.BeginScrollView(scrollViewPosition);
			foreach (GUIStyle item in m_GUIStyles)
			{
				GUILayout.Label("EditorStyles:" + item.name, item);
			}
			EditorGUILayout.EndScrollView();
		}
	}
}
