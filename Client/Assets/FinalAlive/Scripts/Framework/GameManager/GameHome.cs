using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NGame
{
    /// <summary>
    /// 主城游戏逻辑
    /// </summary>
    public class GameHome : GameBase
    {
        public GameHome(object gameParam) : base(new GameTypeHome())
        {
        }

        /// <summary>
        /// 玩法激活
        /// </summary>
        public override void OnStart()
        {
            base.OnStart();

            NFramework.UIManager.ShowHUD(UIHudHome.CreateInstance());
        }
    }
}
