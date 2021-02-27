using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 有关集合的共有知识点。
/// </summary>
public class Test_AboutCollection : MonoBehaviour
{
    void Start()
    {
		InitCollection();
		ForeachTest();
		ForeachCustomIterator();
    }

	/// <summary>
	/// 集合初始化器。
	/// </summary>
	/// <remarks>
	/// 集合初始化器内部是基于继承自ICollection<T>或IEnumerable<T>的Add()方法实现的。
	/// </remarks>
	private void InitCollection()
	{
		//List<T>集合初始化器
		List<string> m_List = new List<string>()
		{
			"z",
			"y",
			"f",
		};

		//Dictionary<K,V>集合初始化器
		Dictionary<int, string> m_Dic = new Dictionary<int, string>()
		{
			[1] = "z",
			[2] = "y",
			[3] = "f",
		};
	}

	/// <summary>
	/// Foreach循环测试。
	/// </summary>
	private void ForeachTest()
	{
		List<string> m_List = new List<string>()
		{
			"z",
			"y",
			"f",
		};

		//标准foreach语句
		foreach (var item in m_List)
		{
			Debug.Log(item);

			//编译器不允许对迭代变量赋值。
			//item = "zyf";

			//编译阶段不报错，但是运行时会报错，不允许迭代时更改集合。
			//m_List.Add("zyf");
		}

		#region 由C#编译器将foreach语句转译成的CIL代码所对应的C#代码
		List<string>.Enumerator enumerator;
		IDisposable disposable;

		enumerator = m_List.GetEnumerator();
		try
		{
			string str;
			while (enumerator.MoveNext())
			{
				str = enumerator.Current;
				Debug.Log(str);
			}
		}
		finally
		{
			disposable = enumerator as IDisposable;
			disposable?.Dispose();
		}
		#endregion
	}

	private void ForeachCustomIterator()
	{
		Debug.Log("测试IteratorSample迭代器");
		IteratorSample sample = new IteratorSample();
		foreach (var item in sample)
		{
			Debug.Log(item);
		}

		Debug.Log("测试IteratorSample2迭代器");
		IteratorSample2<string> sample2 = new IteratorSample2<string>("zyf", "rj");
		foreach (var item in sample2)
		{
			Debug.Log(item);
		}

		//结果显示，使用foreach每次迭代会调用MoveNext()，返回true后，迭代变量的值由GetEnunerator决定。
		//当MoveNext()返回false后，迭代结束，会调用Dispose方法，清理状态。
		Debug.Log("测试IteratorSample2plus迭代器");
		IteratorSample2plus<string> sample2plus = new IteratorSample2plus<string>("zyf", "rj");
		foreach (var item in sample2plus)
		{
			Debug.Log(item);
		}

		IteratorSample3 sample3 = new IteratorSample3();
		foreach (var item in sample3)
		{
			Debug.Log(item);
		}

		IteratorSample3plus sample3plus = new IteratorSample3plus();
		foreach (var item in sample3plus)
		{
			Debug.Log(item);
		}
	}
}

#region 自定义迭代器
/// <summary>
/// 迭代器示例1。
/// </summary>
public class IteratorSample : IEnumerable<string>
{
	public IEnumerator<string> GetEnumerator()
	{
		yield return "z";
		yield return "y";
		yield return "f";
		yield return "zyf";
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

public class IteratorSampleplus : IEnumerable<string>
{
	public IEnumerator<string> GetEnumerator()
	{
		yield return "z";
		yield return "y";
		yield return "f";
		yield return "zyf";
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	private sealed class Enumerator : IEnumerator<string>
	{
		public string Current => throw new NotImplementedException();

		object IEnumerator.Current => throw new NotImplementedException();

		public void Dispose()
		{
			Debug.Log("Dispose");
		}

		public bool MoveNext()
		{
			throw new NotImplementedException();
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}
	}
}

/// <summary>
/// 迭代器示例2。
/// </summary>
/// <typeparam name="T"></typeparam>
public class IteratorSample2<T> : IEnumerable<T>
{
	public IteratorSample2(T first , T second)
	{
		First = first;
		Second = second;
	}

	public T First { get; }
	public T Second { get; }

	public IEnumerator<T> GetEnumerator()
	{
		yield return First;
		yield return Second;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

/// <summary>
/// 示例二与迭代器的CIL代码等价的C#代码。
/// </summary>
/// <typeparam name="T"></typeparam>
public class IteratorSample2plus<T> : IEnumerable<T>
{
	public IteratorSample2plus(T first, T second)
	{
		First = first;
		Second = second;
	}

	public T First { get; }
	public T Second { get; }

	public IEnumerator<T> GetEnumerator()
	{
		ListEnumerator result = new ListEnumerator(0);
		result._Sample = this;
		return result;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	private sealed class ListEnumerator : IEnumerator<T>
	{
		public IteratorSample2plus<T> _Sample;
		T _Current;
		int _ItemCount;

		public ListEnumerator(int itemCount)
		{
			_ItemCount = itemCount;
		}

		public T Current
		{
			get { Debug.Log("Current"); return _Current; }
		}

		object IEnumerator.Current
		{
			get { return _Current; }
		}

		public bool MoveNext()
		{
			Debug.Log("MoveNext");
			switch (_ItemCount)
			{
				case 0:
					_Current = _Sample.First;
					_ItemCount++;
					return true;
				case 1:
					_Current = _Sample.Second;
					_ItemCount++;
					return true;
				default:
					return false;
			}
		}

		public void Dispose()
		{
			Debug.Log("Dispose");
		}

		public void Reset()
		{
			Debug.Log("Reset");
		}
	}
}

public class IteratorSample3 : IEnumerable
{
	public IEnumerator GetEnumerator()
	{
		for (int i = 0; i < 10; i++)
		{
			yield return i;
		}
	}
}

public class IteratorSample3plus : IEnumerable
{
	public IEnumerator GetEnumerator()
	{
		return new Enumerator(0);
	}

	private sealed class Enumerator : IEnumerator
	{
		private int state;
		private int current;
		public int tempValue;

		public object Current
		{
			get { return current; }
		}

		public Enumerator(int state)
		{
			this.state = state;
		}

		public bool MoveNext()
		{
			switch (state)
			{
				case 0:
					state = -1;
					tempValue = 0;

					Go:
					if (state != -1)
					{
						state = -1;
						tempValue++;
					}

					while (tempValue < 10)
					{
						current = tempValue;
						state = 1;
						return true;
					}

					break;
				case 1:
					goto Go;
			}

			return false;
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}
	}
}

#endregion
