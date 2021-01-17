using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineControl : MonoBehaviour
{
    [HideInInspector]
    public PlayableDirector m_Director;
	private void Awake()
	{
		m_Director = GetComponent<PlayableDirector>();
		//m_Director.stopped += (PlayableDirector directory) => Debug.Log("This is a delegate that registers the event at the stop moment");
		//m_Director.played += (PlayableDirector directory) => Debug.Log("This is a delegate that registers the event at the play moment");
		//m_Director.paused += (PlayableDirector directory) => Debug.Log("This is a delegate that registers the event at the pause moment");
	}

	void Start()
    {
        m_Director.GetReferenceValue(new PropertyName("Man_MoveDown"), out bool isvaild);
        //Debug.LogError(isvaild);
    }

    void Update()
    {
        
    }

    public void DirectorTest()
	{
        //m_Director.
    }
}
