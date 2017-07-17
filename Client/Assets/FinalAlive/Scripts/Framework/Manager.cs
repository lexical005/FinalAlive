using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFramework
{
    /// <summary>
    /// 管理器基类
    /// </summary>
    public class ManagerBase : MonoBehaviour
    {
        /// <summary>
        /// 是否初始化完成
        /// </summary>
        [System.NonSerialized]
        private bool _bInitOver = false;
        public bool bInitOver
        {
            get
            {
                return _bInitOver;
            }
        }


        /// <summary>
        /// 执行管理器自身初始化，此时只执行管理器内部的初始化，禁止管理器之间相互操作引用
        /// </summa
        protected virtual void DoInit()
        {
            Debug.Log(string.Format("{0} DoInit", this.name));
        }

        /// <summary>
        /// 管理器启动完成事件
        /// </summary>
        protected virtual void OnStart()
        {
            Debug.Log(string.Format("{0} OnStart", this.name));

            _bInitOver = true;

            Application.OnManagerStart(this);
        }

        /// <summary>
        /// 在场景开始切换前触发此事件
        /// </summary>
        public virtual void OnBeforeSceneStartSwitch()
        {

        }

        ///// <summary>
        ///// 在场景开始切换后触发此事件（暂不开放该事件）
        ///// </summary>
        //public virtual void OnAfterSceneStartSwitch()
        //{

        //}

        ///// <summary>
        ///// 在场景切换完成前触发此事件（暂不开放该事件）
        ///// </summary>
        //public virtual void OnBeforeSceneSwitchOver()
        //{

        //}

        /// <summary>
        /// 在场景切换完成后触发此事件
        /// </summary>
        public virtual void OnAfterSceneSwitchOver()
        {

        }
    }


    /// <summary>
    /// 管理器单例
    /// </summary>
    public class Manager<T> : ManagerBase where T : Manager<T>
    {
        /// <summary>
        /// 管理器唯一实例
        /// 不对外公开
        /// </summary>
        protected static T inst = null;


        /// <summary>
        /// 初始化方法
        /// </summary>
        private void Awake()
        {
            this.name = GetType().FullName;

            if (inst != null)
            {
                throw new Exception(string.Format("Duplicate Manager {0}", this.name));
            }

            inst = this as T;

            Application.AddManager(inst);

            DoInit();
        }


        /// <summary>
        /// 初始化完成
        /// </summary>
        private void Start()
        {
            OnStart();
        }
    }
}
