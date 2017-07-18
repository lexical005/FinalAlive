using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NGame
{
    /// <summary>
    /// 登陆/进入游戏逻辑
    /// </summary>
    public class GameLogin : GameBase
    {
        public GameLogin(GameType gameType, object gameParam) : base(gameType)
        {
        }

        /// <summary>
        /// 场景名称
        /// </summary>
        /// <returns></returns>
        public override string SceneName()
        {
            return "Scene-Login";
        }

        /// <summary>
        /// 玩法激活
        /// </summary>
        public override void OnStart()
        {
            base.OnStart();

            NFramework.UIManager.ShowHUD(UIHudLogin.CreateInstance());
        }
    }
}
