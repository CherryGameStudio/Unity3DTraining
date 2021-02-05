using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BTree
{
	/// <summary>
	/// 行为树顺序节点基类。
	/// </summary>
	public class BTSequenceNode : BTCompositeNode
	{
		protected int m_Index;

		public BTSequenceNode() : base()
		{
			m_Index = 0;
		}

		public override BTResult DoAction()
		{
			if (m_Children == null || m_Children.Count == 0)
			{
				return BTResult.Fail;
			}

			if (m_Index >= m_Children.Count)
			{
				Reset();
			}

			BTResult result = BTResult.None;
			for (int length = m_Children.Count; m_Index < length; m_Index++)
			{
				result = m_Children[m_Index].DoAction();
				if (result == BTResult.Success)
				{
					continue;
				}
				else if (result == BTResult.Running)
				{
					return BTResult.Running;
				}
				else
				{
					Reset();
					return BTResult.Fail;
				}
			}

			Reset();

			return BTResult.Success;
		}

		public void Reset()
		{
			m_Index = 0;
		}
	}
}
