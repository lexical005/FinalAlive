using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NGame
{
    /// <summary>
    /// 场景切换加载逻辑
    /// </summary>
    public class GameTransit : GameBase
    {
         /// <summary>
        /// 切换方式
        /// </summary>
        public enum ETransitType
        {
            /// <summary>
            /// 淡入/淡出
            /// </summary>
            Fade,

            /// <summary>
            /// 直接显示加载界面
            /// </summary>
            Direct,
        }


        /// <summary>
        /// 切换时使用的数据
        /// </summary>
        private class TransitData
        {
            /// <summary>
            /// 旧场景消失方式
            /// </summary>
            public ETransitType leaveTransitType;

            /// <summary>
            /// 新场景出现方式
            /// </summary>
            public ETransitType enterTransitType;

            /// <summary>
            /// 目标Game
            /// </summary>
            public GameBase targetGame;
        }

        /// <summary>
        /// 切换数据
        /// </summary>
        private TransitData m_TransitData;


        /// <summary>
        /// 切换界面
        /// </summary>
        private UITransit uiTransit = null;

        /// <summary>
        /// 目标Game
        /// </summary>
        public GameBase TargetGame
        {
            get
            {
                return this.m_TransitData.targetGame;
            }
        }


        public GameTransit(object gameParams) : base(new GameTypeTransit())
        {
            this.m_TransitData = gameParams as TransitData;
        }

        /// <summary>
        /// 玩法激活
        /// </summary>
        public override void OnStart()
        {
            base.OnStart();

            if (m_TransitData.leaveTransitType == ETransitType.Fade)
            {
                // 屏蔽层淡入显示
                NFramework.UIManager.SystemShieldFadeIn(true, OnLeaveTransit_ShieldFadeInOver);
            }
            else if (m_TransitData.leaveTransitType == ETransitType.Direct)
            {
                // 直接显示透明屏蔽层
                NFramework.UIManager.SystemShield(true, UIConst.COLOR_TRANSIT, UIConst.ALPHA_TRANSIT);

                // 显示切换界面
                this.ShowTransitWindow();

                // 开始加载
                this.StartTransit();
            }
        }

        /// <summary>
        /// 玩法结束
        /// </summary>
        public override void OnEnd()
        {
            base.OnEnd();

            this.DisposeTransitWindow();
        }


        /// <summary>
        /// 切换开始时-屏蔽层淡入结束
        /// </summary>
        private void OnLeaveTransit_ShieldFadeInOver()
        {
            Debug.Log("GameTransit.OnLeaveTransit_ShieldFadeInOver");

            // 显示切换界面
            ShowTransitWindow();

            // 屏蔽层淡出
            NFramework.UIManager.SystemShieldFadeOut(true, OnLeaveTransit_ShieldFadeOutOver);
        }

        /// <summary>
        /// 切换开始时-屏蔽层淡出结束
        /// </summary>
        private void OnLeaveTransit_ShieldFadeOutOver()
        {
            Debug.Log("GameTransit.OnLeaveTransit_ShieldFadeOutOver");

            // 开始加载
            this.StartTransit();
        }

        /// <summary>
        /// 场景切换加载完毕事件
        /// </summary>
        private void OnTransitLoadOver()
        {
            if (m_TransitData.enterTransitType == ETransitType.Fade)
            {
                // 屏蔽层淡入显示
                NFramework.UIManager.SystemShieldFadeIn(true, OnEnterTransit_ShieldFadeInOver);
            }
            else if (m_TransitData.enterTransitType == ETransitType.Direct)
            {
                // 移除透明屏蔽层
                NFramework.UIManager.SystemShield(false, UIConst.COLOR_TRANSIT, UIConst.ALPHA_TRANSIT);

                // 切换
                this.DoTransit();
            }
        }

        /// <summary>
        /// 切换结束时-屏蔽层淡入结束
        /// </summary>
        private void OnEnterTransit_ShieldFadeInOver()
        {
            Debug.Log("GameTransit.OnEnterTransit_ShieldFadeInOver");

            this.DoTransit();

            // 屏蔽层淡出（不再关注淡出结束事件）
            NFramework.UIManager.SystemShieldFadeOut(true, null);
        }

        ///// <summary>
        ///// 切换结束时-屏蔽层淡出结束
        ///// </summary>
        //private void OnEnterTransit_ShieldFadeOutOver()
        //{
        //    Debug.Log("GameTransit.OnEnterTransit_ShieldFadeOutOver");
        //}

        /// <summary>
        /// 进行切换
        /// </summary>
        private void DoTransit()
        {
            NFramework.Application.OnAfterSceneSwitchOver();
        }

        /// <summary>
        /// 显示场景切换界面
        /// </summary>
        private void ShowTransitWindow()
        {
            // 销毁旧场景相关界面
            NFramework.Application.OnBeforeSceneStartSwitch();

            // 显示加载界面
            uiTransit = UITransit.CreateInstance();
            NFramework.UIManager.ShowLogicModelWindow(uiTransit);
        }

        /// <summary>
        /// 开始加载
        /// </summary>
        private void StartTransit()
        {
            uiTransit.StartTransit(m_TransitData.targetGame, OnTransitLoadOver);
        }

        /// <summary>
        /// 销毁场景切换界面
        /// </summary>
        private void DisposeTransitWindow()
        {
            uiTransit.Dispose();
            uiTransit = null;
        }

        /// <summary>
        /// 生成切换数据
        /// </summary>
        /// <param name="leaveTransitType">旧场景消失方式</param>
        /// <param name="enterTransitType">新场景出现方式</param>
        /// <param name="targetGame"></param>
        /// <returns></returns>
        public static object NewTransitData(ETransitType leaveTransitType, ETransitType enterTransitType, GameBase targetGame)
        {
            return new TransitData {
                leaveTransitType = leaveTransitType,
                enterTransitType = enterTransitType,
                targetGame = targetGame,
            };
        }
    }
}
