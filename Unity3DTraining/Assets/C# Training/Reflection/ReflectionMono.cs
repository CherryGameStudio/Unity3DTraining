using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Essential.Reflection
{
    public class ReflectionMono : MonoBehaviour
    {
        void Start()
        {
            TestGetType();
        }

        /// <summary>
        /// 测试GetType()
        /// </summary>
        private void TestGetType()
		{
			MyReflectionClass myReflectionClass = new MyReflectionClass();
			Type type = myReflectionClass.GetType();

            //类基本信息
            Debug.Log("类名为：" + type.Name);
            Debug.Log("程序集名为：" + type.Assembly);
            Debug.Log("类全名称为：" + type.FullName);
            Debug.Log("命名空间名为：" + type.Namespace);
            Debug.Log("直接基类全名为：" + type.BaseType);
            Debug.Log("类接口全名为："+type.GetInterfaces()[0]);

            //类成员信息
            Debug.Log("类中静态字段a的值为：" + type.GetField("a").GetValue(null));//GetField获得的类型是FieldInfo，必须获取公共字段，若字段不是静态则需要传入类的实例object。
            Debug.Log("类中静态属性B的值为：" + type.GetProperty("B").GetValue(null));//GetProperty获得的类型是PropertyInfo，必须获取公共属性，若属性不是静态则需要传入类的实例object。
            Debug.Log("类中方法LogMessage为：" + type.GetMethod("LogMessage").Name);//GetMethod获得的类型是MethodInfo，必须获取公共属性。

            //类成员使用
            type.GetField("a").SetValue(null, 10);
            Debug.Log("类中静态字段a改变后的值为：" + type.GetField("a").GetValue(null));
            type.GetProperty("B").SetValue(null, 20);
            Debug.Log("类中静态属性B改变后的值为：" + type.GetProperty("B").GetValue(null));
            type.GetMethod("LogMessage").Invoke(null, null);
        }

        private interface IMyReflectionClass { }
        private abstract class BaseMyReflectionClass { }

        private sealed class MyReflectionClass : BaseMyReflectionClass,IMyReflectionClass
		{
            public static int a = 1;

            private static int b = 2;
            public static int B
            {
                get { return b; }
                set { b = value; }
            }

            public static void LogMessage()
			{
                Debug.Log("成功调用LogMessage");
			}
		}
    }
}
