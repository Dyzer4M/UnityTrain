using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
//计时器类，用来计时
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// 计时结束需要调用的回调函数
    /// </summary>
    readonly Action m_Callback;

    /// <summary>
    /// 计时时间和当前时间
    /// </summary>
    float m_Time, m_CurrentTime;

    /// <summary>
    /// 计算当前时间百分比
    /// </summary>
    public float normalizedProgress
    {
        get { return Mathf.Clamp(m_CurrentTime / m_Time, 0f, 1f); }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="newTime">定时</param>
    /// <param name="onElapsed">回调</param>
    public Timer(float newTime, Action onElapsed = null)
    {
        SetTime(newTime);
        m_CurrentTime = 0f;
        m_Callback += onElapsed;
    }

    /// <summary>
    /// 加时
    /// </summary>
    /// <param name="deltaTime">change in time between ticks</param>
    /// <returns>true if the timer has elapsed, false otherwise</returns>
    public virtual bool Tick(float deltaTime)
    {
        return AssessTime(deltaTime);
    }

    /// <summary>
    /// 计时器获得时间
    /// </summary>
    /// <param name="deltaTime">每帧所用的世界</param>
    /// <returns>计时器是否计时结束</returns>
    protected bool AssessTime(float deltaTime)
    {
        m_CurrentTime += deltaTime;
        if (m_CurrentTime >= m_Time)
        {
            FireEvent();
            return true;
        }

        return false;
    }

    /// <summary>
    /// 重置时间
    /// </summary>
    public void Reset()
    {
        m_CurrentTime = 0;
    }

    /// <summary>
    /// 计时结算调用所有回调
    /// </summary>
    public void FireEvent()
    {
        m_Callback.Invoke();
    }

    /// <summary>
    /// 设置计时时间
    /// </summary>
    /// <param name="newTime">sets the time to a new value</param>
    public void SetTime(float newTime)
    {
        m_Time = newTime;

        if (newTime <= 0)
        {
            m_Time = 0.1f;
        }
    }

}
