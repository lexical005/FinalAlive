using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NGame
{
    /// <summary>
    /// PVP1游戏逻辑
    /// </summary>
    public class GameBattlePVP1 : GameBase
    {
        public GameBattlePVP1(GameType gameType, object gameParam) : base(gameType)
        {
        }

        /// <summary>
        /// 场景名称
        /// </summary>
        /// <returns></returns>
        public override string SceneName()
        {
            return "Scene-Battle-PVP1";
        }

        /// <summary>
        /// 玩法激活
        /// </summary>
        public override void OnStart()
        {
            base.OnStart();

            NFramework.UIManager.ShowHUD(UIHudBattlePVP1.CreateInstance());
        }
    }
}
