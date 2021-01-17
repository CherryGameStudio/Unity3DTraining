using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
	public class FSMFight : FSMState<PlayerController>
	{
		public override void OnInit(FSM<PlayerController> fsm)
		{
			base.OnInit(fsm);
		}

		public override void OnEnter(FSM<PlayerController> fsm)
		{
			base.OnEnter(fsm);
			Debug.Log("进入战斗状态");
		}

		public override void OnExit(FSM<PlayerController> fsm)
		{
			base.OnExit(fsm);
			Debug.Log("离开战斗状态");
		}

		public override void OnUpdate(FSM<PlayerController> fsm)
		{
			base.OnUpdate(fsm);
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
			{
				fsm.ChangeState<FSMMove>();
				return;
			}

			if (Input.GetKey(KeyCode.J))
			{
				Debug.Log("攻击");
				return;
			}

			if (Input.GetKey(KeyCode.K))
			{
				Debug.Log("防御");
				return;
			}

			fsm.ChangeState<FSMIdle>();
		}
	}
}
