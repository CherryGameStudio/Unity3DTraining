using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeTest : MonoBehaviour
{
    public Node RootNode;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("根节点启动！");

        //初始化所有节点脚本
    }

    float time = 0;
    float waitTime = 1;//1s轮询一次
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
		if (waitTime - time < 0)
		{
            time = 0;
            Debug.Log("发送行为请求");
            RootNode.Execute();
		}
    }
}
