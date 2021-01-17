using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TempCheckReference
{

	[MenuItem("Temp/检查项目中丢失引用的预制体")]
	public static void CheckNullReferenceFromAssets()
	{
		string[] resPaths = AssetDatabase.FindAssets("t: Prefab", new string[] { "Assets" });

		for (int i = 0; i < resPaths.Length; i++)  
		{

			string path = AssetDatabase.GUIDToAssetPath(resPaths[i]);
			GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);  
			EditorUtility.DisplayProgressBar("检查项目中丢失引用的预制体", gameObject.name, (float)i / resPaths.Length);

			MonoBehaviour[] scripts = gameObject.GetComponentsInChildren<MonoBehaviour>(true);
			if (null != scripts)
			{
				string tips = string.Empty;
				for (int j = 0; j < scripts.Length; j++)
				{
					MonoBehaviour mono = scripts[j];
					if (null == mono) continue;
					SerializedObject tempObject = new SerializedObject(mono);
					SerializedProperty temProperty = tempObject.GetIterator();
					while (temProperty.NextVisible(true))
					{
						if (temProperty.propertyType == SerializedPropertyType.ObjectReference
							&& temProperty.objectReferenceValue == null
							&& temProperty.objectReferenceInstanceIDValue != 0)
						{
							tips += mono.GetType().ToString() + "| |" + temProperty.propertyPath + "引用丢失\t\n";
						}
					}
				}
				if (!string.IsNullOrEmpty(tips))
				{
					Transform tempTrans = gameObject.transform;
					string name = tempTrans.name;
					//EditorUtility.DisplayDialog(item.name + "丢失引用！", tips, "确定");
					while (tempTrans.parent != null)
					{
						name = Path.Combine(tempTrans.parent.name, name);
						tempTrans = tempTrans.parent;
					}
					Debug.LogError(name + "丢失引用！" + "引用目录为" + tips);
				}
			}
		}
		EditorUtility.ClearProgressBar();
	}

	[MenuItem("Temp/检查场景中丢失引用的预制体")]
	public static void CheckNullReferenceFromScenes()
	{
		foreach (var item in GameObject.FindObjectsOfType<GameObject>())
		{
			if (null == item) return;
			MonoBehaviour[] scripts = item.GetComponents<MonoBehaviour>();
			if (null != scripts)
			{
				string tips = string.Empty;
				for (int i = 0; i < scripts.Length; i++)
				{
					MonoBehaviour mono = scripts[i];
					if (null == mono) continue;
					SerializedObject tempObject = new SerializedObject(mono);
					SerializedProperty temProperty = tempObject.GetIterator();
					while (temProperty.NextVisible(true))
					{
						if (temProperty.propertyType == SerializedPropertyType.ObjectReference
							&& temProperty.objectReferenceValue == null
							&& temProperty.objectReferenceInstanceIDValue != 0)
						{
							tips += mono.GetType().ToString() + "| |" + temProperty.propertyPath + "引用丢失\t\n";
						}
					}
				}
				if (!string.IsNullOrEmpty(tips))
				{
					Transform tempTrans = item.transform;
					string name = tempTrans.name;
					//EditorUtility.DisplayDialog(item.name + "丢失引用！", tips, "确定");
					while (tempTrans.parent != null)
					{
						name = Path.Combine(tempTrans.parent.name, name);
						tempTrans = tempTrans.parent;
					}
					Debug.LogError(name + "丢失引用！" + "引用目录为" + tips);
				}
			}
		}
	}
}
