using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 一个具体的定时器
/// </summary>
public class TimerInfo
{
    readonly ELifeScope _lifeScope = ELifeScope.Global;     // 组别
    readonly bool _timeScale;                           // 时间是否受缩放影响
    readonly float _interval;                           // 触发间隔. 单位 秒
    readonly delegate_void_void _callback = null;       // 触发时的回调

    int _triggerCount;      // 剩余触发次数. -1 无限. >0 有限次数
    float passTime = 0f;    // 用于累计逝去时间
    bool running = true;    // 是否有效运行

    /// <summary>
    /// 定义一个定时器
    /// </summary>
    public TimerInfo(ELifeScope lifeScope, bool timeScale, float interval, int triggerCount, delegate_void_void callback)
    {
        this._lifeScope = lifeScope;
        this._timeScale = timeScale;
        this._interval = interval;
        this._triggerCount = triggerCount;
        this._callback = callback;
    }

    /// <summary>
    /// 时间步进，触发定时事件
    /// </summary>
    /// <returns></returns>
    public bool Update()
    {
        if (running)
        {
            passTime += _timeScale ? Time.deltaTime : Time.unscaledDeltaTime;
            if (passTime >= _interval)
            {
                passTime -= _interval;

                if (_triggerCount > 0)
                {
                    --_triggerCount;
                }

                _callback();
            }
        }
        return _triggerCount == 0 || !running;
    }

    /// <summary>
    /// 停止定时器
    /// </summary>
    public void Stop()
    {
        running = false;
    }

    /// <summary>
    /// 停止定时器
    /// </summary>
    public bool StopScope(ELifeScope lifeScope)
    {
        if (_lifeScope == lifeScope)
        {
            running = false;
            return true;
        }
        return false;
    }
}
