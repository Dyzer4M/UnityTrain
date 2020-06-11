using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBehavior : MonoBehaviour
{
    /// <summary>
    /// L保存所有的计时器
    /// </summary>
    readonly List<Timer> m_ActiveTimers = new List<Timer>();

    /// <summary>
    /// 增加新的计时器
    /// </summary>
    /// <param name="newTimer">需要增加的计时器</param>
    public  void StartTimer(Timer newTimer)
    {
        m_ActiveTimers.Add(newTimer);

    }


    public void StopAllTimer()
    {
        for(int i = 0; i < m_ActiveTimers.Count; i++)
        {
            StopTimer(m_ActiveTimers[i]);
        }
    }


    /// <summary>
    /// 移除计时器
    /// </summary>
    /// <param name="timer">the timer to be removed from the list of active timers</param>
    protected void PauseTimer(Timer timer)
    {
        if (m_ActiveTimers.Contains(timer))
        {
            m_ActiveTimers.Remove(timer);
        }
    }

    /// <summary>
    ///暂停计时器
    /// </summary>
    /// <param name="timer">the timer to be stopped</param>
    protected void StopTimer(Timer timer)
    {
        timer.Reset();
        PauseTimer(timer);
    }

    /// <summary>
    /// 计时
    /// </summary>
    protected virtual void Update()
    {
        for (int i = m_ActiveTimers.Count - 1; i >= 0; i--)
        {
            if (m_ActiveTimers[i].Tick(Time.deltaTime))
            {
                StopTimer(m_ActiveTimers[i]);
            }
        }

    }     
}

