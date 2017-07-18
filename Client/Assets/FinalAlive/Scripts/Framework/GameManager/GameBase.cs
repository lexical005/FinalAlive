using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NGame
{
    /// <summary>
    /// 游戏逻辑基类
    /// </summary>
    public abstract class GameBase
    {
        protected readonly GameType m_GameType;

        protected GameBase(GameType _GameType)
        {
            this.m_GameType = _GameType;
        }

        /// <summary>
        /// 场景名称
        /// </summary>
        /// <returns></returns>
        public abstract string SceneName();

        /// <summary>
        /// 预加载Game相关的资源
        /// </summary>
        public virtual void Preload()
        {
        }


        /// <summary>
        /// 玩法结束
        /// </summary>
        public virtual void OnEnd()
        {
        }


        /// <summary>
        /// 玩法激活
        /// </summary>
        public virtual void OnStart()
        {
        }
    }
}
