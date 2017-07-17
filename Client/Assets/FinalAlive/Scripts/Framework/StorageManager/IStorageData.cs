using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NStorage
{
    /// <summary>
    /// 本地存储数据接口
    /// </summary>
    public interface IStorageData
    {
        bool IsStoraged();
        void Load();
        void Save();
        void Reset();
    }
}
