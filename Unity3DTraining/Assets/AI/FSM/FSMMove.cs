using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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

		private MoveState curState;
		private int curMoveInputIndex;
		private float speed = 0.01f;

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
				fsm.Owner.AnimMixer.SetInputWeight(curMoveInputIndex, 0f);
				curMoveInputIndex = 3;
				fsm.Owner.AnimMixer.SetInputWeight(curMoveInputIndex, 1f);
				fsm.Owner.transform.transform.position += new Vector3(0, speed, 0);
				Debug.Log("上移");
				return;
			}

			if (Input.GetKey(KeyCode.S))
			{
				fsm.Owner.AnimMixer.SetInputWeight(curMoveInputIndex, 0f);
				curMoveInputIndex = 0;
				fsm.Owner.AnimMixer.SetInputWeight(curMoveInputIndex, 1f);
				fsm.Owner.transform.transform.position += new Vector3(0, -speed, 0);
				Debug.Log("下移");
				return;
			}

			if (Input.GetKey(KeyCode.A))
			{
				fsm.Owner.AnimMixer.SetInputWeight(curMoveInputIndex, 0f);
				curMoveInputIndex = 1;
				fsm.Owner.AnimMixer.SetInputWeight(curMoveInputIndex, 1f);
				fsm.Owner.transform.transform.position += new Vector3(-speed, 0, 0);
				Debug.Log("左移");
				return;
			}

			if (Input.GetKey(KeyCode.D))
			{
				fsm.Owner.AnimMixer.SetInputWeight(curMoveInputIndex, 0f);
				curMoveInputIndex = 2;
				fsm.Owner.AnimMixer.SetInputWeight(curMoveInputIndex, 1f);
				fsm.Owner.transform.transform.position += new Vector3(speed, 0, 0);
				Debug.Log("右移");
				return;
			}

			fsm.ChangeState<FSMIdle>();
			fsm.Owner.AnimMixer.SetInputWeight(curMoveInputIndex, 0f);
		}
	}
}
