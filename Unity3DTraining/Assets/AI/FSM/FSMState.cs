using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
	public abstract class FSMState<T> where T : class
	{
		public virtual void OnInit(FSM<T> fsm)
		{

		}

		public virtual void OnEnter(FSM<T> fsm)
		{

		}

		public virtual void OnExit(FSM<T> fsm)
		{

		}

		public virtual void OnUpdate(FSM<T> fsm)
		{

		}
	}
}
