using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
	public class FSMIdle : FSMState<PlayerController>
	{
		public override void OnInit(FSM<PlayerController> fsm)
		{
			base.OnInit(fsm);
		}

		public override void OnEnter(FSM<PlayerController> fsm)
		{
			base.OnEnter(fsm);
			Debug.Log("进入待机状态");
		}

		public override void OnExit(FSM<PlayerController> fsm)
		{
			base.OnExit(fsm);
			Debug.Log("离开待机状态");
		}

		public override void OnUpdate(FSM<PlayerController> fsm)
		{
			base.OnUpdate(fsm);

			if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
			{
				fsm.ChangeState<FSMMove>();
			}

			if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.K))
			{
				fsm.ChangeState<FSMFight>();
			}
		}
	}
}
