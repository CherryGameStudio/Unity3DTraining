using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// C# List使用单元测试脚本。
/// </summary>
public class Test_List : MonoBehaviour
{
    private List<int> m_List;

    void Start()
    {
		ExpandListCapacity();

		ListCtor();

		ListAddDelete();

		ListSearch();

		ListSort();

		ListOtherOperation();

		//string a = "a";
		//string b = null;
		//Debug.Log(a.CompareTo(b));// -1

		//int a1 = 1;
		//int a2 = 2;
		//Debug.Log(a1.CompareTo(a2));// -1

		
	}

	public static bool Even(int value) => (value % 2) == 0;
	public static bool Even2(int value)
	{
		return (value % 2) == 0;
	}

	/// <summary>
	/// List扩容原理。
	/// </summary>
	private void ExpandListCapacity()
	{
        Debug.Log("开始值类型List<T>测试");

		#region List<T>扩容原理
		Profiler.BeginSample("List<T>扩容");
		m_List = new List<int>();
        Debug.Log(string.Format("初始化列表后，列表的长度为{0}，列表的大小为{1}",m_List.Count,m_List.Capacity));

        m_List.Add(1);
        Debug.Log(string.Format("列表添加第一个元素后，列表的长度为{0}，列表的大小为{1}", m_List.Count, m_List.Capacity));

        int temp = m_List.Capacity;
		for (int i = 0; i < temp; i++)
		{
            m_List.Add(i);
		}
        Debug.Log(string.Format("给列表添加超过其容器的元素后，列表的长度为{0}，列表的大小为{1}", m_List.Count, m_List.Capacity));

		temp = m_List.Capacity;
		for (int i = 0; i < temp; i++)
		{
			m_List.Add(i);
		}
		Debug.Log(string.Format("再次给列表添加超过其容器的元素后，列表的长度为{0}，列表的大小为{1}", m_List.Count, m_List.Capacity));

		m_List.TrimExcess();
		Debug.Log(string.Format("TrimExcess后，列表的长度为{0}，列表的大小为{1}", m_List.Count, m_List.Capacity));
		Profiler.EndSample();
		#endregion
	}

	/// <summary>
	/// List构造函数。
	/// </summary>
	private void ListCtor()
	{
		m_List = new List<int>();
		Debug.Log(string.Format("创建了一个空的List，数组元素个数为{0}，数组容量大小为{1}", m_List.Count, m_List.Capacity));

		m_List = new List<int>(10);
		Debug.Log(string.Format("创建了一个指定数组容量为10的List，数组元素个数为{0}，数组容量大小为{1}", m_List.Count, m_List.Capacity));

		m_List = new List<int>(new List<int>() { 1, 2, 3 });
		Debug.Log(string.Format("创建了一个由另一个IEnumerable<T>拷贝过来的数组，数组元素个数为{0}，数组容量大小为{1}", m_List.Count, m_List.Capacity));
	}

	/// <summary>
	/// 列表增删函数。
	/// </summary>
	private void ListAddDelete()
	{
		Debug.Log("开始列表的增删测试");
		m_List = new List<int>();

		//添加
		m_List.Add(0);
		m_List.Add(1);
		TryForeachList(m_List);

		m_List.AddRange(new List<int> { 2, 3, 4 });
		TryForeachList(m_List);

		m_List.Insert(0, -1);
		TryForeachList(m_List);

		m_List.InsertRange(5, new List<int> { 6, 6, 6, 6, 6 });
		TryForeachList(m_List);

		//删除操作
		m_List.Remove(6);
		TryForeachList(m_List);

		m_List.RemoveAt(0);
		TryForeachList(m_List);

		m_List.RemoveRange(0, 3);
		TryForeachList(m_List);

		m_List.RemoveAll((int item) => item == 6);
		TryForeachList(m_List);
	}

