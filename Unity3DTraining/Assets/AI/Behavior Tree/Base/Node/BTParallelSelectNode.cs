using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BTree
{
	/// <summary>
	/// 行为树并行选择节点。
	/// </summary>
	public class BTParallelSelectNode : BTCompositeNode
	{
		protected List<BTNode> m_WaitNodes;
		protected bool m_IsFail;

		public BTParallelSelectNode() : base()
		{
			m_WaitNodes = new List<BTNode>();
			m_IsFail = false;
		}

		public override BTResult DoAction()
		{
			if (m_Children == null || m_Children.Count == 0)
			{
				return BTResult.Fail;
			}

			BTResult result = BTResult.None;
			for (int i = 0, length = m_Children.Count; i < length; i++)
			{
				result = m_Children[i].DoAction();

				switch (result)
				{
					case BTResult.Success:
						break;
					case BTResult.Running:
						m_WaitNodes.Add(m_Children[i]);
						break;
					default:
						m_IsFail = true;
						break;
				}
			}

			if (m_WaitNodes.Count > 0)
			{
				return BTResult.Running;
			}

			result = CheckResult();
			Reset();
			return result;
		}

		private BTResult CheckResult()
		{
			return m_IsFail ? BTResult.Fail : BTResult.Success;
		}

		private void Reset()
		{
			m_WaitNodes.Clear();
			m_IsFail = false;
		}
	}
}
