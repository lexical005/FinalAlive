using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NFramework
{
    /// <summary>
    /// 本地存储管理器
    /// </summary>
    public class StorageManager : Manager<StorageManager>
    {
        /// <summary>
        /// 本地存储数据类型
        /// </summary>
        private enum EStorageDataType
        {
            Account,
        }

        /// <summary>
        /// 本地存储的数据
        /// </summary>
        [System.NonSerialized]
        private List<NStorage.IStorageData> storage_datas = new List<NStorage.IStorageData>
        {
            new NStorage.StorageDataAccount(),
        };

        /// <summary>
        /// 自身初始化
        /// </summary>
        protected override void DoInit()
        {
            base.DoInit();

            // 加载存储的数据
            foreach (var one in storage_datas)
            {
                one.Load();
            }
        }

        /// <summary>
        /// 管理器逻辑启动
        /// </summary
        protected override void OnStart()
        {
            base.OnStart();
        }

        public static NStorage.StorageDataAccount Account
        {
            get
            {
                return inst.storage_datas[(int)EStorageDataType.Account] as NStorage.StorageDataAccount;
            }
        }
    }
}