	/// <summary>
	/// 列表的查找操作。
	/// </summary>
	private void ListSearch()
	{
		Debug.Log("开始列表查找操作的测试");
		m_List = new List<int> { 6, 5, 4, 3, 2, 1, 0, 6, 5, 4, 3, 2, 1, 0, };
		TryForeachList(m_List);

		Predicate<int> matchEven = (int item) => item % 2 == 0;//偶数筛选
		Predicate<int> matchOdd = (int item) => item % 2 == 1;//奇数筛选

		//Find
		Debug.Log("列表第一个偶数为"+m_List.Find(matchEven));
		Debug.Log("列表第一个奇数为" + m_List.Find(matchOdd));

		Debug.Log("列表最后一个偶数为" + m_List.FindLast(matchEven));
		Debug.Log("列表最后一个奇数为" + m_List.FindLast(matchOdd));

		Debug.Log("列表偶数为" + TryGetListStr(m_List.FindAll(matchEven)));
		Debug.Log("列表奇数为" + TryGetListStr(m_List.FindAll(matchOdd)));

		Debug.Log("列表从前往后第一个符合条件的偶数的索引为" + m_List.FindIndex(matchEven));
		Debug.Log("列表从前往后第一个符合条件的偶数的索引为" + m_List.FindIndex(5, matchEven));
		Debug.Log("列表从前往后第一个符合条件的偶数的索引为" + m_List.FindIndex(5,2,matchEven));

		Debug.Log("列表从后往前第一个符合条件的偶数的索引为" + m_List.FindLastIndex(matchEven));
		Debug.Log("列表从后往前第一个符合条件的偶数的索引为" + m_List.FindLastIndex(6, matchEven));
		Debug.Log("列表从后往前第一个符合条件的偶数的索引为" + m_List.FindLastIndex(6, 2, matchEven));

		//IndexOf
		Debug.Log("列表中从前往后第一个符合条件的元素值为6的索引为" + m_List.IndexOf(6));
		Debug.Log("列表中从前往后第一个符合条件的元素值为6的索引为" + m_List.IndexOf(6,5));
		Debug.Log("列表中从前往后第一个符合条件的元素值为6的索引为" + m_List.IndexOf(6,5,2));

		Debug.Log("列表中从后往前第一个符合条件的元素值为6的索引为" + m_List.LastIndexOf(6));
		Debug.Log("列表中从后往前第一个符合条件的元素值为6的索引为" + m_List.LastIndexOf(6, 5));
		Debug.Log("列表中从后往前第一个符合条件的元素值为6的索引为" + m_List.LastIndexOf(6, 5, 2));
	}

	/// <summary>
	/// 列表排序相关操作。
	/// </summary>
	private void ListSort()
	{
		//Sort
		Debug.Log("开始测试排序相关操作");
		m_List = new List<int> { 1, 5, 2, 3, 4 };
		m_List.Sort();
		TryForeachList(m_List);

		Comparison<int> comparisonForward = (int a, int b) =>
		{
			if (a == b)
			{
				return 0;
			}
			else if (a > b) 
			{
				return 1;
			}
			else
			{
				return -1;
			}
		};

		Comparison<int> comparisonReverse = (int a, int b) =>
		{
			if (a == b)
			{
				return 0;
			}
			else if (a < b)
			{
				return 1;
			}
			else
			{
				return -1;
			}
		};

		m_List = new List<int> { 1, 5, 2, 3, 4 };
		m_List.Sort(comparisonForward);
		TryForeachList(m_List);

		m_List = new List<int> { 1, 5, 2, 3, 4 };
		m_List.Sort(comparisonReverse);
		TryForeachList(m_List);

		m_List = new List<int> { 1, 5, 2, 3, 4 };
		m_List.Sort(new IntComparison(IntComparison.Args.Forward));
		TryForeachList(m_List);

		m_List = new List<int> { 1, 5, 2, 3, 4 };
		m_List.Sort(new IntComparison(IntComparison.Args.Reverse));
		TryForeachList(m_List);


		//BinarySearch
		m_List = new List<int> { 1, 5, 2, 3, 4 };
		Debug.Log(m_List.BinarySearch(1));
		//使用BinarySearch，集合序列必须是有序的，否则可能会出现错误的结果。而IndexOf不要求有序。
		Debug.Log(m_List.BinarySearch(5));// -6 
		Debug.Log(m_List.IndexOf(5));// 1
		Debug.Log(m_List.BinarySearch(2));
		Debug.Log(m_List.BinarySearch(3));
		Debug.Log(m_List.BinarySearch(4));

		//使用BinarySearch，如果没有找到对应Item则返回一个负整数。
		//该值按位取反(~)结果是“大于被查找元素的下一个元素”的索引，没有更大的值则是元素的总数。
		//这样就可以在列表中的特定位置方便的插入特定的值，同时保持排序状态。
		m_List = new List<int> { 1, 5, 3, 3,7, 9 };
		m_List.Sort();
		Debug.Log(~m_List.BinarySearch(2));
		Debug.Log(~m_List.BinarySearch(4));
		Debug.Log(~m_List.BinarySearch(6));
		Debug.Log(~m_List.BinarySearch(8));
		Debug.Log(~m_List.BinarySearch(10));

		//测试BinarySearch来保持序列的办法。
		TryForeachList(m_List);//1,3,3,5,7,9
		m_List.Insert(~m_List.BinarySearch(6), 6);
		TryForeachList(m_List);//1,3,3,5,6,7,9
	}

