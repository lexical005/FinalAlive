using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NStorage
{
    /// <summary>
    /// 本地存储的账号数据
    /// </summary>
    public class StorageDataAccount : IStorageData
    {
        public string LastLoginAccount = string.Empty;
        public string LastLoginPassword = string.Empty;

        /// <summary>
        /// 本地是否有存储
        /// </summary>
        public bool IsStoraged()
        {
            return !string.IsNullOrEmpty(LastLoginAccount);
        }

        /// <summary>
        /// 加载存储的数据
        /// </summary>
        public void Load()
        {
            LastLoginAccount = PlayerPrefs.GetString("LastLoginAccount", string.Empty);
            LastLoginPassword = PlayerPrefs.GetString("LastLoginPassword", string.Empty);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save()
        {
            PlayerPrefs.SetString("LastLoginAccount", LastLoginAccount);
            PlayerPrefs.SetString("LastLoginPassword", LastLoginPassword);
        }

        /// <summary>
        /// 重置本地存储的数据
        /// </summary>
        public void Reset()
        {
            LastLoginAccount = string.Empty;
            LastLoginPassword = string.Empty;

            Save();
        }
    }
}
