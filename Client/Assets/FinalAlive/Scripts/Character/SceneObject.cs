using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景内的物件的
/// </summary>
public class SceneObject : MonoBehaviour
{
    /// <summary>
    /// 动画控制器
    /// </summary>
    protected Animator m_Animator;

    private void Start()
    {
        this.OnStart();
    }

    // Update is called once per frame
    protected void Update()
    {
        this.OnUpdate(Time.deltaTime);
    }

    protected void FixedUpdate()
    {
        this.OnFixedUpdate(Time.deltaTime);
    }

    /// <summary>
    /// 执行初始化
    /// </summary>
    protected virtual void OnStart()
    {
        m_Animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <param name="deltaTime"></param>
    protected virtual void OnUpdate(float deltaTime)
    {
    }

    /// <summary>
    /// 固定更新
    /// </summary>
    /// <param name="deltaTime"></param>
    protected virtual void OnFixedUpdate(float deltaTime)
    {
    }
}
