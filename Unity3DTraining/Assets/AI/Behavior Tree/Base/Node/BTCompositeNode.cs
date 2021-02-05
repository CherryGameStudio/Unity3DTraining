using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BTree
{
	/// <summary>
	/// 行为树组合节点抽象基类。
	/// </summary>
	public abstract class BTCompositeNode : BTNode
	{
		protected List<BTNode> m_Children;

		public BTCompositeNode()
		{
			m_Children = new List<BTNode>();
		}

		public void AddChildNode(BTNode node)
		{
			m_Children.Add(node);
		}

		public void RemoveChildNode(BTNode node)
		{
			m_Children.Remove(node);
		}

		public void Clear()
		{
			m_Children.Clear();
		}

		public override abstract BTResult DoAction();
	}
}
