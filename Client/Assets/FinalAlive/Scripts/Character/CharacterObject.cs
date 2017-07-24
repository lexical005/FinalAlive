using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景内角色组件
/// </summary>
public class CharacterObject : BattleObject
{
    [System.NonSerialized]
    private bool m_AllowInput = true;

    /// <summary>
    /// 位移、旋转、跳跃相关
    /// </summary>
    [System.NonSerialized]
    private CharacterCompMotor m_CompMotor;

    /// <summary>
    /// 用户操作输入
    /// </summary>
    public CharacterCompInput m_CompInput
    {
        get;
        private set;
    }

    /// <summary>
    /// 执行初始化
    /// </summary>
    protected override void OnStart()
    {
        base.OnStart();
        
        m_CompInput = new CharacterCompInput();

        m_CompMotor = new CharacterCompMotor(this, gameObject.GetComponent<CharacterController>(), transform);
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <param name="deltaTime"></param>
    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }

    /// <summary>
    /// 固定更新
    /// </summary>
    /// <param name="deltaTime"></param>
    protected override void OnFixedUpdate(float deltaTime)
    {
        base.OnFixedUpdate(deltaTime);

        m_CompMotor.OnFixedUpdate();

        // Character movement, we do this here as we're updating physics stuff
        // if (allowInput && !characterData.playerDead)
        if (m_AllowInput)
        { // is playerDead necessary
            m_CompMotor.inputMoveDirection = transform.rotation * m_CompInput.moveDirection;

            //gameObject.transform.Rotate(m_CompInput.sightDirection.z * 2f, m_CompInput.sightDirection.x * 10f, 0);
            gameObject.transform.Rotate(0, m_CompInput.sightDirection.x * 90f, 0);
            m_CompInput.sightDirection.x = m_CompInput.sightDirection.y = m_CompInput.sightDirection.z = 0;
        }
    }

    public virtual void AllowInput(bool aState)
    {
        // set bool
        m_AllowInput = aState;
        // reset characterController(?) & characterMotor
        m_CompMotor.inputMoveDirection = Vector3.zero;
        // reset all necessary
    }

    public void OnFall()
    {
    }
    public void OnLand()
    {
    }
    public void OnJump()
    {
    }
}
