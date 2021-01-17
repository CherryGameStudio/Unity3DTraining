using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.WayFinding
{
	public class Node
	{
		public Node Parent;
		public bool IsVisit;
		public Vector3 Value;
		public bool IsWalkable;
		public GameObject Go;
	}
}
