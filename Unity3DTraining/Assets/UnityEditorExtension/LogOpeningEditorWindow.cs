using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorExtension
{
	public class LogOpeningEditorWindow
	{
		[MenuItem("EditorExtension/LogOpeningEditorWindows")]
		public static void LogOpeningEditorWindows()
		{
			foreach (var item in Resources.FindObjectsOfTypeAll<EditorWindow>())
			{
				Debug.Log(item.GetType().ToString	());
			}
		}
	}
}
