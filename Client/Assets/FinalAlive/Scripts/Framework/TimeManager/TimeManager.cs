using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace NFramework
{
    /// <summary>
    /// 时间管理器，提供的主要功能有：定时器（触发时机在通用Update之后）
    /// </summary>
    public class TimeManager : Manager<TimeManager>
    {
        /// <summary>
        /// 定时器列表
        /// </summary>
        private static LinkedList<TimerInfo> listTimers = null;


        /// <summary>
        /// 帧更新结束时的回调列表
        /// </summary>
        private static List<delegate_void_void> listFrameEnd = null;


        /// <summary>
        /// 帧更新结束协程
        /// </summary>
        private static Coroutine corFrameEnd = null;


        /// <summary>
        /// 初始化TimeManager
        /// </summary>
        static TimeManager()
        {
            listTimers = new LinkedList<TimerInfo>();
            listFrameEnd = new List<delegate_void_void>(2);
        }


        /// <summary>
        /// 添加一个定时器
        /// </summary>
        /// <param name="lifeScope">是否受时间缩组别放影响</param>
        /// <param name="timeScale">是否受时间缩放影响</param>
        /// <param name="interval">触发间隔. 单位秒</param>
        /// <param name="triggerCount">触发次数. -1 无限次数</param>
        /// <param name="callback">每次触发时的回调</param>
        public static TimerInfo AddTimer(ELifeScope lifeScope, bool timeScale, float interval, int triggerCount, delegate_void_void callback)
        {
            TimerInfo timer = new TimerInfo(lifeScope, true, interval, triggerCount, callback);
            listTimers.AddLast(timer);
            return timer;
        }


        /// <summary>
        /// 根据组别，移除定时器
        /// </summary>
        public static void RemoveTimerGroup(ELifeScope lifeScope)
        {
            LinkedListNode<TimerInfo> node = listTimers.First;
            LinkedListNode<TimerInfo> next = null;
            while (node != null)
            {
                next = node.Next;

                if (node.Value.StopScope(lifeScope))
                {
                    listTimers.Remove(node);
                }

                node = next;
            }
        }


        /// <summary>
        /// 在帧更新结束时进行回调
        /// </summary>
        /// <param name="callback"></param>
        public static void DoAtFrameEnd(delegate_void_void callback)
        {
            if (corFrameEnd == null)
            {
                corFrameEnd = inst.StartCoroutine(inst.delayAtFrameEnd());
            }

            listFrameEnd.Add(callback);
        }


        /// <summary>
        /// 帧更新结束协程
        /// </summary>
        /// <returns></returns>
        IEnumerator delayAtFrameEnd()
        {
            yield return new WaitForEndOfFrame();

            corFrameEnd = null;
            foreach (var callback in listFrameEnd)
            {
                if (callback != null)
                {
                    callback();
                }
            }
            listFrameEnd.Clear();
        }


        void Update()
        {
            LinkedListNode<TimerInfo> node = listTimers.First;
            LinkedListNode<TimerInfo> next = null;
            while (node != null)
            {
                next = node.Next;

                if (node.Value.Update())
                {
                    listTimers.Remove(node);
                }

                node = next;
            }
        }
    }
}
