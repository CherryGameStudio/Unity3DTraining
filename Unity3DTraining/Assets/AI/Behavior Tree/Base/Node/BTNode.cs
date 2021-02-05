using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BTree
{
	/// <summary>
	/// 行为树节点抽象基类。
	/// </summary>
	public abstract class BTNode
	{
		/// <summary>
		/// 节点行为。
		/// </summary>
		/// <returns>节点行为返回的结果。</returns>
		/// <remarks>
		/// 任何节点执行后，必须向其父节点报告执行结果：BTResult。
		/// 这个结果将左右整棵树的决策方向。
		/// 节点具体可以分为组合节点，装饰节点，条件节点和行为节点。
		/// </remarks>
		public abstract BTResult DoAction();
	}
}
