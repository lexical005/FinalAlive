using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

/// <summary>
/// PVP1主界面
/// </summary>
public class UIHudBattlePVP1 : UIWindowTemplate<UIHudBattlePVP1, NUIExport.hudBattlePVP1.UI_main>
{
    /// <summary>
    /// 左侧控制移动的摇杆
    /// </summary>
    private NUIInternal.Joystick m_LeftMoveDirJoystick;

    /// <summary>
    /// 右侧控制朝向的摇杆
    /// </summary>
    private NUIInternal.Joystick m_RightSightDirJoystick;

    /// <summary>
    /// 私有构造
    /// </summary>
    public UIHudBattlePVP1() : base(NUIExport.hudBattlePVP1.UI_main.CreateInstance)
    {
        NFramework.UIManager.AddPackage("hudBattlePVP1", ELifeScope.Scene);
    }

    /// <summary>
    /// 初始化完毕，创建实例
    /// </summary>
    protected override void OnInit()
    {
        base.OnInit();

        NUIInternal.JoystickData left = new NUIInternal.JoystickData
        {
            m_touchArea = mainComp.m_leftJoystick,
            m_btnJoystickMove = mainComp.m_leftJoystick.m_circle,
            m_imgJoystickMove = mainComp.m_leftJoystick.m_circle.m_circle,
            m_imgJoystickCenter = mainComp.m_leftJoystick.m_center,
            m_radius = 150,
        };
        m_LeftMoveDirJoystick = new NUIInternal.Joystick(left);
        m_LeftMoveDirJoystick.OnMove.Add(OnLeftMoveDirJoystickMove);
        m_LeftMoveDirJoystick.OnEnd.Add(OnLeftMoveDirJoystickEnd);

        NUIInternal.JoystickData right = new NUIInternal.JoystickData
        {
            m_touchArea = mainComp.m_rightJoystick,
            m_btnJoystickMove = mainComp.m_rightJoystick.m_circle,
            m_imgJoystickMove = mainComp.m_rightJoystick.m_circle.m_circle,
            m_imgJoystickCenter = mainComp.m_rightJoystick.m_center,
            m_radius = 150,
        };
        m_RightSightDirJoystick = new NUIInternal.Joystick(right);
        m_RightSightDirJoystick.OnMove.Add(OnRightSightDirJoystickMove);
        m_RightSightDirJoystick.OnEnd.Add(OnRightSightDirJoystickEnd);
    }

    /// <summary>
    /// 左侧控制移动的摇杆发生了移动
    /// </summary>
    /// <param name="context"></param>
    void OnLeftMoveDirJoystickMove(EventContext context)
    {
        Debug.LogFormat("OnLeftMoveDirJoystickMove: {0}", context.data);
    }

    /// <summary>
    /// 左侧控制移动的摇杆结束了移动
    /// </summary>
    void OnLeftMoveDirJoystickEnd()
    {
        Debug.LogFormat("OnLeftMoveDirJoystickEnd");
    }

    /// <summary>
    /// 左侧控制朝向的摇杆发生了移动
    /// </summary>
    /// <param name="context"></param>
    void OnRightSightDirJoystickMove(EventContext context)
    {
        Debug.LogFormat("OnRightSightDirJoystickMove: {0}", context.data);
    }

    /// <summary>
    /// 左侧控制朝向的摇杆结束了移动
    /// </summary>
    void OnRightSightDirJoystickEnd()
    {
        Debug.LogFormat("OnRightSightDirJoystickEnd");
    }
}
