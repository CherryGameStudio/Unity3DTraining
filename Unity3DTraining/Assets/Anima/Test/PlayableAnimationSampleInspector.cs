using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Animation.Playables
{
	[CustomEditor(typeof(PlayableAnimationSample))]
	public class PlayableAnimationSampleInspector : Editor
	{
		private AnimationClip m_Clip;


		PlayableGraph loopGraph;
		AnimationMixerPlayable mixer;
		int current = 0;

		PlayableGraph graph;

		long time = 10000000;
		long waitTime;
		//public AnimationClip[] m_LoopClips;

		SerializedProperty m_LoopClips;


		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			PlayableAnimationSample script = target as PlayableAnimationSample;
			Animator animator = script.GetComponent<Animator>();
			if (animator == null)
			{
				return;
			}

			GUILayout.BeginVertical();
			if (GUILayout.Button("Play loop clips"))
			{
				loopGraph = PlayableGraph.Create("PlayableSample");
				AnimationPlayableOutput output = AnimationPlayableOutput.Create(loopGraph, "OutputSample", animator);
				mixer = AnimationMixerPlayable.Create(loopGraph,5);
				output.SetSourcePlayable(mixer);
				for (int i = 0; i < script.m_LoopClips.Length; i++)
				{
					AnimationClipPlayable playable = AnimationClipPlayable.Create(loopGraph, script.m_LoopClips[i]);
					loopGraph.Connect(playable, 0, mixer, i);
					mixer.SetInputWeight(i, 0f);
				}
				mixer.SetInputWeight(0, 1f);
				current = 0;
				loopGraph.Play();

				waitTime = DateTime.Now.Ticks;
			}

			long curTime = DateTime.Now.Ticks;
			long detalTime = curTime - waitTime;
			if (loopGraph.IsValid() && detalTime > time)
			{
				mixer.SetInputWeight(current, 0f);
				current = (current + 1) % 4;
				mixer.SetInputWeight(current, 1f);

				waitTime = DateTime.Now.Ticks;
			}

			m_Clip = EditorGUILayout.ObjectField(m_Clip, typeof(AnimationClip), false) as	AnimationClip;
			if (GUILayout.Button("Play choosed clip."))
			{
				graph = PlayableGraph.Create("PlayableSample");
				AnimationPlayableOutput output = AnimationPlayableOutput.Create(graph, "OutputSample", animator);
				AnimationClipPlayable playable = AnimationClipPlayable.Create(graph, m_Clip);
				output.SetSourcePlayable(playable);
				graph.Play();
			}
			//EditorGUILayout.PropertyField(m_LoopClips, true);

			GUILayout.EndVertical();
		}

		private void OnEnable()
		{
			m_LoopClips = serializedObject.FindProperty("m_LoopClips");
		}

		
	}
}
