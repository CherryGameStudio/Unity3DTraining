using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

namespace EditorExtension
{
	public class EditorSearchTextField : EditorWindow
	{
		[MenuItem("EditorExtension/LayoutExtension/Search")]
		public static void OpenSearchWindow()
		{
			GetWindow<EditorSearchTextField>("搜索栏面板").Show();
		}

		//测试List<string>
		private static List<string> m_StringList = new List<string> { "abc", "12", "de", "45" };
		private string searchStringStr;

		//测试List<int>
		private static List<int> m_IntList = new List<int> { 123, 234, 345, 456 };
		private string searchIntStr;

		//测试List<Vector2>
		private static List<Vector2> m_Vec2List = new List<Vector2> { new Vector2(1, 2), new Vector2(2, 3), new Vector2(5, 6) };
		private string searchVec2Str;


		private void OnGUI()
		{
			SearchTextField(ref searchStringStr, m_StringList);
			SearchTextField<int>(ref searchIntStr, m_IntList);
			SearchTextField<Vector2>(ref searchVec2Str, m_Vec2List);
		}

		//EditorSearch
		public static void SearchTextField(ref string searchText, List<string> filterList)
		{
			if (searchText == null)
			{
				searchText = "";
			}
			GUILayout.BeginHorizontal();
			GUILayout.Label("请输入搜索关键字:", GUILayout.Width(120));
			searchText = GUILayout.TextField(searchText, GUILayout.ExpandWidth(true));
			if (GUILayout.Button("清空输入", GUILayout.Width(80)))
			{
				searchText = "";
				GUI.changed = true;
				GUIUtility.keyboardControl = 0;
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginVertical();
			bool isGetValue = false;
			foreach (string item in filterList)
			{
				if (item.Contains(searchText))
				{
					GUILayout.Label(item);
					if (!isGetValue)
					{
						isGetValue = true;
					}
				}
			}
			if (!isGetValue)
			{
				GUILayout.Label("无匹配元素！");
			}
			GUILayout.EndVertical();
		}

		public static void SearchTextField<T>(ref string searchText, List<T> filterList)
		{
			if (typeof(T).GetMethod("ToString", Type.EmptyTypes) == null)
			{
				throw new Exception(string.Format("类型【{0}】不能用作搜索队列，没有ToString方法", typeof(T).FullName));
			}

			if (searchText == null)
			{
				searchText = "";
			}
			GUILayout.BeginHorizontal();
			GUILayout.Label("请输入搜索关键字:", GUILayout.Width(120));
			searchText = GUILayout.TextField(searchText, GUILayout.ExpandWidth(true));
			if (GUILayout.Button("清空输入", GUILayout.Width(80)))
			{
				searchText = "";
				GUI.changed = true;
				GUIUtility.keyboardControl = 0;
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginVertical();
			bool isGetValue = false;
			foreach (T item in filterList)
			{
				if (item.ToString().Contains(searchText))
				{
					GUILayout.Label(item.ToString());
					if (!isGetValue)
					{
						isGetValue = true;
					}
				}
			}
			if (!isGetValue)
			{
				GUILayout.Label("无匹配元素！");
			}
			GUILayout.EndVertical();
		}
	}
}
