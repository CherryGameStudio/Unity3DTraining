using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorExtension
{
	public class EditorIconWindow : EditorWindow 
	{
		[MenuItem("EditorExtension/ShowEditorIcons")]
		public static void ShowEditorIcons()
		{
			GetWindow<EditorIconWindow>("ShowEditorIcons").Show();
		}

		private Vector2 m_ScrollView;
		private List<string> m_Icons = new List<string>();
		private void Awake()
		{
			Texture2D[] t = Resources.FindObjectsOfTypeAll<Texture2D>();
			foreach (var item in t)
			{
				GUIContent content = EditorGUIUtility.IconContent(item.name);
				if (content != null && content.image != null) 
				{
					m_Icons.Add(item.name);
				}
			}
		}

		private void OnGUI()
		{
			m_ScrollView = GUILayout.BeginScrollView(m_ScrollView);
			GUILayout.Button(EditorGUIUtility.IconContent("mini btn on focus@2x"));
			float width = 50f;
			int count = (int)(position.width / width);
			for (int i = 0; i < m_Icons.Count; i+=count)
			{
				GUILayout.BeginHorizontal();
				for (int j = 0; j < count; j++)
				{
					int index = i + j;
					if (index<m_Icons.Count)
					{
						if (GUILayout.Button(EditorGUIUtility.IconContent(m_Icons[index]), GUILayout.Width(width), GUILayout.Height(30)))
						{
							Debug.Log(string.Format("EditorGUIUtility.IconContent(\"{0}\")", m_Icons[index]));
							//TODO复制到粘贴板
						}
						GUILayout.Space(10);
					}
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndScrollView();
		}
	}
}
