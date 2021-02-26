using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// C# LinkedList使用单元测试脚本。
/// </summary>
public class Test_LinkedList : MonoBehaviour
{
    LinkedList<int> m_IntLinkedList;

    // Start is called before the first frame update
    void Start()
    {
        //IntLinkedListTest();






	}

    // Update is called once per frame
    void Update()
    {

    }

    private void IntLinkedListTest()
	{
        m_IntLinkedList = new LinkedList<int>();
        m_IntLinkedList.AddFirst(1);
        TryForeachIntLinkedList();

	}

    private void TryForeachIntLinkedList()
	{
		if (m_IntLinkedList != null && m_IntLinkedList.Count >= 0)
		{
            string tempStr = "";

			foreach (int item in m_IntLinkedList)
			{
                tempStr = tempStr + item + ",";
			}

            Debug.Log(tempStr);
		}
	}
}
