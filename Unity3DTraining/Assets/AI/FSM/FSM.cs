using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace AI.FSM
{
    public class FSM<T> where T : class
    {
        public T Owner;
        public FSMState<T> CurrentState;

        private readonly Dictionary<Type, FSMState<T>> m_FSMStateDic;

        public FSM()
		{
            m_FSMStateDic = new Dictionary<Type, FSMState<T>>();
		}

        public static FSM<T> CreateFSM(T owner,params FSMState<T>[] states)
		{
            FSM<T> fsm = new FSM<T>();
            fsm.Owner = owner;
			foreach (var item in states)
			{
                item.OnInit(fsm);
                fsm.m_FSMStateDic.Add(item.GetType(), item);
			}

            return fsm;
		}

        public void StartState<TState>() where TState : FSMState<T>
		{
            TState state = GetState<TState>();
            state.OnEnter(this);
            CurrentState = state;
		}

        public void ChangeState<TState>() where TState : FSMState<T>
		{
            TState state = GetState<TState>();

            CurrentState.OnExit(this);
            CurrentState = state;
            CurrentState.OnEnter(this);
		}

        public TState GetState<TState>() where TState : FSMState<T>
		{
			if (m_FSMStateDic.TryGetValue(typeof(TState), out FSMState<T> state))
			{
                return state as TState;
			}

            return null;
		}

        public void OnUpdate()
		{
            CurrentState.OnUpdate(this);
		}
    }
}
