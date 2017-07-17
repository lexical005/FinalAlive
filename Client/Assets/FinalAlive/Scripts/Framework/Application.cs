using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFramework
{
    /// <summary>
    /// 应用程序类
    /// </summary>
    public static class Application
    {
        /// <summary>
        /// 所有的管理器实例
        /// </summary>
        private static List<ManagerBase> allManagers = new List<ManagerBase>(8);


        /// <summary>
        /// 记录Manager
        /// </summary>
        /// <param name="manager"></param>
        public static void AddManager(ManagerBase manager)
        {
            allManagers.Add(manager);
        }

        public static void OnManagerStart(ManagerBase manager)
        {
            foreach (var m in allManagers)
            {
                if (!m.bInitOver)
                {
                    return;
                }
            }

            // 所有管理器都已经初始化完成
            Debug.Log("all manager start");
        }

        /// <summary>
        /// 在场景开始切换前触发此事件（加载界面已显示）
        /// </summary>
        public static void OnBeforeSceneStartSwitch()
        {
            foreach (var m in allManagers)
            {
                m.OnBeforeSceneStartSwitch();
            }
        }

        ///// <summary>
        ///// 在场景开始切换后触发此事件
        ///// </summary>
        //public static void OnAfterSceneStartSwitch()
        //{
        //    foreach (var m in allManagers)
        //    {
        //        m.OnAfterSceneStartSwitch();
        //    }
        //}

        ///// <summary>
        ///// 在场景切换完成前触发此事件
        ///// </summary>
        //public static void OnBeforeSceneSwitchOver()
        //{
        //    foreach (var m in allManagers)
        //    {
        //        m.OnBeforeSceneSwitchOver();
        //    }
        //}

        /// <summary>
        /// 在场景切换完成后触发此事件（加载界面尚未被销毁）
        /// </summary>
        public static void OnAfterSceneSwitchOver()
        {
            foreach (var m in allManagers)
            {
                m.OnAfterSceneSwitchOver();
            }
        }
    }
}
