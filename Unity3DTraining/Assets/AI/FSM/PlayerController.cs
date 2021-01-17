using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public class PlayerController : MonoBehaviour
    {
        FSM<PlayerController> m_FSM;

        // Start is called before the first frame update
        void Start()
        {
            m_FSM = FSM<PlayerController>.CreateFSM(this, new FSMIdle(), new FSMFight(), new FSMMove());
            m_FSM.StartState<FSMIdle>();
            
        }

        // Update is called once per frame
        void Update()
        {
            m_FSM.OnUpdate();
        }
    }
}
