using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设置节点在切换场景时不销毁
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
