using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自定义列表集合测试脚本。
/// </summary>
public class Test_CustomList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		//测试集合初始化器
		Debug.Log("测试集合初始化器");
		CustomList<int> m_List = new CustomList<int>()
		{
			1,
			2,
			3,
		};
		TryForeach(m_List);

		//测试扩容
		m_List = new CustomList<int>() { 1 };
		int temp = m_List.Capacity;
		for (int i = 0; i < temp; i++)
		{
			m_List.Add(i);
		}
		TryForeach(m_List);
		Debug.Log("数组容量为" + m_List.Capacity);
		temp = m_List.Capacity;
		for (int i = 0; i < temp; i++)
		{
			m_List.Add(i);
		}
		TryForeach(m_List);
		Debug.Log("数组容量为" + m_List.Capacity);

		//测试常用函数以及索引器
		m_List = new CustomList<int>() { 1, 2, 3, 4, 5 };
		Debug.Log(string.Format("第一个元素和最后一个元素分别是{0},{1}", m_List[0], m_List[m_List.Count - 1]));

		Debug.Log("初始列表元素组成为" + m_List);
		m_List.Remove(5);
		Debug.Log("移除5这个元素后，列表为" + m_List);
		m_List.RemoveAt(0);
		Debug.Log("移除索引为0的元素后，列表为" + m_List);
		Debug.Log("此时2这个元素在列表的索引为" + m_List.IndexOf(2));
		m_List.Add(6);
		Debug.Log("添加6这个元素后，列表为" + m_List);
		TryForeach(m_List);
	}

	private void TryForeach(IEnumerable list)
	{
		Debug.Log("开始此次循环");
		foreach (var item in list)
		{
			Debug.Log(item);
		}
	}
}

public class CustomList<T> : IEnumerable<T>
{
	private T[] m_Array;
	private int m_ItemCount;

	public int Count
	{
		get { return m_ItemCount; }
	}

	public int Capacity
	{
		get { return m_Array.Length; }
		//set
	}

	public CustomList()
	{
		m_Array = new T[0];
		m_ItemCount = 0;
	}

	public CustomList(int capacity)
	{
		m_Array = new T[capacity];
		m_ItemCount = 0;
	}

	public T this[int index]
	{
		get { return m_Array[index]; }
	}

	public void Add(T item)
	{
		if (m_ItemCount == Capacity)
		{
			if (Capacity == 0)
			{
				//数组为零，则扩充为4
				m_Array = new T[4];
			}
			else
			{
				//二倍扩容
				T[] tempArray = new T[2 * Capacity];
				m_Array.CopyTo(tempArray, 0);
				m_Array = tempArray;
			}
		}

		m_Array[m_ItemCount] = item;
		m_ItemCount++;
	}

	public int IndexOf(T item)
	{
		for (int i = 0; i < m_ItemCount; i++)
		{
			if (m_Array[i].Equals(item))
			{
				return i;
			}
		}

		return -1;
	}

	public void Remove(T item)
	{
		RemoveAt(IndexOf(item));
	}

	public void RemoveAt(int index)
	{
		if (index < m_ItemCount && index >= 0)
		{
			//m_ItemCount-1是因为若m_ItemCount等于Capacity，那么i一定要小于等于m_ItemCount - 2
			for (int i = index; i < m_ItemCount - 1; i++)
			{
				m_Array[i] = m_Array[i + 1];
			}

			m_ItemCount--;
		}
		else
		{
			Debug.LogError("超出移除元素的索引值");
		}
	}

	public override string ToString()
	{
		string temp = "";
		for (int i = 0; i < m_ItemCount; i++)
		{
			if (i == m_ItemCount - 1)
			{
				temp += m_Array[i];
			}
			else
			{
				temp = temp + m_Array[i] + ",";
			}
		}

		return temp;
	}

	public IEnumerator<T> GetEnumerator()
	{
		Enumerator enumerator = new Enumerator(this);
		return enumerator;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public struct Enumerator : IEnumerator<T>
	{
		//private T m_Current;
		private CustomList<T> m_List;
		private int index;

		public Enumerator(CustomList<T> list)
		{
			index = -1;
			m_List = list;
			//m_Current = default(T);
		}

		public T Current
		{
			get { return m_List[index]; }
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}

		public void Dispose()
		{
			index = -1;
			Debug.Log("Dispose");
		}

		public bool MoveNext()
		{
			Debug.Log("MoveNext");
			if (m_List.m_ItemCount == 0)
			{
				return false;
			}

			index++;
			if (index < m_List.m_ItemCount)
			{
				return true;
			}

			return false;
		}

		public void Reset()
		{
			Debug.Log("Reset");
		}
	}
}
