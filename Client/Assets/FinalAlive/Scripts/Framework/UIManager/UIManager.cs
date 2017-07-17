using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using NUIManagerInternal;


namespace NFramework
{
    /// <summary>
    /// 界面管理器
    /// </summary>
    public class UIManager : Manager<UIManager>
    {
        /// <summary>
        /// ui层级，枚举值高的层级，显示在枚举值低的层级之上
        /// </summary>
        private enum ELayer
        {
            /// <summary>
            /// 主界面，最底层
            /// </summary>
            hudLayer,

            /// <summary>
            /// 标准窗口层级
            /// </summary>
            windowLayer,

            /// <summary>
            /// 显示在标准窗口层级的上方，主要用途：在各功能系统界面上方，显示当前货币详情
            /// </summary>
            topLayer,

            /// <summary>
            /// 逻辑模态界面
            /// </summary>
            logicModelLayer,

            /// <summary>
            /// 系统模态界面
            /// </summary>
            systemModelLayer,

            count,
        }

        /// <summary>
        /// ui层级对应的根节点
        /// </summary>
        [System.NonSerialized]
        private GComponent[] layerRoot = new GComponent[(int)ELayer.count];

        /// <summary>
        /// 记录所有界面窗口
        /// </summary>
        [System.NonSerialized]
        private LinkedList<UIWindow> allWindows = new LinkedList<UIWindow>();


        /// <summary>
        /// 字体定义
        /// </summary>
        [System.Serializable]
        private class uiFont
        {
            public string uiFontName;
            public string fontPath;
        }
        [SerializeField]
        private uiFont[] uiFonts;


        /// <summary>
        /// 包信息
        /// </summary>
        [System.NonSerialized]
        private Dictionary<string, FairyPackage> dictPackage = new Dictionary<string, FairyPackage>();


        /// <summary>
        /// 系统屏蔽层
        /// </summary>
        [System.NonSerialized]
        private NUIExport.basic.UI_shield systemShield = null;

        /// <summary>
        /// 自身初始化
        /// </summary>
        protected override void DoInit()
        {
            base.DoInit();

            // 注册字体
            for (int index = 0; index < uiFonts.Length; ++index)
            {
                BaseFont font = FontManager.GetFont(uiFonts[index].fontPath);
                FontManager.RegisterFont(font, uiFonts[index].uiFontName);
            }


            // ui元素绑定
            NUIExport.Binder.BindAll();


            // 添加基础包
            AddPackage("basic", ELifeScope.Global);


            // 添加ui层的根节点
            for (int i = 0; i < layerRoot.Length; ++i)
            {
                ELayer eLayer = (ELayer)i;
                layerRoot[i] = UIPackage.CreateObject("basic", "empty").asCom;
                layerRoot[i].gameObjectName = eLayer.ToString();
                GRoot.inst.AddChild(layerRoot[i]);
            }
        }

        /// <summary>
        /// 管理器逻辑启动
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();

            // 创建系统屏蔽层
            systemShield = NUIExport.basic.UI_shield.CreateInstance();
            systemShield.visible = false;
            systemShield.enabled = false;
            layerRoot[(int)ELayer.systemModelLayer].AddChild(systemShield);
        }

        private void AddWindow(ELayer layer, UIWindow window)
        {
            layerRoot[(int)layer].AddChild(window);
            allWindows.AddLast(window);
        }

        /// <summary>
        /// 在场景开始切换前触发此事件（加载界面已显示）
        /// </summary>
        public override void OnBeforeSceneStartSwitch()
        {
            DestroyWindowLifeScopeScene();
        }

        /// <summary>
        /// 添加包定义
        /// </summary>
        /// <param name="namePackage"></param>
        /// <param name="lifeScope"></param>
        public static void AddPackage(string namePackage, ELifeScope lifeScope)
        {
            FairyPackage package = null;
            if (!inst.dictPackage.TryGetValue(namePackage, out package))
            {
                package = new FairyPackage(namePackage, lifeScope);
                inst.dictPackage[namePackage] = package;
            }
#if UNITY_EDITOR
            else
            {
                package.CheckScope(lifeScope);
            }
#endif

            package.Load();
        }


        /// <summary>
        /// 显示主界面
        /// </summary>
        /// <param name="window"></param>
        public static void ShowHUD(UIWindow window)
        {
            inst.AddWindow(ELayer.hudLayer, window);
        }


        /// <summary>
        /// 销毁所有生命周期为场景的界面
        /// </summary>
        public static void DestroyWindowLifeScopeScene()
        {
            LinkedListNode<UIWindow> node = inst.allWindows.First;
            LinkedListNode<UIWindow> next = null;
            while (node != null)
            {
                next = node.Next;

                if (node.Value.Dispose(ELifeScope.Scene))
                {
                    inst.allWindows.Remove(node);
                }

                node = next;
            }
        }

        /// <summary>
        /// 在逻辑模态窗口层显示窗口
        /// </summary>
        /// <param name="window"></param>
        public static void ShowLogicModelWindow(UIWindow window)
        {
            inst.layerRoot[(int)ELayer.logicModelLayer].AddChild(window);
        }


        /// <summary>
        /// 系统屏蔽立即生效/失效
        /// </summary>
        /// <param name="shield">是否屏蔽</param>
        /// <param name="shieldOnColor">屏蔽时的屏蔽层颜色</param>
        /// <param name="alpha">屏蔽时的屏蔽层透明度</param>
        public static void SystemShield(bool shield, Color shieldOnColor, float alpha)
        {
            if (inst.systemShield != null)
            {
                inst.systemShield.visible = shield;
                inst.systemShield.enabled = shield;

                if (shield)
                {
                    inst.systemShield.m_shield.color = shieldOnColor;
                    inst.systemShield.m_shield.alpha = alpha;
                }
            }
        }

        /// <summary>
        /// 系统屏蔽立即生效, 且根据fade配置是否有淡入表现
        /// </summary>
        /// <param name="fade">是否有淡入表现</param>
        /// <param name="fadeInCallback">有淡入表现时的回调方法</param>
        public static void SystemShieldFadeIn(bool fade, PlayCompleteCallback fadeInCallback = null)
        {
            if (inst.systemShield != null)
            {
                inst.systemShield.visible = true;
                inst.systemShield.enabled = true;

                if (fade)
                {
                    if (fadeInCallback != null)
                    {
                        inst.systemShield.m_fadein.Play(fadeInCallback);
                    }
                    else
                    {
                        inst.systemShield.m_fadein.Play();
                    }
                }
            }
        }

        /// <summary>
        /// 系统屏蔽在淡出表现结束后取消屏蔽
        /// </summary>
        /// <param name="fade">是否有淡出表现</param>
        /// <param name="fadeOuntCallback">有淡出表现时的回调方法</param>
        public static void SystemShieldFadeOut(bool fade, PlayCompleteCallback fadeOuntCallback = null)
        {
            if (inst.systemShield != null)
            {
                if (!fade)
                {
                    inst.systemShield.visible = false;
                    inst.systemShield.enabled = false;
                }
                else
                {
                    inst.systemShield.m_fadeout.Play(delegate ()
                    {
                        inst.systemShield.visible = false;
                        inst.systemShield.enabled = false;

                        if (fadeOuntCallback != null)
                        {
                            fadeOuntCallback();
                        }
                    });
                }
            }
        }
    }
}
