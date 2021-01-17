#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[CustomEditor(typeof(TimelineControl))] 
public class TimelineControlInspector : Editor
{
	public PlayableAsset PlayableAsset;
	public DirectorWrapMode WrapMode;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		TimelineControl script = target as TimelineControl;
		if (script.m_Director == null)
		{
			return;
		}

		GUILayout.BeginVertical();

		//Timeline的播放，停止与暂停
		GUILayout.Label("The Control of timeline play,stop or pause.");
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("play"))
		{
			script.m_Director.Play();
		}
		if (GUILayout.Button("stop"))
		{
			script.m_Director.Stop();
		}
		if (GUILayout.Button("resume"))
		{
			script.m_Director.Resume();
		}
		if (GUILayout.Button("pause"))
		{
			script.m_Director.Pause();
		}
		GUILayout.EndHorizontal();

		//Timeline中的属性
		GUILayout.Label("The properties of timeline.");
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Get duration(double)"))
		{
			Debug.Log("duration is : " + script.m_Director.duration);
		}
		if (GUILayout.Button("Get initialTime(double)"))
		{
			Debug.Log("initialTime is : " + script.m_Director.initialTime);
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Get time(float)"))
		{
			Debug.Log("time is : " + script.m_Director.time);
		}
		if (GUILayout.Button("Get playOnAwake(bool)"))
		{
			Debug.Log("playOnAwake is : " + script.m_Director.playOnAwake);
		}
		GUILayout.EndHorizontal();
		if (GUILayout.Button("Get timeUpdateMode(DirectorUpdateMode)"))
		{
			Debug.Log("timeUpdateMode is : " + script.m_Director.timeUpdateMode);
		}
		if (GUILayout.Button("Get playableGraph(PlayableGraph)"))
		{
			Debug.Log("playableGraph is : " + script.m_Director.playableGraph.GetEditorName());
		}
		if (GUILayout.Button("Get playableAsset(PlayableAsset)"))
		{
			Debug.Log("playableAsset is : " + script.m_Director.playableAsset);
		}
		if (GUILayout.Button("Get extrapolationMode(DirectorWrapMode)"))
		{
			Debug.Log("extrapolationMode is : " + script.m_Director.extrapolationMode);
		}
		if (GUILayout.Button("Get state(PlayState)"))
		{
			Debug.Log("state is : " + script.m_Director.state);
		}

		//选择播放某个Timeline
		GUILayout.Label("Please choose your timeline asset and wrap mode.");
		PlayableAsset = EditorGUILayout.ObjectField("Timeline asset:",PlayableAsset, typeof(PlayableAsset), true) as PlayableAsset;
		WrapMode = (DirectorWrapMode)EditorGUILayout.EnumPopup("DirectoryWrapMode:",WrapMode);
		if (GUILayout.Button("Play current Timeline at the current mode."))
		{
			script.m_Director.Play(PlayableAsset, WrapMode);
		}

		GUILayout.EndVertical();
	}
}
#endif