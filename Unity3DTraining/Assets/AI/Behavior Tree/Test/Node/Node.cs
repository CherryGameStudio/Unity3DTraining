using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
	NodeType nodeType = NodeType.Action;

	public abstract void Execute();
}
