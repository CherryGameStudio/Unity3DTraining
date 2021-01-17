using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace AI.WayFinding {
	public class DFS : MonoBehaviour
	{
		public Vector3 StartTrans;
		public Vector3 EndTrans;

		private Dictionary<Vector3, Node> m_MapNodes;
		private List<Node> m_CachedNodes;
		private Transform canvasTrans;

		private void Awake()
		{
			m_MapNodes = new Dictionary<Vector3, Node>();
			m_CachedNodes = new List<Node>();
			canvasTrans = GameObject.Find("Canvas").transform;
		}

		private void Start()
		{
			for (int i = 0; i < 20; i++)
			{
				for (int j = 0; j < 20; j++)
				{
					GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(GetNodePath(out bool isWalkable));
					go = Instantiate(go);

					Node node = new Node();
					node.IsVisit = false;
					node.Parent = null;
					node.Value = new Vector3(i * 20, j * 20, 0);
					node.IsWalkable = isWalkable;
					node.Go = go;
					m_MapNodes.Add(node.Value, node);

					go.transform.SetParent(canvasTrans);
					go.transform.localPosition = node.Value;
				}
			}

			List<Node> passNodeList = new List<Node>();
			m_MapNodes.TryGetValue(StartTrans, out Node startNode);
			m_MapNodes.TryGetValue(EndTrans, out Node endNode);

			long time = DateTime.Now.Ticks;
			Search(startNode, endNode, ref passNodeList);
			Debug.Log(DateTime.Now.Ticks - time);

			if (passNodeList.Count == 0)
			{
				Debug.LogError("终点位置不可到达");
			}
			foreach (Node node in passNodeList)
			{
				Destroy(node.Go);
				GameObject go = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AI/AIPrefab/Way-finding/A.prefab"));
				go.transform.SetParent(canvasTrans);
				go.transform.localPosition = node.Value;
				node.Go = go;
			}
		}

		private string GetNodePath(out bool isWalkable)
		{
			int a = UnityEngine.Random.Range(0, 10);
			if (a>= 0&&a<=7)
			{
				isWalkable = true;
				return "Assets/AI/AIPrefab/Way-finding/星.prefab";
			}

			if (a>7&&a<10)
			{
				isWalkable = false;
				return "Assets/AI/AIPrefab/Way-finding/@.prefab";
			}

			isWalkable = false;
			return "";
		}

		private void Search(Node start,Node end,ref List<Node> passNodeList)
		{
			passNodeList.Clear();

			foreach (var node in m_MapNodes)
			{
				BFSSearch(node.Value, start, end, ref passNodeList);
				if (passNodeList.Count >= 0)
				{
					return;
				}
			}
		}

		private void BFSSearch(Node currentNode,Node start,Node end,ref List<Node> passNodeList)
		{
			Queue<Node> queue = new Queue<Node>();
			queue.Enqueue(currentNode);

			while (queue.Count > 0) 
			{
				Node head = queue.Dequeue();
				GetNeighborNode(head, m_CachedNodes);
				Node[] nodes = m_CachedNodes.ToArray();
				for (int i = 0; i < nodes.Length; i++)
				{
					if (nodes[i].IsWalkable == true)
					{
						nodes[i].Parent = head;
						queue.Enqueue(nodes[i]);

						if (nodes[i].Value == end.Value)
						{
							/* 
							//不影响原生地图信息的方法
							Node node = new Node();
							node.IsVisit = nodes[i].IsVisit;
							node.Parent = nodes[i].Parent;
							node.Value = nodes[i].Value;
							node.IsWalkable = nodes[i].IsWalkable;
							node.Go = nodes[i].Go;

							while (node.Value != start.Value)
							{
								passNodeList.Add(node);

								node = node.Parent;
								if (node == null)
								{
									Debug.LogError("或道路被堵死");
									return;
								}
								Node tempNode = new Node();
								tempNode.IsVisit = node.IsVisit;
								tempNode.Parent = node.Parent;
								tempNode.Value = node.Value;
								tempNode.IsWalkable = node.IsWalkable;
								tempNode.Go = node.Go;
							}
							*/

							if (nodes[i] == null)
							{
								Debug.LogError("或道路被堵死");
							}
							while (nodes[i].Value != start.Value)
							{
								passNodeList.Add(nodes[i]);
								nodes[i] = nodes[i].Parent;
							}
							passNodeList.Add(start);
							passNodeList.Reverse();
							return;
						}
					}
				}
			}
		}

		private void GetNeighborNode(Node node , List<Node> cachedNodes)
		{
			Node tempNode;
			if (!m_MapNodes.ContainsValue(node))
			{
				Debug.LogError("使用了不在地图中的Node");
				return;
			}
			
			cachedNodes.Clear();
			if (m_MapNodes.TryGetValue(node.Value + new Vector3(0, 20), out tempNode) && tempNode.IsVisit == false) 
			{
				cachedNodes.Add(tempNode);
				tempNode.IsVisit = true;
			}
			if (m_MapNodes.TryGetValue(node.Value + new Vector3(20, 0), out tempNode) && tempNode.IsVisit == false)
			{
				cachedNodes.Add(tempNode);
				tempNode.IsVisit = true;
			}
			if (m_MapNodes.TryGetValue(node.Value - new Vector3(0, 20), out tempNode) && tempNode.IsVisit == false)
			{
				cachedNodes.Add(tempNode);
				tempNode.IsVisit = true;
			}
			if (m_MapNodes.TryGetValue(node.Value - new Vector3(20, 0), out tempNode) && tempNode.IsVisit == false)
			{
				cachedNodes.Add(tempNode);
				tempNode.IsVisit = true;
			}

			//cachedNodes.Add(node);
			node.IsVisit = true; 
		}
	} 
}
