using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BTree
{
	/// <summary>
	/// 行为树装饰节点基类。
	/// </summary>
	public class BTDecorator : BTNode
	{
		protected BTNode m_Child;
		public BTDecorator()
		{
			m_Child = null;
		}

		/// <summary>
		/// 装饰节点的返回结果与子节点返回结果有关。
		/// </summary>
		/// <returns>返回结果。</returns>
		public override BTResult DoAction()
		{
			return m_Child.DoAction();
		}

		protected void SetChild(BTNode node)
		{
			m_Child = node;
		}
	}
}
