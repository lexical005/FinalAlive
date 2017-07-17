
namespace NGame
{

    /// <summary>
    /// 游戏类型
    /// </summary>
    public abstract class GameType
    {
        protected enum EGameType
        {
            Invalid,

            /// <summary>
            /// 场景切换
            /// </summary>
            Transit,


            /// <summary>
            /// 登录
            /// </summary>
            Login,

            /// <summary>
            /// 主城
            /// </summary>
            Home,

            /// <summary>
            /// 战斗-PVP-1
            /// </summary>
            Battle_PVP_1,
        }

        private EGameType m_GameType = EGameType.Invalid;

        protected GameType(EGameType _GameType)
        {
            this.m_GameType = _GameType;
        }

        /// <summary>
        /// 生成GameType对应的Game实例
        /// </summary>
        /// <param name="gameParam"></param>
        /// <returns></returns>
        public abstract GameBase NewGame(object gameParam);
    }

    /// <summary>
    /// 游戏类型--登陆
    /// </summary>
    public class GameTypeLogin : GameType
    {
        public GameTypeLogin() : base(EGameType.Login)
        {
        }

        public override GameBase NewGame(object gameParam)
        {
            return new GameLogin(gameParam);
        }
    }

    /// <summary>
    /// 游戏类型--主城
    /// </summary>
    public class GameTypeHome : GameType
    {
        public GameTypeHome() : base(EGameType.Home)
        {
        }

        public override GameBase NewGame(object gameParam)
        {
            return new GameHome(gameParam);
        }
    }

    /// <summary>
    /// 游戏类型--场景切换
    /// </summary>
    public class GameTypeTransit : GameType
    {
        public GameTypeTransit() : base(EGameType.Transit)
        {
        }

        public override GameBase NewGame(object gameParam)
        {
            return new GameTransit(gameParam);
        }
    }
}
