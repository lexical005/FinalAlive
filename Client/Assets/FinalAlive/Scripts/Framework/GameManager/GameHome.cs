﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NGame
{
    /// <summary>
    /// 主城游戏逻辑
    /// </summary>
    public class GameHome : GameBase
    {
        public GameHome(GameType gameType, object gameParam) : base(gameType)
        {
        }

        /// <summary>
        /// 场景名称
        /// </summary>
        /// <returns></returns>
        public override string SceneName()
        {
            return "Scene-Home";
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
