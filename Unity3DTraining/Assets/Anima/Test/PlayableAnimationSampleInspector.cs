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
		public AnimationClip[] m_LoopClips;

		

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
			m_Clip = EditorGUILayout.ObjectField(m_Clip, typeof(AnimationClip), false) as	AnimationClip;
			if (GUILayout.Button("Play choosed clip."))
			{
				PlayableGraph graph = PlayableGraph.Create("PlayableSample");
				AnimationPlayableOutput output = AnimationPlayableOutput.Create(graph, "OutputSample", animator);
				AnimationClipPlayable playable = AnimationClipPlayable.Create(graph, m_Clip);
				output.SetSourcePlayable(playable);
				graph.Play();
			}

			GUILayout.EndVertical();
		}

		private void OnEnable()
		{
			serializedObject.FindProperty("m_LoopClips");
		}
	}
}
