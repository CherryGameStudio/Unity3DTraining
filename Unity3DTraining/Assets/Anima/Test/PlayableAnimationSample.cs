using UnityEngine.Playables;
using UnityEngine.Animations;
using UnityEngine;

namespace Animation.Playables
{
    [RequireComponent(typeof(Animator))]
    public class PlayableAnimationSample : MonoBehaviour
    {
		private AnimationClip m_Clip;
        [SerializeField]
		public AnimationClip[] m_LoopClips = null;

		// Start is called before the first frame update
		void Start()
        {
            //playableGraph = PlayableGraph.Create("PlayAnimationSample");
            //Animator animator = GetComponent<Animator>();
            //AnimationPlayableOutput output = AnimationPlayableOutput.Create(playableGraph, "Animation_Cherry", animator);
            //AnimationClipPlayable animationClip = AnimationClipPlayable.Create(playableGraph, clip);
            //output.SetSourcePlayable(animationClip);
            //playableGraph.Play();
        }

        // Update is called once per frame
        void Update()
        {

        }

		private void OnDisable()
		{
           
		}
	}
}
