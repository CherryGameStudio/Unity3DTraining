using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BTree
{
	/// <summary>
	/// 行为树并行顺序节点基类。
	/// </summary>
	public class BTParallelSequenceNode : BTCompositeNode
	{
		protected List<BTNode> m_WaitNodes;
		protected bool m_IsSuccess;

		public BTParallelSequenceNode() : base()
		{
			m_WaitNodes = new List<BTNode>();
			m_IsSuccess = false;
		}

		public override BTResult DoAction()
		{
			if (m_Children == null || m_Children.Count == 0)
			{
				return BTResult.Fail;
			}

			BTResult result = BTResult.None;
			for (int i = 0 , length = m_Children.Count; i < length; i++)
			{
				result = m_Children[i].DoAction();

				switch (result)
				{
					case BTResult.Success:
						m_IsSuccess = true;
						return result;
					case BTResult.Running:
						m_WaitNodes.Add(m_Children[i]);
						break;
					default:
						break;
				}
			}

			result = CheckResult();
			Reset();
			return result;
		}

		private BTResult CheckResult()
		{
			return m_IsSuccess ? BTResult.Success : BTResult.Fail;
		}

		private void Reset()
		{
			m_WaitNodes.Clear();
			m_IsSuccess = false;
		}
	}
}
