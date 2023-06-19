using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    void CreateSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] List<ActiveTimer> m_timers = new List<ActiveTimer>();

    private void Awake()
    {
        CreateSingleton();


    }

    public void RunAfterTime(Action function, float time)
    {
        m_timers.Add(new ActiveTimer(function, time, this));
    }

    public void DestroyTimer(ActiveTimer timer)
    {
        m_timers.Remove(timer);
    }

    private void Update()
    {
        for (int i = 0; i < m_timers.Count; i++)
        {
            m_timers[i].UpdateTimer();
        }
    }
}

[Serializable]
public class ActiveTimer
{
    [SerializeField] TimerManager m_manager;
    
    [SerializeField] Action m_functionToCall;
    [SerializeField] float m_timeUntilEnd;

    [SerializeField] float m_counter = 0;

    public ActiveTimer(Action function, float time, TimerManager timerManager)
    {
        m_functionToCall = function;
        m_timeUntilEnd = time;
        m_manager = timerManager;
    }

    public void UpdateTimer()
    {
        if (m_counter >= m_timeUntilEnd)
        {
            m_functionToCall();
            m_manager.DestroyTimer(this);
        }
        else
        {
            m_counter += Time.deltaTime;
        }
    }
}
