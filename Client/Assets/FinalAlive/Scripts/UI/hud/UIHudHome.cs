using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

/// <summary>
/// 主城主界面
/// </summary>
public class UIHudHome : UIWindowTemplate<UIHudHome, NUIExport.hudHome.UI_main>
{
    /// <summary>
    /// 私有构造
    /// </summary>
    public UIHudHome() : base(NUIExport.hudHome.UI_main.CreateInstance)
    {
        NFramework.UIManager.AddPackage("hudHome", ELifeScope.Scene);
    }

    /// <summary>
    /// 初始化完毕，创建实例
    /// </summary>
    protected override void OnInit()
    {
        base.OnInit();

        // 按钮事件
        this.mainComp.m_battlePVP1.onClick.Add(OnClickBattlePVP1);
    }
        
    /// <summary>
    /// 进入PVP1
    /// </summary>
    /// <param name="c"></param>
    private void OnClickBattlePVP1(EventContext c)
    {
        NFramework.GameManager.TransitToGame(
            NGame.GameTransit.ETransitType.Fade,
            NGame.GameTransit.ETransitType.Fade,
            new NGame.GameTypePVP1(),
            null);
    }
}
