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
}
