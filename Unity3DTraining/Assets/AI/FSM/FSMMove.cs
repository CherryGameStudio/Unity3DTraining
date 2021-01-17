using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
	public class FSMMove : FSMState<PlayerController>
	{
		private enum MoveState
		{
			Up,
			Down,
			Left,
			Right,
		}

		public override void OnInit(FSM<PlayerController> fsm)
		{
			base.OnInit(fsm);
		}

		public override void OnEnter(FSM<PlayerController> fsm)
		{
			base.OnEnter(fsm);
			Debug.Log("进入移动状态");
		}

		public override void OnExit(FSM<PlayerController> fsm)
		{
			base.OnExit(fsm);
			Debug.Log("离开移动状态");
		}

		public override void OnUpdate(FSM<PlayerController> fsm)
		{
			base.OnUpdate(fsm);

			if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.K))
			{
				fsm.ChangeState<FSMFight>();
				return;
			}

			if (Input.GetKey(KeyCode.W))
			{
				Debug.Log("上移");
				return;
			}

			if (Input.GetKey(KeyCode.S))
			{
				Debug.Log("下移");
				return;
			}

			if (Input.GetKey(KeyCode.A))
			{
				Debug.Log("左移");
				return;
			}

			if (Input.GetKey(KeyCode.D))
			{
				Debug.Log("右移");
				return;
			}

			fsm.ChangeState<FSMIdle>();
		}
	}
}
