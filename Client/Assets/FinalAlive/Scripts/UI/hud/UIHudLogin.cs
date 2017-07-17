using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System.Text.RegularExpressions;

/// <summary>
/// 登陆主界面
/// </summary>
public class UIHudLogin : UIWindowTemplate<UIHudLogin, NUIExport.hudLogin.UI_main>
{
    public UIHudLogin() : base(NUIExport.hudLogin.UI_main.CreateInstance)
    {
        NFramework.UIManager.AddPackage("hudLogin", ELifeScope.Scene);
    }

    /// <summary>
    /// 初始化完毕，创建实例
    /// </summary>
    protected override void OnInit()
    {
        base.OnInit();

        // 按钮事件
        this.mainComp.m_login.onClick.Add(OnClickLogin);
        this.mainComp.m_enter.onClick.Add(OnClickEnter);

        // 账号初始化
        NStorage.StorageDataAccount account_data = NFramework.StorageManager.Account;
        if (account_data.IsStoraged())
        {
            ShowEnterPage(account_data);
        }
        else
        {
            ShowLoginPage();
        }
    }

    /// <summary>
    /// 显示账号密码登陆分页
    /// </summary>
    private void ShowLoginPage()
    {
    }

    /// <summary>
    /// 显示进入游戏分页
    /// </summary>
    /// <param name="account_data"></param>
    private void ShowEnterPage(NStorage.StorageDataAccount account_data)
    {
        this.mainComp.m_account.text = account_data.LastLoginAccount;
        this.mainComp.m_password.text = account_data.LastLoginPassword;

        this.mainComp.m_tip.text = string.Format("[i]欢迎[color=#00FF00]{0}[/color]归来[/i]", account_data.LastLoginAccount);

        this.mainComp.m_page.selectedIndex = 1;
    }

    /// <summary>
    /// 账号密码登陆
    /// </summary>
    /// <param name="c"></param>
    private void OnClickLogin(EventContext c)
    {
        string new_account = this.mainComp.m_account.text;
        string new_password = this.mainComp.m_password.text;
        Regex regex_account = new Regex(@"^[a-zA-Z0-9_]{6,16}$");
        Regex regex_password = new Regex(@"^[a-zA-Z0-9_]{6,16}$");
        if (!regex_account.IsMatch(new_account) || !regex_password.IsMatch(new_password))
        {
            return;
        }

        NStorage.StorageDataAccount account_data = NFramework.StorageManager.Account;
        account_data.LastLoginAccount = new_account;
        account_data.LastLoginPassword = new_password;
        account_data.Save();

        // 切换到进入游戏分页
        ShowEnterPage(account_data);
    }

    /// <summary>
    /// 进入游戏
    /// </summary>
    /// <param name="c"></param>
    private void OnClickEnter(EventContext c)
    {
        NFramework.GameManager.TransitToGame(
            NGame.GameTransit.ETransitType.Direct,
            NGame.GameTransit.ETransitType.Direct,
            new NGame.GameTypeHome(),
            null);
    }
}