	public class IntComparison : IComparer<int>
	{
		public enum Args
		{
			Forward,
			Reverse,
		}

		Args m_Args = Args.Forward;

		public IntComparison(Args args)
		{
			m_Args = args;
		}

		public int Compare(int x, int y)
		{
			int temp;

			if (x == y)
			{
				temp = 0;
			}
			else if (x > y)
			{
				temp = 1;
			}
			else
			{
				temp = -1;
			}

			if (m_Args == Args.Forward)
			{
				return temp;
			}
			else
			{
				return -temp;
			}
		}
	}


	private void ListOtherOperation()
	{
		//Contains
		m_List = new List<int> { 1, 2, 3, 4, 5 };
		Debug.Log("是否存在1这个元素" + m_List.Contains(1));
		Debug.Log("是否存在8这个元素" + m_List.Contains(8));

		//CopyTo
		m_List = new List<int> { 1, 2, 3, 4, 5 };
		int[] temp = new int[5];
		m_List.CopyTo(temp);
		TryForeachList(temp);

		//Exists
		m_List = new List<int> { 1, 3, 5 };
		Debug.Log("是否存在奇数" + m_List.Exists((int item) => item % 2 == 1));
		m_List = new List<int> { 2, 4, 6 };
		Debug.Log("是否存在奇数" + m_List.Exists((int item) => item % 2 == 1));

		//ForEach
		m_List = new List<int> { 1, 2, 3 };
		TryForeachList(m_List);
		m_List.ForEach((int item) => item = item * 10);
		TryForeachList(m_List);

		List<string> strList = new List<string> { "z", "y", "f" };
		strList.ForEach((string item) => item += " wow!");
		TryForeachList(strList);

		//GetRange
		m_List = new List<int> { 1, 2, 3, 4, 5 };
		m_List = m_List.GetRange(1, 3);
		TryForeachList(m_List);

		//ToArray
		//略

		//TrueForAll
		m_List = new List<int> { 1, 3, 5 };
		Debug.Log("是否全是奇数" + m_List.TrueForAll((int item) => item % 2 == 1));
		m_List = new List<int> { 1, 3, 6 };
		Debug.Log("是否全是奇数" + m_List.TrueForAll((int item) => item % 2 == 1));
	}

	private void TryForeachList(IEnumerable list)
	{
		string temp = "";

		IEnumerator enumerator = list.GetEnumerator();
		while (enumerator.MoveNext())
		{
			temp = temp + enumerator.Current + ",";
		}

		Debug.Log("此时列表元素为："+temp);
	}

	private string TryGetListStr(IEnumerable list)
	{
		string temp = "";

		IEnumerator enumerator = list.GetEnumerator();
		while (enumerator.MoveNext())
		{
			temp = temp + enumerator.Current + ",";
		}

		return temp;
	}
}
