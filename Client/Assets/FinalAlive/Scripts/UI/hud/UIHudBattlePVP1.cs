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
    /// 私有构造
    /// </summary>
    public UIHudBattlePVP1() : base(NUIExport.hudBattlePVP1.UI_main.CreateInstance)
    {
        NFramework.UIManager.AddPackage("hudBattlePVP1", ELifeScope.Scene);
    }
}
