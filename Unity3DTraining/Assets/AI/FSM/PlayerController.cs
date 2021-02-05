using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace AI.FSM
{
    public class PlayerController : MonoBehaviour
    {
        FSM<PlayerController> m_FSM;

		//动画状态需要的属性
		public PlayableGraph AnimGraph;
		public AnimationMixerPlayable AnimMixer;
        public AnimationPlayableOutput AnimOutput;
		//[SerializeField]
		public List<AnimationClip> AnimClips;
        private Animator Animator;

		// Start is called before the first frame update
		void Start()
        {
            m_FSM = FSM<PlayerController>.CreateFSM(this, new FSMIdle(), new FSMFight(), new FSMMove());
            m_FSM.StartState<FSMIdle>();

            Animator = GetComponent<Animator>();
            AnimGraph = PlayableGraph.Create("Man_Graph");
            AnimOutput = AnimationPlayableOutput.Create(AnimGraph, "Man_Output", Animator);
            AnimMixer = AnimationMixerPlayable.Create(AnimGraph, AnimClips.Count, true);
            AnimOutput.SetSourcePlayable(AnimMixer);
            for (int i = 0; i < AnimClips.Count; i++)
			{
                AnimationClipPlayable playable = AnimationClipPlayable.Create(AnimGraph, AnimClips[i]);
                AnimGraph.Connect(playable, 0, AnimMixer, i);
			}

            AnimGraph.Play();
        }

        // Update is called once per frame
        void Update()
        {
            m_FSM.OnUpdate();
        }
    }
}
