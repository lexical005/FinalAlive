using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NFramework
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public class ResourceManager : Manager<ResourceManager>
    {
        /// <summary>
        /// 自身初始化
        /// </summary>
        protected override void DoInit()
        {
            base.DoInit();
        }

        /// <summary>
        /// 管理器逻辑启动
        /// </summary
        protected override void OnStart()
        {
            base.OnStart();
        }

        //
        public static void PreloadScene()
        {

        }
    }
}
