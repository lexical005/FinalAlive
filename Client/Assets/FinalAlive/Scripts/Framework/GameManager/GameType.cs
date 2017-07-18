
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
            return new GameLogin(this, gameParam);
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
            return new GameHome(this, gameParam);
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
            return new GameTransit(this, gameParam);
        }
    }

    /// <summary>
    /// 游戏类型--PVP1
    /// </summary>
    public class GameTypePVP1 : GameType
    {
        public GameTypePVP1() : base(EGameType.Battle_PVP_1)
        {
        }

        public override GameBase NewGame(object gameParam)
        {
            return new GameBattlePVP1(this, gameParam);
        }
    }
}
