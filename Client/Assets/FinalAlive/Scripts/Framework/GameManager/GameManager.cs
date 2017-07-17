using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NFramework
{
    /// <summary>
    /// Game管理器
    /// </summary>
    public class GameManager : Manager<GameManager>
    {
        /// <summary>
        /// 当前Game
        /// </summary>
        [System.NonSerialized]
        private NGame.GameBase curGame = null;


        /// <summary>
        /// 管理器逻辑启动
        /// </summary
        protected override void OnStart()
        {
            base.OnStart();

            GoFirstGame();
        }

        /// <summary>
        /// 结束当前Game
        /// </summary>
        private void EndCurGame()
        {
            if (curGame != null)
            {
                curGame.OnEnd();
                curGame = null;
            }
        }

        /// <summary>
        /// 激活Game
        /// </summary>
        /// <param name="newGame"></param>
        private void ActiveGame(NGame.GameBase newGame)
        {
            curGame = newGame;
            curGame.OnStart();
        }

        /// <summary>
        /// 在场景切换完成后触发此事件（加载界面尚未被销毁）
        /// </summary>
        public override void OnAfterSceneSwitchOver()
        {
            if (curGame is NGame.GameTransit)
            {
                NGame.GameTransit transitGame = curGame as NGame.GameTransit;
                NGame.GameBase targetGame = transitGame.TargetGame;

                EndCurGame();

                ActiveGame(targetGame);
            }
        }


        /// <summary>
        /// 进入初始Game
        /// </summary>
        /// <param name="targetGameParams"></param>
        public static void GoFirstGame()
        {
            inst.ActiveGame(new NGame.GameLogin(null));
        }

        /// <summary>
        /// 切换到Game
        /// </summary>
        /// <param name="leaveTransitType">旧场景消失方式</param>
        /// <param name="enterTransitType">新场景出现方式</param>
        /// <param name="targetGameType">目标Game类型</param>
        /// <param name="targetGameParams">目标Game参数</param>
        public static void TransitToGame(
            NGame.GameTransit.ETransitType leaveTransitType,
            NGame.GameTransit.ETransitType enterTransitType,
            NGame.GameType targetGameType,
            object targetGameParams)
        {
            inst.EndCurGame();

            var targetGame = targetGameType.NewGame(targetGameParams);
            var transitParam = NGame.GameTransit.NewTransitData(leaveTransitType, enterTransitType, targetGame);
            inst.ActiveGame(new NGame.GameTransit(transitParam));
        }
    }
}
