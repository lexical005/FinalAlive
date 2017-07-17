using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 窗口界面基础类
/// 
/// 窗口动态创建是指初始时，仅指定窗口需要使用的资源，等窗口需要显示时才实际开始构建窗口的内容。
/// 在窗口的构造函数中调用Window.addUISource，IUISource是一个接口，用户需要自行实现载入相关UI包的逻辑。
/// 当窗口第一次显示之前，IUISource的加载方法将会被调用，并等待载入完成后才返回执行Window.OnInit。
/// 
/// Window.show->窗口已初始化->Window.doShowAnimation->Window.onShown
/// Window.show->窗口未初始化->窗口为动态创建->IUISource.load(在窗口的构造函数中调用Window.addUISource设置)->Window.onInit->Window.doShowAnimation->Window.onShown
/// Window.show->窗口未初始化->窗口非动态创建->Window.onInit->Window.doShowAnimation->Window.onShown
/// 
/// Window.hide->Window.doHideAnimation->Window.onHide
/// 
/// ←↑→↓
/// 
/// Window.Show
///     ↓
/// 窗口已初始化------>否------->是否动态创建-->是-->IUISource.Load
///     ↓                            ↓                      ↓
/// Window.DoShowAnimation<-----Window.OnInit<---加载完成<---
///     ↓
/// Window.OnShown
/// 如果你需要窗口显示时播放动画效果，那么覆盖DoShowAnimation编写你的动画代码，并且在动画结束后调用OnShown
/// 覆盖OnShown编写其他需要在窗口显示时处理的业务逻辑
/// 
/// 
/// Window.Hide
///     ↓
/// Window.DoHideAnimation
///     ↓
/// Window.Onhide
/// 
/// </summary>
public class UIWindow : FairyGUI.Window
{
    /// <summary>
    /// 生命周期
    /// </summary>
    public readonly ELifeScope lifeScope = ELifeScope.Scene;


    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="lifeScope"></param>
    protected UIWindow(ELifeScope lifeScope)
    {
        this.lifeScope = lifeScope;
    }

    /// <summary>
    /// 销毁
    /// </summary>
    override public void Dispose()
    {
        Debug.Log("Dispose");

        base.Dispose();
    }

    /// <summary>
    /// 销毁
    /// </summary>
    /// <param name="lifeScope"></param>
    /// <returns></returns>
    public bool Dispose(ELifeScope lifeScope)
    {
        if (this.lifeScope == lifeScope)
        {
            Dispose();
            return true;
        }
        return false;
    }


    /// <summary>
    /// 界面出现动画表现，派生类覆盖此方法，以编写自己的出现动画表现（派生实现时，不要调用基类的DoShowAnimation方法）
    /// 在动画结束后，调用OnShown，以触发OnShown显示完成事件
    /// </summary>
    protected override void DoShowAnimation()
    {
        this.OnShown();
    }

    /// <summary>
    /// 窗口显示完成事件，一个窗口实例，随着显示/隐藏操作，可触发多次
    /// </summary>
    protected override void OnShown()
    {
        base.OnShown();
    }

    /// <summary>
    /// 界面消失动画表现，派生类覆盖此方法，以编写自己的消失动画表现（派生实现时，不要调用基类的DoShowAnimation方法）
    /// 在动画表现结束后，调用HideImmediately，以触发隐藏
    /// </summary>
    protected override void DoHideAnimation()
    {
        this.HideImmediately();
    }

    /// <summary>
    /// 窗口消失完成事件，一个窗口实例，随着显示/隐藏操作，可触发多次
    /// </summary>
    protected override void OnHide()
    {
        base.OnHide();
    }

    /// <summary>
    /// 关闭窗口界面
    /// </summary>
    protected virtual void OnCloseWindow()
    {
    }
}


public class UIWindowTemplate<WindowType, WindowMainComponentType> : UIWindow
    where WindowType : UIWindowTemplate<WindowType, WindowMainComponentType>, new()
    where WindowMainComponentType : FairyGUI.GComponent
{
    protected delegate WindowMainComponentType MainComponentCreator();

    /// <summary>
    /// 主界面组件实例
    /// </summary>
    protected WindowMainComponentType mainComp;

    /// <summary>
    /// 主界面组件的创建方法
    /// </summary>
    /// <returns></returns>
    protected static MainComponentCreator mainComponentCreator = null;

    protected UIWindowTemplate(MainComponentCreator creator, ELifeScope lifeScope = ELifeScope.Scene)
        : base(lifeScope)
    {
        mainComponentCreator = creator;
        this.name = typeof(WindowType).FullName;
    }


    /// <summary>
    /// 创建实例
    /// </summary>
    /// <returns></returns>
    public static WindowType CreateInstance()
    {
        return new WindowType();
    }


    /// <summary>
    /// 窗口初始化完毕事件，一个窗口实例，只会触发一次
    /// </summary>
    protected override void OnInit()
    {
        // 实例化
        this.mainComp = mainComponentCreator();
        this.contentPane = this.mainComp;

        base.OnInit();

        // 窗口适配
        this.contentPane.SetSize(FairyGUI.GRoot.inst.width, FairyGUI.GRoot.inst.height);
        this.contentPane.AddRelation(FairyGUI.GRoot.inst, FairyGUI.RelationType.Size);

        // 点击时不调整显示层级
        bringToFontOnClick = false;

        // 自动注册关闭按钮
        if (this.frame != null)
        {
            FairyGUI.GComponent btnClose = this.frame.GetChild(UIConst.BTN_NAME_CLOSE).asCom;
            if (btnClose != null)
            {
                btnClose.onClick.Add(OnCloseWindow);
            }
        }
    }
}
