using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITransit : UIWindowTemplate<UITransit, NUIExport.transit.UI_main>
{
    /// <summary>
    /// 切换状态
    /// </summary>
    private enum ETransitStatus
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        Invalid,

        /// <summary>
        /// 等待开始
        /// </summary>
        WaitLoad,

        /// <summary>
        /// 异步加载中
        /// </summary>
        AysncLoad,

        /// <summary>
        /// 显示加载完成状态
        /// </summary>
        ShowLoadOver,

        /// <summary>
        /// 异步加载结束
        /// </summary>
        WaitEnd,
    }

    /// <summary>
    /// 异步加载状态
    /// </summary>
    private ETransitStatus m_Status = ETransitStatus.Invalid;


    /// <summary>
    /// 目标Game
    /// </summary>
    private NGame.GameBase m_TargetGame;

    /// <summary>
    /// 加载完毕时的回调
    /// </summary>
    private delegate_void_void m_LoadOverCallback = null;


    /// <summary>
    /// 异步加载目标场景
    /// </summary>
    private AsyncOperation m_AsyncLoadScene = null;

    /// <summary>
    /// 显示进度[0, 100]
    /// </summary>
    private int m_ShowProgress = 0;

    /// <summary>
    /// 加载完成状态显示几帧
    /// </summary>
    private int m_StayLoadOverFrameCount = 3;


    public UITransit() : base(NUIExport.transit.UI_main.CreateInstance)
    {
        NFramework.UIManager.AddPackage("transit", ELifeScope.Global);
    }


    /// <summary>
    /// 进行场景切换加载
    /// </summary>
    /// <param name="targetGame"></param>
    /// <param name="loadOverCallback"></param>
    public void StartTransit(NGame.GameBase targetGame, delegate_void_void loadOverCallback)
    {
        this.m_TargetGame = targetGame;
        this.m_LoadOverCallback = loadOverCallback;
        this.m_Status = ETransitStatus.WaitLoad;
    }

    protected override void OnUpdate()
    {
        if (m_Status == ETransitStatus.WaitLoad)
        {
            m_Status = ETransitStatus.AysncLoad;

            m_AsyncLoadScene = SceneManager.LoadSceneAsync("Scene-Home", LoadSceneMode.Single);
        }
        else if (m_Status == ETransitStatus.AysncLoad)
        {
            int tmp = (int)(m_AsyncLoadScene.progress * 100);
            if (m_ShowProgress < tmp || Mathf.Approximately(m_AsyncLoadScene.progress, 1))
            {
                m_ShowProgress++;
            }

            Debug.Log(string.Format("{0}-{1}", m_ShowProgress, tmp));

            mainComp.m_loading.asProgress.value = m_ShowProgress;
            if (m_ShowProgress >= 100)
            {
                m_Status = ETransitStatus.ShowLoadOver;
            }
        }
        else if (m_Status == ETransitStatus.ShowLoadOver)
        {
            --m_StayLoadOverFrameCount;
            if (m_StayLoadOverFrameCount == 0)
            {
                m_Status = ETransitStatus.WaitEnd;

                //NFramework.Application.OnBeforeSceneSwitchOver();
            }
        }
        else if (m_Status == ETransitStatus.WaitEnd)
        {
            m_Status = ETransitStatus.Invalid;

            if (m_LoadOverCallback != null)
            {
                m_LoadOverCallback();
            }
        }
    }
}
