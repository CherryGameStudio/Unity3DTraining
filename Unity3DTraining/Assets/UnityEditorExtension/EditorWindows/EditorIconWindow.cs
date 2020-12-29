using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorExtension
{
	public class EditorIconWindow : EditorWindow 
	{
		[MenuItem("EditorExtension/编辑器辅助开发工具/ShowEditorIcons")]
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
				Debug.unityLogger.logEnabled = false;
				GUIContent content = EditorGUIUtility.IconContent(item.name);
				if (content != null && content.image != null) 
				{
					m_Icons.Add(item.name);
				}
				Debug.unityLogger.logEnabled = true;
			}
		}

		private void OnGUI()
		{
			GUILayout.Label("点击按钮获取当前的GUIContent到剪切板");
			m_ScrollView = GUILayout.BeginScrollView(m_ScrollView);
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
							GUIUtility.systemCopyBuffer = string.Format("EditorGUIUtility.IconContent(\"{0}\")", m_Icons[index]);
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
