using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayableBehaviourTest : PlayableBehaviour
{
	//the function is called when this clip is over, or timeline is paused on the clip, or timeline is stopped and PlayState is changed to Playing.
	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		base.OnBehaviourPause(playable, info);
		//Debug.Log("This is a callback function when the behaviour is paused.");
	}

	//the function is called when time moment enters this clip or timeline is played or resumed on this clip.
	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		base.OnBehaviourPlay(playable, info);
		//Debug.Log("This is a callback function when the behaviour is played.");
	}

	//the function is called when timeline PlayState is changed to Playing.
	public override void OnGraphStart(Playable playable)
	{
		base.OnGraphStart(playable);
		//Debug.Log("This is a callback function when the graph is started.");
	}

	//the function is call when timeline PlayState is changed to Paused.
	public override void OnGraphStop(Playable playable)
	{
		base.OnGraphStop(playable);
		//Debug.Log("This is a callback function when the graph is stopped.");
	}

	//when one clip on this track is created,other clips will be destroyed and created.
	public override void OnPlayableCreate(Playable playable)
	{
		base.OnPlayableCreate(playable);
		//Debug.Log("This is a callback function when the playable is created.");
	}

	//when one clip on this track is created,other clips will be destroyed and created.
	public override void OnPlayableDestroy(Playable playable)
	{
		base.OnPlayableDestroy(playable);
		//Debug.Log("This is a callback function when the playable is destroyed.");
	}

	public override void PrepareData(Playable playable, FrameData info)
	{
		base.PrepareData(playable, info);
		//Debug.Log("This is a function when the data is prepared.");
	}

	public override void PrepareFrame(Playable playable, FrameData info)
	{
		base.PrepareFrame(playable, info);
		//Debug.LogWarning("This is a function when the time is on the prepare frame.");
	}

	//the function is called after the prepare frame function is called.
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		base.ProcessFrame(playable, info, playerData);
		//Debug.Log("This is a function when the time is on the process frame.");
	}
}
